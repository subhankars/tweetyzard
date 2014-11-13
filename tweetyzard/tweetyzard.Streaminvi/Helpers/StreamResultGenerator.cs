using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using Streaminvi.Properties;
using TweetinviCore.Enum;
using TweetinviCore.Events;
using TweetinviCore.Events.EventArguments;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Exceptions;
using TweetinviCore.Interfaces.Streaminvi;

namespace Streaminvi.Helpers
{
    /// <summary>
    /// Extract objects from any kind of stream
    /// </summary>
    public class StreamResultGenerator : IStreamResultGenerator
    {
        private readonly IExceptionHandler _exceptionHandler;
        private const int STREAM_RESUME_DELAY = 1000;

        public event EventHandler StreamStarted;
        public event EventHandler StreamResumed;
        public event EventHandler StreamPaused;
        public event EventHandler<StreamExceptionEventArgs> StreamStopped;

        private StreamReader _currentReader;
        private Exception _lastException;

        private bool IsRunning
        {
            get { return _streamState == StreamState.Resume || _streamState == StreamState.Pause; }
        }

        private StreamState _streamState;
        public StreamState StreamState
        {
            get { return _streamState; }
            set
            {
                if (_streamState != value)
                {
                    _streamState = value;

                    switch (_streamState)
                    {
                        case StreamState.Resume:
                            this.Raise(StreamResumed);
                            break;
                        case StreamState.Pause:
                            this.Raise(StreamPaused);
                            break;
                        case StreamState.Stop:
                            var streamExceptionEventArgs = new StreamExceptionEventArgs(_lastException);
                            this.Raise(StreamStopped, streamExceptionEventArgs);
                            break;
                    }
                }
            }
        }

        public StreamResultGenerator(IExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }

        public void StartStream(Action<string> processObject, Func<HttpWebRequest> generateWebRequest)
        {
            Func<string, bool> processValidObject = json =>
            {
                processObject(json);
                return true;
            };

            StartStream(processValidObject, generateWebRequest);
        }

        public void StartStream(Func<string, bool> processObject, Func<HttpWebRequest> generateWebRequest)
        {
            if (IsRunning)
            {
                throw new OperationCanceledException(Resources.Stream_IllegalMultipleStreams);
            }

            if (processObject == null)
            {
                throw new NullReferenceException(Resources.Stream_ObjectDelegateIsNull);
            }

            _lastException = null;
            _streamState = StreamState.Resume;
            this.Raise(StreamStarted);

            HttpWebRequest webRequest = generateWebRequest();
            _currentReader = InitWebRequest(webRequest);

            if (_lastException != null)
            {
                _streamState = StreamState.Stop;
            }

            int errorOccured = 0;
            while (StreamState != StreamState.Stop)
            {
                if (StreamState == StreamState.Pause)
                {
                    Thread.Sleep(STREAM_RESUME_DELAY);
                    continue;
                }

                try
                {
                    string jsonResponse = _currentReader.ReadLine();

                    #region Error Checking

                    if (jsonResponse == null)
                    {
                        if (errorOccured == 0)
                        {
                            ++errorOccured;
                        }
                        else if (errorOccured == 1)
                        {
                            ++errorOccured;
                            webRequest.Abort();
                            _currentReader = InitWebRequest(webRequest);
                        }
                        else if (errorOccured == 2)
                        {
                            ++errorOccured;
                            webRequest.Abort();
                            webRequest = generateWebRequest();
                            _currentReader = InitWebRequest(webRequest);
                        }
                        else
                        {
                            Trace.WriteLine("Twitter API is not accessible");
                            break;
                        }
                    }
                    else
                    {
                        errorOccured = 0;
                    }

                    #endregion

                    if (jsonResponse == String.Empty)
                    {
                        continue;
                    }

                    if (StreamState == StreamState.Resume && !processObject(jsonResponse))
                    {
                        StreamState = StreamState.Stop;
                        break;
                    }
                }
                catch (WebException wex)
                {
                    _exceptionHandler.AddWebException(wex, String.Empty);
                }
                catch (Exception ex)
                {
                    _lastException = ex;

                    if (ex is IOException)
                    {
                        if (StreamState == StreamState.Stop)
                        {
                            return;
                        }

                        if (ex.Message == "Unable to read data from the transport connection: The connection was closed.")
                        {
                            _currentReader = InitWebRequest(webRequest);
                        }

                        try
                        {
                            _currentReader.ReadLine();
                            _lastException = null;
                        }
                        catch (IOException ex2)
                        {
                            if (ex2.Message == "Unable to read data from the transport connection: The connection was closed.")
                            {
                                Trace.WriteLine("Streamreader was unable to read from the stream!");
                            }
                        }
                        catch (ObjectDisposedException)
                        {
                            // StopStream has been called
                            _lastException = null;
                        }
                    }

                    break;
                }
            }

            if (webRequest != null)
            {
                webRequest.Abort();
            }

            if (_currentReader != null)
            {
                _currentReader.Dispose();
            }

            StreamState = StreamState.Stop;
        }

        private StreamReader InitWebRequest(WebRequest webRequest)
        {
            StreamReader reader = null;
            webRequest.Timeout = -1;

            try
            {
                HttpWebResponse webResponse = (HttpWebResponse) webRequest.GetResponse();
                Stream responseStream = webResponse.GetResponseStream();

                if (responseStream != null)
                {
                    reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                }
            }
            catch (WebException wex)
            {
                _exceptionHandler.AddWebException(wex, webRequest.RequestUri.AbsoluteUri);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    if (ex.Message == "Stream was not readable.")
                    {
                        webRequest.Abort();
                    }
                }

                _lastException = ex;
                StreamState = StreamState.Stop;
            }

            return reader;
        }

        public void ResumeStream()
        {
            StreamState = StreamState.Resume;
        }

        public void PauseStream()
        {
            StreamState = StreamState.Pause;
        }

        public void StopStream()
        {
            StreamState = StreamState.Stop;

            if (_currentReader != null)
            {
                _currentReader.Close();
            }
        }

        public void StopStream(Exception exception, IDisconnectMessage disconnectMessage = null)
        {
            _lastException = exception;
            _streamState = StreamState.Stop;
            var streamExceptionEventArgs = new StreamExceptionEventArgs(exception, disconnectMessage);
            this.Raise(StreamStopped, streamExceptionEventArgs);
        }
    }
}