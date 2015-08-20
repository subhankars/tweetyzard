using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using tweetyzard.dataaccess;
using Tweetinvi;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.Models;
using TweetinviLogic.Model;
using tweetyzard.dataaccess.DataAccess;
using tweetyzard.domain;
using twetyzard.utility;

namespace tweetyzard.utility
{
    public partial class tweetyzard : MetroForm
    {
        #region Private fields
        private string accessToken = System.Configuration.ConfigurationManager.AppSettings["AccessToken"].ToString();
        private string accessTokenSecret = System.Configuration.ConfigurationManager.AppSettings["AccessTokenSecret"].ToString();
        private string consumerKey = System.Configuration.ConfigurationManager.AppSettings["ConsumerKey"].ToString();
        private string consumerSecret = System.Configuration.ConfigurationManager.AppSettings["ConsumerSecret"].ToString();
        private int nbTweetDetected = 0;
        private int searchedTweet = 0;
        private List<ITrend> trends;
        private int fetch = 0;
        private List<TweetStore> listOfGatheredTweets = new List<TweetStore>();
        #endregion

        public tweetyzard()
        {
            InitializeComponent();
            searchPhraseTextBox.Focus();

            try
            {
                TwitterCredentials.Credentials = TwitterCredentials.CreateCredentials(accessToken, accessTokenSecret, consumerKey, consumerSecret);
            }
            catch
            {
                MessageBox.Show("Exception occured at authentication");
                return;
            }
            
            var loggedUser = User.GetLoggedUser();
            
            if (loggedUser != null)
            {
                userNameLabel.Visible = true;
                pbUser.Visible = true;
                userNameLabel.Text = loggedUser.Name;
            }
            else
            {
                userNameLabel.Visible = false;
                pbUser.Visible = false;
            }

            lblStream.ForeColor = Color.DimGray;
            btnExport.Enabled = false;
            ExportToDb.Enabled = false;
            
            string version = "ver."+ System.Reflection.Assembly.GetExecutingAssembly()
                                           .GetName()
                                           .Version
                                           .ToString();
            versionInfo.Text = version;

            try
            {
                trendingTopics.Text = "";
                var trendsController = TweetinviContainer.Resolve<ITrendsController>();
                IPlaceTrends placeTrends = trendsController.GetPlaceTrendsAt(1);

                if (placeTrends != null)
                {
                    trends = placeTrends.Trends;
                }
            }
            catch
            {
                trendingTopics.Text = "";
            }

            toolTipTw.SetToolTip(this.btnStartStream, "Starts the stream of tweets for the given keyword using Stream API");
            toolTipTw.SetToolTip(this.btnSearch, "Searches tweets for the given keyword using Search API");
            toolTipTw.SetToolTip(this.btnExport, "Exports gathered tweets to choosen datasource");
            toolTipTw.SetToolTip(this.btnReset, "Refreshes the form");
            toolTipTw.SetToolTip(this.btnStopStream, "Stops the running search query");
            toolTipTw.SetToolTip(this.geoFlag, "Searches tweets which are only geo tagged");
            
        }

        #region PrivateMethods
        private int tweetCounter = 0;
        private void BackgroundWorkerOnProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            try
            {
                ITweet tweet = null;

                if (progressChangedEventArgs.UserState != null)
                {
                    if (!backgroundWorker.CancellationPending && !this.IsDisposed)
                    {
                        tweet = (ITweet)progressChangedEventArgs.UserState;

                        if (nbTweetDetected == 1 && tweetsTextBox.Text.Length > 0)
                        {
                            tweetsTextBox.Text = "";
                        }

                        ////Refresh tweetsTextBox after every 1000 tweets
                        if (tweetCounter == 1000)
                        {
                            tweetsTextBox.ResetText();
                            tweetCounter = 0;
                        }
                        tweetCounter++;

                        tweetsTextBox.AppendText(nbTweetDetected.ToString() + String.Format(". {0} :  {1}\r\n", Convert.ToString(tweet.Creator.Name), Convert.ToString(tweet.Text)) + "\n");
                        tweetsTextBox.SelectionStart = tweetsTextBox.TextLength;
                        tweetsTextBox.ScrollToCaret();
                        tweetCount.Text = nbTweetDetected.ToString("#,##0");
                        lblStream.Text = "Live Streaming...";
                        lblStream.ForeColor = Color.Teal;
                        var tweetStore = Utility.MapStreamedTweetToTweetDomain((ITweet)progressChangedEventArgs.UserState, searchPhraseTextBox.Text);
                        listOfGatheredTweets.Add(tweetStore);
                        if (System.Configuration.ConfigurationManager.AppSettings["SaveStreamToDatabase"].ToString() == "true")
                        {
                            using (var dbCtx = new Entities())
                            {
                                dbCtx.TweetStore.Add(tweetStore);
                                dbCtx.SaveChanges();
                            } 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.ToString());
            }
        }

        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            try
            {
                nbTweetDetected = 0;
                var stream = Stream.CreateFilteredStream();
                if (geoFlag.Checked)
                {
                    ICoordinates coordinates1 = new Coordinates(-180, -90);
                    ICoordinates coordinates2 = new Coordinates(180, 90);
                    ILocation location = new Location(coordinates1, coordinates2);
                    stream.AddLocation(location);
                }

                stream.AddTrack(searchPhraseTextBox.Text);
                stream.MatchingTweetReceived += (o, args) =>
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        stream.StopStream();
                    }

                    backgroundWorker.ReportProgress(++nbTweetDetected, args.Tweet);
                };

                stream.StartStreamMatchingAllConditions();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.ToString()); ;
            }
        }

        private void btnStartStream_Click(object sender, EventArgs e)
        {

            if (searchPhraseTextBox.Text.Length < 1)
            {
                MessageBox.Show("Search phrase is required to start streaming");
                searchPhraseTextBox.Focus();
                return;
            }

            lblStream.Text = "Live Stream";
            tweetCount.Text = "0";
            listOfGatheredTweets = new List<TweetStore>();
            tweetsTextBox.Text = "";
            tweetsTextBox.Text = "Searching for tweets...\n\n";
            btnStartStream.Enabled = false;
            searchPhraseTextBox.Enabled = false;
            geoFlag.Enabled = false;
            btnExport.Enabled = false;
            btnSearch.Enabled = false;

            backgroundWorker = new BackgroundWorker();

            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker = new BackgroundWorker();
                backgroundWorker.WorkerSupportsCancellation = true;
                backgroundWorker.WorkerReportsProgress = true;
                backgroundWorker.DoWork += BackgroundWorkerOnDoWork;
                backgroundWorker.ProgressChanged += BackgroundWorkerOnProgressChanged;
                backgroundWorker.RunWorkerCompleted += BackgroundWorkerOnRunWorkerCompleted;
                backgroundWorker.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Previous operation is still running in backgroung. Please wait or restart your application");
                return;
            }
        }

        private void SearchBackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnStartStream.Enabled = true;
            searchPhraseTextBox.Enabled = true;
            btnExport.Enabled = true;
            ExportToDb.Enabled = true;
        }

        private void SearchBackgroundWorkerOnProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            try
            {
                ITweet tweet = null;

                if (progressChangedEventArgs.UserState != null)
                {
                    if (!backgroundWorker.CancellationPending && !this.IsDisposed)
                    {
                        tweet = (ITweet)progressChangedEventArgs.UserState;

                        ////Remove searching... text as soon as a tweet is found
                        if (searchedTweet == 1 && tweetsTextBox.Text.Length > 0)
                        {
                            tweetsTextBox.Text = "";
                        }

                        tweetsTextBox.AppendText(searchedTweet.ToString() + String.Format(". {0} :  {1}\r\n", Convert.ToString(tweet.Creator.Name), Convert.ToString(tweet.Text)) + "\n");
                        tweetsTextBox.SelectionStart = tweetsTextBox.TextLength;
                        tweetsTextBox.ScrollToCaret();
                        tweetCount.Text = searchedTweet.ToString("#,##0");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.ToString());
            }
        }

        private void SearchBackgroundWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            searchedTweet = 0;
            var searchParams = Search.GenerateSearchTweetParameter(searchPhraseTextBox.Text);
            searchParams.TweetSearchFilter = TweetSearchFilter.OriginalTweetsOnly;
            searchParams.SearchQuery = searchPhraseTextBox.Text;
            searchParams.Lang = Language.English;
            searchParams.SearchType = SearchResultType.Recent;
            searchParams.Until = DateTime.Today;
            searchParams.MaximumNumberOfResults = 1000;
            List<ITweet> searchedTweets = Search.SearchTweets(searchParams);


            if (searchedTweets.Count > 0)
            {
                foreach (ITweet tweet in searchedTweets)
                {
                    listOfGatheredTweets.Add(Utility.MapStreamedTweetToTweetDomain(tweet, searchPhraseTextBox.Text));
                    backgroundWorkerSearch.ReportProgress(++searchedTweet, tweet);
                }
            }
            else
            {
                tweetsTextBox.Text = "";
                tweetsTextBox.Text = "No tweets found. \n\n";
            }

        }

        private void BackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            btnStartStream.Enabled = true;
        }

        private void btnStopStream_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
                if (listOfGatheredTweets.Count > 0)
                {
                    tweetsTextBox.AppendText("\r\n --- Stream Stopped, You can export the data to excel or to database now ---");
                    tweetsTextBox.SelectionStart = tweetsTextBox.TextLength;
                    tweetsTextBox.ScrollToCaret();
                }

                lblStream.ForeColor = Color.DimGray;
                searchPhraseTextBox.Focus();
                btnStartStream.Enabled = true;
                btnSearch.Enabled = true;
                searchPhraseTextBox.Enabled = true;
                btnExport.Enabled = true;
                ExportToDb.Enabled = true;
                geoFlag.Enabled = true;
                lblStream.Text = "Live Stream";
            }
            else if (backgroundWorkerSearch.IsBusy)
            {
                backgroundWorkerSearch.CancelAsync();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ////Export to Excel as of now
            if (listOfGatheredTweets.Count > 0)
            {
                var dlg = new SaveFileDialog();
                dlg.Filter = "Excel Files|*.xlsx;";
                dlg.ShowDialog();
                if (!string.IsNullOrEmpty(dlg.FileName))
                {
                    DataSet dsTweetContainer = new DataSet();
                    tweetsTextBox.AppendText("\r\n Exporting your data to excel. Please wait...");
                    dsTweetContainer.Tables.Add(Utility.ConvertToDataTable(listOfGatheredTweets, "TweetyzardExport"));
                    using (dsTweetContainer)
                    {
                        OpenXMLHelper.ExportDataSetToExcel(dsTweetContainer, dlg.FileName, true);
                        tweetsTextBox.AppendText("\r\n Your data has been exported to: " + dlg.FileName);
                        tweetsTextBox.SelectionStart = tweetsTextBox.TextLength;
                        tweetsTextBox.ScrollToCaret();
                        listOfGatheredTweets = new List<TweetStore>();
                    }
                }
            }
            else
            {
                MessageBox.Show("No Tweet Gathered", "No Tweets", MessageBoxButtons.OK);
                return;
            }

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
            }

            this.Reset();
        }

        private void Reset()
        {
            lblStream.ForeColor = Color.DimGray;
            searchPhraseTextBox.Focus();
            btnStartStream.Enabled = true;
            searchPhraseTextBox.Enabled = true;
            btnExport.Enabled = false;
            ExportToDb.Enabled = false;
            tweetCount.Text = "0";
            searchPhraseTextBox.Text = "";
            listOfGatheredTweets = new List<TweetStore>();
            tweetsTextBox.Text = "";
            geoFlag.Checked = false;
            geoFlag.Enabled = true;
            lblStream.Text = "Live Stream";
        }

        private void thisTimer_Tick(object sender, EventArgs e)
        {
            currentTime.Text = DateTime.Now.ToString("F");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (searchPhraseTextBox.Text.Length < 1)
            {
                MessageBox.Show("Search phrase is required to start searching");
                searchPhraseTextBox.Focus();
                return;
            }

            backgroundWorkerSearch = new BackgroundWorker();

            if (!backgroundWorker.IsBusy && !backgroundWorkerSearch.IsBusy)
            {
                backgroundWorkerSearch = new BackgroundWorker();
                backgroundWorkerSearch.WorkerSupportsCancellation = true;
                backgroundWorkerSearch.WorkerReportsProgress = true;
                backgroundWorkerSearch.DoWork += SearchBackgroundWorkerOnDoWork;
                backgroundWorkerSearch.ProgressChanged += SearchBackgroundWorkerOnProgressChanged;
                backgroundWorkerSearch.RunWorkerCompleted += SearchBackgroundWorkerOnRunWorkerCompleted;
                backgroundWorkerSearch.RunWorkerAsync();
                lblStream.Text = "Search Result";
                tweetCount.Text = "0";
                listOfGatheredTweets = new List<TweetStore>();
                tweetsTextBox.Text = "";
                tweetsTextBox.Text = "Searching for tweets...\n\n";
                btnStartStream.Enabled = false;
                searchPhraseTextBox.Enabled = false;
                geoFlag.Enabled = false;
                btnExport.Enabled = false;
            }
            else
            {
                MessageBox.Show("Previous operation is still running in backgroung. Please wait or restart your application");
                return;
            }
        }

        private void ExportToDb_Click(object sender, EventArgs e)
        {
            try
            {
                if (DatabaseHelper.SqlBulkInsert("TweetStore", Utility.ConvertToDataTable(listOfGatheredTweets, "TweetStore")))
                {
                    tweetsTextBox.AppendText("\r\n Your data has been exported to database");
                }
                else
                {
                    tweetsTextBox.AppendText("\r\n Something went wrong while saving data to database");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Exception occured while saving to database");
                return;
            }
        }

        private void trendTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                while (fetch != 10)
                {
                    trendingTopics.Text = trends[fetch].Name;
                    fetch++;
                    if (fetch == 10)
                    {
                        fetch = 0;
                    }
                    return;
                }
            }
            catch
            {
                trendTimer.Stop();
                return;
            }

        }

        private void CopyTrendToSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(trendingTopics.Text))
            {
                searchPhraseTextBox.Text = trendingTopics.Text;
            }
        }

        private void trendRefreshTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                var trendsController = TweetinviContainer.Resolve<ITrendsController>();
                var placeTrends = trendsController.GetPlaceTrendsAt(1);
                if (placeTrends != null)
                {
                    trendTimer.Stop();
                    fetch = 0;
                    trends = new List<ITrend>();
                    trends = placeTrends.Trends;
                    trendTimer.Start();
                }
            }
            catch
            {
                return;
            }
        } 

        #endregion
        
    }
}