using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TweetinviCore.Enum;
using TweetinviCore.Events;
using TweetinviCore.Events.EventArguments;
using TweetinviCore.Helpers;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.oAuth;
using TweetinviCore.Interfaces.Streaminvi;
using TweetinviCore.Wrappers;

namespace Streaminvi
{
    public class TrackedStream : TwitterStream, ITrackedStream
    {
        public event EventHandler<MatchedTweetReceivedEventArgs> MatchingTweetReceived;

        protected readonly IStreamTrackManager<ITweet> _streamTrackManager;
        protected readonly IJsonObjectConverter _jsonObjectConverter;
        protected readonly ITweetFactory _tweetFactory;
        protected readonly IOAuthToken _oAuthToken;

        public TrackedStream(
            IStreamTrackManager<ITweet> streamTrackManager,
            IJsonObjectConverter jsonObjectConverter,
            IJObjectStaticWrapper jObjectStaticWrapper,
            IStreamResultGenerator streamResultGenerator,
            ITweetFactory tweetFactory,
            IOAuthToken oAuthToken)

            : base(streamResultGenerator, jsonObjectConverter, jObjectStaticWrapper)
        {
            _streamTrackManager = streamTrackManager;
            _jsonObjectConverter = jsonObjectConverter;
            _tweetFactory = tweetFactory;
            _oAuthToken = oAuthToken;
        }

        public void StartStream(string url)
        {
            Func<HttpWebRequest> generateWebRequest = delegate
            {
                return _oAuthToken.GetQueryWebRequest(url, HttpMethod.GET);
            };

            Action<string> generateTweetDelegate = json =>
            {
                var tweet = _tweetFactory.GenerateTweetFromJson(json);
                if (tweet == null)
                {
                    TryInvokeGlobalStreamMessages(json);
                    return;
                }

                var detectedTracksAndActions = _streamTrackManager.GetMatchingTracksAndActions(tweet.Text);
                var detectedTracks = detectedTracksAndActions.Select(x => x.Item1);
                if (detectedTracksAndActions.Any())
                {
                    this.Raise(MatchingTweetReceived, new MatchedTweetReceivedEventArgs(tweet, detectedTracks));
                }
            };

            _streamResultGenerator.StartStream(generateTweetDelegate, generateWebRequest);
        }

        public int TracksCount
        {
            get { return _streamTrackManager.TracksCount; }
        }

        public int MaxTracks
        {
            get { return _streamTrackManager.MaxTracks; }
        }

        public Dictionary<string, Action<ITweet>> Tracks
        {
            get { return _streamTrackManager.Tracks; }
        }

        public void AddTrack(string track, Action<ITweet> trackReceived = null)
        {
            _streamTrackManager.AddTrack(track, trackReceived);
        }

        public void RemoveTrack(string track)
        {
            _streamTrackManager.RemoveTrack(track);
        }

        public bool ContainsTrack(string track)
        {
            return _streamTrackManager.ContainsTrack(track);
        }

        public void ClearTracks()
        {
            _streamTrackManager.ClearTracks();
        }

        protected void RaiseMatchingTweetReceived(MatchedTweetReceivedEventArgs eventArgs)
        {
            this.Raise(MatchingTweetReceived, eventArgs);
        }
    }
}