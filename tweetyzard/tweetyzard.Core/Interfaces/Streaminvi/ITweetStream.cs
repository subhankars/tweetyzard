using System;
using TweetinviCore.Events.EventArguments;

namespace TweetinviCore.Interfaces.Streaminvi
{
    public interface ITweetStream : ITwitterStream
    {
        event EventHandler<TweetReceivedEventArgs> TweetReceived;
        void StartStream(string url);
    }
}