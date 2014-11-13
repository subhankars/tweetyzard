using System;
using TweetinviCore.Events.EventArguments;

namespace TweetinviCore.Interfaces.Streaminvi
{
    public interface ISampleStream : ITwitterStream
    {
        event EventHandler<TweetReceivedEventArgs> TweetReceived;
        void StartStream();
    }
}