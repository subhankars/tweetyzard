﻿using System;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.DTO;

namespace TweetinviCore.Events.EventArguments
{
    public class UserEventArgs : EventArgs
    {
        public UserEventArgs(IUser user)
        {
            User = user;
        }

        public IUser User { get; private set; }
    }

    public class LoggedUserUpdatedEventArgs : EventArgs
    {
        public LoggedUserUpdatedEventArgs(ILoggedUser loggedUser)
        {
            LoggedUser = loggedUser;
        }

        public ILoggedUser LoggedUser { get; private set; }
    }

    public class UserFollowedEventArgs : UserEventArgs
    {
        public UserFollowedEventArgs(IUser user) : base(user)
        {
        }
    }

    public class UserBlockedEventArgs : UserEventArgs
    {
        public UserBlockedEventArgs(IUser user) : base(user)
        {
        }
    }

    public class UserWitheldEventArgs : EventArgs
    {
        public UserWitheldEventArgs(IUserWitheldInfo userWitheldInfo)
        {
            UserWitheldInfo = userWitheldInfo;
        }

        public IUserWitheldInfo UserWitheldInfo { get; private set; }
    }
}