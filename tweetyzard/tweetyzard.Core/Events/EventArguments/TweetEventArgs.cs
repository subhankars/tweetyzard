using System;
using System.Collections.Generic;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Models;

namespace TweetinviCore.Events.EventArguments
{
    public class TweetEventArgs : EventArgs
    {
        public TweetEventArgs(ITweet tweet)
        {
            Tweet = tweet;
        }

        public ITweet Tweet { get; private set; }
    }

    public class TweetReceivedEventArgs : TweetEventArgs
    {
        public TweetReceivedEventArgs(ITweet tweet) : base(tweet)
        {
        }
    }

    public class TweetFavouritedEventArgs : TweetEventArgs
    {
        public TweetFavouritedEventArgs(ITweet tweet, IUser favoritingUser) : base(tweet)
        {
            FavouritingUser = favoritingUser;
        }

        public IUser FavouritingUser { get; private set; }
    }

    public class MatchedTweetReceivedEventArgs : TweetEventArgs
    {
        public MatchedTweetReceivedEventArgs(ITweet tweet, IEnumerable<string> matchingTracks) : base(tweet)
        {
            MatchingTracks = matchingTracks;
        }

        public IEnumerable<string> MatchingTracks { get; private set; }
    }

    public class MatchedTweetAndLocationReceivedEventArgs : MatchedTweetReceivedEventArgs
    {
        public MatchedTweetAndLocationReceivedEventArgs(ITweet tweet, IEnumerable<string> matchingTracks, IEnumerable<ILocation> matchingLocations)
            : base(tweet, matchingTracks)
        {
            MatchedLocations = matchingLocations;
        }

        public IEnumerable<ILocation> MatchedLocations { get; private set; }
    }

    public class TweetDeletedEventArgs : EventArgs
    {
        public TweetDeletedEventArgs(ITweetDeletedInfo tweetDeletedInfo)
        {
            TweetDeletedInfo = tweetDeletedInfo;
        }

        public ITweetDeletedInfo TweetDeletedInfo { get; private set; }
    }

    public class TweetLocationDeletedEventArgs : EventArgs
    {
        public TweetLocationDeletedEventArgs(ITweetLocationRemovedInfo tweetLocationRemovedInfo)
        {
            TweetLocationRemovedInfo = tweetLocationRemovedInfo;
        }

        public ITweetLocationRemovedInfo TweetLocationRemovedInfo { get; private set; }
    }

    public class TweetWitheldEventArgs : EventArgs
    {
        public TweetWitheldEventArgs(ITweetWitheldInfo tweetWitheldInfo)
        {
            TweetWitheldInfo = tweetWitheldInfo;
        }

        public ITweetWitheldInfo TweetWitheldInfo { get; private set; }
    }
}