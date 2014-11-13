using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Streaminvi.Model;
using TweetinviCore.Enum;
using TweetinviCore.Events;
using TweetinviCore.Events.EventArguments;
using TweetinviCore.Helpers;
using TweetinviCore.Interfaces.Streaminvi;
using TweetinviCore.Wrappers;

namespace Streaminvi
{
    public class TwitterStream : ITwitterStream
    {
        protected readonly IStreamResultGenerator _streamResultGenerator;
        private readonly IJsonObjectConverter _jsonObjectConverter;
        private readonly IJObjectStaticWrapper _jObjectWrapper;

        private readonly SortedDictionary<string, Action<JToken>> _streamEventsActions;

        public TwitterStream(
            IStreamResultGenerator streamResultGenerator,
            IJsonObjectConverter jsonObjectConverter,
            IJObjectStaticWrapper jObjectWrapper)
        {
            _streamResultGenerator = streamResultGenerator;
            _jsonObjectConverter = jsonObjectConverter;
            _jObjectWrapper = jObjectWrapper;
            _streamEventsActions = new SortedDictionary<string, Action<JToken>>();

            InitializeStreamEventsActions();
        }

        private void InitializeStreamEventsActions()
        {
            _streamEventsActions.Add("delete", TryRaiseTweetDeleted);
            _streamEventsActions.Add("scrub_geo", TryRaiseTweetLocationRemoved);
            _streamEventsActions.Add("disconnect", TryRaiseDisconnectMessageReceived);
            _streamEventsActions.Add("limit", TryRaiseLimitReached);
            _streamEventsActions.Add("status_withheld", TryRaiseTweetWitheld);
            _streamEventsActions.Add("user_withheld", TryRaiseUserWitheld);
            _streamEventsActions.Add("warning", TryRaiseWarning);
        }

        public event EventHandler StreamStarted
        {
            add { _streamResultGenerator.StreamStarted += value; }
            remove { _streamResultGenerator.StreamStarted -= value; }
        }
        public event EventHandler StreamResumed
        {
            add { _streamResultGenerator.StreamResumed += value; }
            remove { _streamResultGenerator.StreamResumed -= value; }
        }
        public event EventHandler StreamPaused
        {
            add { _streamResultGenerator.StreamPaused += value; }
            remove { _streamResultGenerator.StreamPaused -= value; }
        }
        public event EventHandler<StreamExceptionEventArgs> StreamStopped
        {
            add { _streamResultGenerator.StreamStopped += value; }
            remove { _streamResultGenerator.StreamStopped -= value; }
        }

        public event EventHandler<TweetDeletedEventArgs> TweetDeleted;
        public event EventHandler<TweetLocationDeletedEventArgs> TweetLocationInfoRemoved;
        public event EventHandler<DisconnectMessageEventArgs> DisconnectMessageReceived;
        public event EventHandler<TweetWitheldEventArgs> TweetWitheld;
        public event EventHandler<UserWitheldEventArgs> UserWitheld;

        public event EventHandler<LimitReachedEventArgs> LimitReached;
        public event EventHandler<WarningFallingBehindEventArgs> WarningFallingBehindDetected;
        public event EventHandler<UnmanagedMessageReceivedEventArgs> UnmanagedEventReceived;

        public StreamState StreamState
        {
            get { return _streamResultGenerator.StreamState; }
        }

        public void ResumeStream()
        {
            _streamResultGenerator.ResumeStream();
        }

        public void PauseStream()
        {
            _streamResultGenerator.PauseStream();
        }

        public void StopStream()
        {
            _streamResultGenerator.StopStream();
        }

        protected void StopStream(Exception ex)
        {
            _streamResultGenerator.StopStream(ex);
        }

        protected void TryInvokeGlobalStreamMessages(string json)
        {
            if (String.IsNullOrEmpty(json))
            {
                return;
            }

            var jsonObject = _jObjectWrapper.GetJobjectFromJson(json);
            var jsonRootToken = jsonObject.Children().First();
            var messageType = _jObjectWrapper.GetNodeRootName(jsonRootToken);

            if (_streamEventsActions.ContainsKey(messageType))
            {
                var messageInfo = jsonObject[messageType];
                _streamEventsActions[messageType].Invoke(messageInfo);
            }
            else
            {
                var unmanagedMessageEventArgs = new UnmanagedMessageReceivedEventArgs(json);
                this.Raise(UnmanagedEventReceived, unmanagedMessageEventArgs);
            }
        }

        private void TryRaiseTweetDeleted(JToken jToken)
        {
            jToken = jToken["status"];
            if (jToken == null)
            {
                return;
            }

            var deletedTweetInfo = _jsonObjectConverter.DeserializeObject<TweetDeletedInfo>(jToken.ToString());
            var deletedTweetEventArgs = new TweetDeletedEventArgs(deletedTweetInfo);
            this.Raise(TweetDeleted, deletedTweetEventArgs);
        }

        private void TryRaiseTweetLocationRemoved(JToken jToken)
        {
            var tweetLocationDeleted = _jsonObjectConverter.DeserializeObject<TweetLocationRemovedInfo>(jToken.ToString());
            var tweetLocationDeletedEventArgs = new TweetLocationDeletedEventArgs(tweetLocationDeleted);
            this.Raise(TweetLocationInfoRemoved, tweetLocationDeletedEventArgs);
        }

        private void TryRaiseDisconnectMessageReceived(JToken jToken)
        {
            var disconnectMessage = _jsonObjectConverter.DeserializeObject<DisconnectMessage>(jToken.ToString());
            var disconnectMessageEventArgs = new DisconnectMessageEventArgs(disconnectMessage);
            this.Raise(DisconnectMessageReceived, disconnectMessageEventArgs);
            _streamResultGenerator.StopStream(null, disconnectMessage);
        }

        private void TryRaiseLimitReached(JToken jToken)
        {
            var nbTweetsMissed = jToken.Value<int>("track");
            this.Raise(LimitReached, new LimitReachedEventArgs(nbTweetsMissed));
        }

        private void TryRaiseTweetWitheld(JToken jToken)
        {
            var info = _jsonObjectConverter.DeserializeObject<TweetWitheldInfo>(jToken.ToString());
            var eventArgs = new TweetWitheldEventArgs(info);
            this.Raise(TweetWitheld, eventArgs);
        }

        private void TryRaiseUserWitheld(JToken jToken)
        {
            var info = _jsonObjectConverter.DeserializeObject<UserWitheldInfo>(jToken.ToString());
            var eventArgs = new UserWitheldEventArgs(info);
            this.Raise(UserWitheld, eventArgs);
        }

        private void TryRaiseWarning(JToken jToken)
        {
            TryRaiseFallingBehindWarning(jToken);
        }

        private void TryRaiseFallingBehindWarning(JToken jsonWarning)
        {
            if (jsonWarning["percent_full"] != null)
            {
                var warningMessage = _jsonObjectConverter.DeserializeObject<WarningMessageFallingBehind>(jsonWarning.ToString());
                this.Raise(WarningFallingBehindDetected, new WarningFallingBehindEventArgs(warningMessage));
            }
        }
    }
}