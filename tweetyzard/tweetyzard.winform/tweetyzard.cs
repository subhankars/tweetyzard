using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Tweetinvi;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.Models;
using TweetinviLogic.Model;
using tweetyzard.domain;
using twetyzard.utility;

namespace tweetyzard.utility
{
    public partial class tweetyzard : MetroForm
    {
        #region Private fields
        private string userKey = System.Configuration.ConfigurationManager.AppSettings["userKey"].ToString();
        private string userSecret = System.Configuration.ConfigurationManager.AppSettings["userSecret"].ToString();
        private string consumerKey = System.Configuration.ConfigurationManager.AppSettings["consumerKey"].ToString();
        private string consumerSecret = System.Configuration.ConfigurationManager.AppSettings["consumerSecret"].ToString();
        private int nbTweetDetected = 0;
        private int searchedTweet = 0;
        private List<ITrend> trends;
        private int fetch = 0;
        #endregion
        
        //public List<CustomTweetDomain> listOfGatheredTweets = new List<CustomTweetDomain>();
        public List<TweetStore> listOfGatheredTweets = new List<TweetStore>();

        public tweetyzard()
        {
            InitializeComponent();
            searchPhraseTextBox.Focus();
            TwitterCredentials.Credentials = TwitterCredentials.CreateCredentials(userKey, userSecret, consumerKey, consumerSecret);
            
            var loggedUser = User.GetLoggedUser();
            
            if (loggedUser != null)
            {
                userNameLabel.Visible = true;
                //profileImage.Visible = true;
                //welcomeLabel.Visible = true;
                pbUser.Visible = true;
                userNameLabel.Text = loggedUser.Name;
               // profileImage.ImageLocation = loggedUser.UserDTO.ProfileImageUrl;
            }
            else
            {
                userNameLabel.Visible = false;
                //profileImage.Visible = false;
                //welcomeLabel.Visible = false;
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

            trendingTopics.Text = "";
            var trendsController = TweetinviContainer.Resolve<ITrendsController>();
            IPlaceTrends placeTrends = trendsController.GetPlaceTrendsAt(1);

            if (placeTrends != null)
            {
                trends = placeTrends.Trends;
            }

            toolTipTw.SetToolTip(this.StartStreamButton, "Starts the stream of tweets for the given keyword using Stream API");
            toolTipTw.SetToolTip(this.btnSearch, "Searches tweets for the given keyword using Search API");
            toolTipTw.SetToolTip(this.btnExport, "Exports gathered tweets to choosen datasource");
            toolTipTw.SetToolTip(this.btnReset, "Refreshes the form");
            toolTipTw.SetToolTip(this.StopStreamButton, "Stops the running search query");
            toolTipTw.SetToolTip(this.geoFlag, "Searches tweets which are only geo tagged");
            
        }
     
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
                        
                        ////Remove searching... text as soon as a tweet is found
                        if (nbTweetDetected == 1 && tweetsTextBox.Text.Length > 0)
                        {
                            tweetsTextBox.Text = "";
                        }

                        tweetsTextBox.AppendText(nbTweetDetected.ToString() + String.Format(". {0} :  {1}\r\n", Convert.ToString(tweet.Creator.Name), Convert.ToString(tweet.Text)) + "\n");
                        tweetsTextBox.SelectionStart = tweetsTextBox.TextLength;
                        tweetsTextBox.ScrollToCaret();
                        tweetCount.Text = nbTweetDetected.ToString("#,##0");
                        lblStream.Text = "Live Streaming...";
                        lblStream.ForeColor = Color.Teal;

                        listOfGatheredTweets.Add(Utility.MapStreamedTweetToTweetDomain((ITweet)progressChangedEventArgs.UserState, searchPhraseTextBox.Text));
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

        private void StartStreamClicked(object sender, EventArgs e)
        {

            if (searchPhraseTextBox.Text.Length < 1)
            {
                MessageBox.Show("Search phrase is required to start streaming");
                searchPhraseTextBox.Focus();
                return;
            } 
            
            lblStream.Text = "Live Stream";
            tweetCount.Text = "0";
            //listOfGatheredTweets = new List<CustomTweetDomain>();
            listOfGatheredTweets = new List<TweetStore>();
            tweetsTextBox.Text = "";
            tweetsTextBox.Text = "Searching for tweets...\n\n";
            StartStreamButton.Enabled = false;
            searchPhraseTextBox.Enabled = false;
            geoFlag.Enabled = false;
            btnExport.Enabled = false;

            backgroundWorker = new BackgroundWorker();

            if (!backgroundWorker.IsBusy && !backgroundWorkerSearch.IsBusy)
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
            StartStreamButton.Enabled = true;
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
            //searchParams.SinceId = 1;
            //searchParams.MaxId = long.MaxValue;
            searchParams.MaximumNumberOfResults = 10000;
            //searchParams.SetGeoCode(Tweetinvi.Geo.GenerateCoordinates(-10, 20), 1000000, DistanceMeasure.Miles);

            List<ITweet> searchedTweets = Search.SearchTweets(searchParams);


            if (searchedTweets.Count > 0)
            {
                foreach (ITweet tweet in searchedTweets)
                {
                    listOfGatheredTweets.Add(Utility.MapStreamedTweetToTweetDomain(tweet, searchPhraseTextBox.Text));
                    backgroundWorkerSearch.ReportProgress(++searchedTweet, tweet);
                }
            }
           
        }

        private void BackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            StartStreamButton.Enabled = true;
        }

        private void StopStreamButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
                if (listOfGatheredTweets.Count > 0)
                {
                    tweetsTextBox.AppendText("\r\n***** Stream Stopped. You can export the data now *****");
                    tweetsTextBox.SelectionStart = tweetsTextBox.TextLength;
                    tweetsTextBox.ScrollToCaret();
                }

                lblStream.ForeColor = Color.DimGray;
                searchPhraseTextBox.Focus();
                StartStreamButton.Enabled = true;
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
                    dsTweetContainer.Tables.Add(Utility.ToDataTable(listOfGatheredTweets, "TweetyzardExport"));
                    using (dsTweetContainer)
                    {
                        OpenXMLHelper.ExportDataSetToExcel(dsTweetContainer, dlg.FileName);
                        tweetsTextBox.AppendText("\r\n Your data has been exported to: " + dlg.FileName);
                        tweetsTextBox.SelectionStart = tweetsTextBox.TextLength;
                        tweetsTextBox.ScrollToCaret();
                        //listOfGatheredTweets = new List<CustomTweetDomain>();
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
            StartStreamButton.Enabled = true;
            searchPhraseTextBox.Enabled = true;
            btnExport.Enabled = false;
            ExportToDb.Enabled = false;
            tweetCount.Text = "0";
            searchPhraseTextBox.Text = "";
            //listOfGatheredTweets = new List<CustomTweetDomain>();
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
                //listOfGatheredTweets = new List<CustomTweetDomain>();
                listOfGatheredTweets = new List<TweetStore>();
                tweetsTextBox.Text = "";
                tweetsTextBox.Text = "Searching for tweets...\n\n";
                StartStreamButton.Enabled = false;
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
            MessageBox.Show("Export to database is not supported at this moment", "DB Export not supported", MessageBoxButtons.OK);
            return;
        }

        private void trendTimer_Tick(object sender, EventArgs e)
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
            catch (Exception ex)
            {
                return;
            }


        }

      
        
    }
   
}