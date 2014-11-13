using System;
using System.Net;
using TweetinviCore.Enum;
using TweetinviCore.Events;
using TweetinviCore.Events.EventArguments;
using TweetinviCore.Helpers;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.oAuth;
using TweetinviCore.Interfaces.Streaminvi;
using TweetinviCore.Wrappers;

namespace Streaminvi
{
    public class TweetStream : TwitterStream, ITweetStream
    {
        private readonly ITweetFactory _tweetFactory;
        private readonly IOAuthToken _oAuthToken;

        public event EventHandler<TweetReceivedEventArgs> TweetReceived;

        public TweetStream(
            IStreamResultGenerator streamResultGenerator,
            IJsonObjectConverter jsonObjectConverter,
            IJObjectStaticWrapper jObjectStaticWrapper,
            ITweetFactory tweetFactory,
            IOAuthToken oAuthToken)
            : base(streamResultGenerator, jsonObjectConverter, jObjectStaticWrapper)
        {
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

                this.Raise(TweetReceived, new TweetReceivedEventArgs(tweet));
            };

            _streamResultGenerator.StartStream(generateTweetDelegate, generateWebRequest);
        }
    }
}