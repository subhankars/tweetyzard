using System;
using TweetinviCore.Enum;
using TweetinviCore.Events;
using TweetinviCore.Events.EventArguments;

namespace TweetinviCore.Interfaces.Streaminvi
{
    public interface ITwitterStream
    {
        event EventHandler StreamStarted;
        event EventHandler StreamResumed;
        event EventHandler StreamPaused;
        event EventHandler<StreamExceptionEventArgs> StreamStopped;

        event EventHandler<TweetDeletedEventArgs> TweetDeleted;
        event EventHandler<TweetLocationDeletedEventArgs> TweetLocationInfoRemoved;
        event EventHandler<DisconnectMessageEventArgs> DisconnectMessageReceived;
        event EventHandler<TweetWitheldEventArgs> TweetWitheld;
        event EventHandler<UserWitheldEventArgs> UserWitheld;

        event EventHandler<LimitReachedEventArgs> LimitReached;
        event EventHandler<WarningFallingBehindEventArgs> WarningFallingBehindDetected;
        event EventHandler<UnmanagedMessageReceivedEventArgs> UnmanagedEventReceived;

        /// <summary>
        /// Get the current state of the stream
        /// </summary>
        StreamState StreamState { get; }

        /// <summary>
        /// Resume a stopped Stream
        /// </summary>
        void ResumeStream();

        /// <summary>
        /// Pause a running Stream
        /// </summary>
        void PauseStream();

        /// <summary>
        /// Stop a running or paused stream
        /// </summary>
        void StopStream();
    }
}