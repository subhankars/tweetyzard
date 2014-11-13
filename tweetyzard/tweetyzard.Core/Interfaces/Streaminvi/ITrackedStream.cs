using System;
using TweetinviCore.Events.EventArguments;

namespace TweetinviCore.Interfaces.Streaminvi
{
    public interface ITrackedStream : ITwitterStream, ITrackableStream<ITweet>
    {
        event EventHandler<MatchedTweetReceivedEventArgs> MatchingTweetReceived;
        void StartStream(string url);
    }
}