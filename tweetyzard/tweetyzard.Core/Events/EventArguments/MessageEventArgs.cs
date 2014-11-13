using System;
using TweetinviCore.Interfaces;

namespace TweetinviCore.Events.EventArguments
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(IMessage message)
        {
            Message = message;
        }

        public IMessage Message { get; private set; }
    }
}