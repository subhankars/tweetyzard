using System;
using TweetinviCore.Interfaces.DTO;

namespace TweetinviCore.Events.EventArguments
{
    public class DisconnectMessageEventArgs : EventArgs
    {
        public DisconnectMessageEventArgs(IDisconnectMessage disconnectMessage)
        {
            DisconnectMessage = disconnectMessage;
        }

        public IDisconnectMessage DisconnectMessage { get; private set; }
    }
}