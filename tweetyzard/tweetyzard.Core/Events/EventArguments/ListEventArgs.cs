using System;
using TweetinviCore.Interfaces;

namespace TweetinviCore.Events.EventArguments
{
    public class ListEventArgs : EventArgs
    {
        public ListEventArgs(ITweetList list)
        {
            List = list;
        }

        public ITweetList List { get; private set; }
    }

    public class ListUserUpdatedEventArgs : ListEventArgs
    {
        public ListUserUpdatedEventArgs(ITweetList list, IUser user)
            : base(list)
        {
            User = user;
        }

        public IUser User { get; private set; }
    }
}