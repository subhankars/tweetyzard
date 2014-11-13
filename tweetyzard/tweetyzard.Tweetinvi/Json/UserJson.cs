using System;
using System.Collections.Generic;
using TweetinviControllers.User;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.DTO;

namespace Tweetinvi.Json
{
    public static class UserJson
    {
        [ThreadStatic]
        private static IUserJsonController _userJsonController;

        static UserJson()
        {
            Initialize();
        }

        public static IUserJsonController UserJsonController
        {
            get
            {
                if (_userJsonController == null)
                {
                    Initialize();
                }
                
                return _userJsonController;
            }
        }

        private static void Initialize()
        {
            _userJsonController = TweetinviContainer.Resolve<IUserJsonController>();
        }

        // Friends
        public static IEnumerable<string> GetFriendIds(IUser user, int maxFriendsToRetrieve = 5000)
        {
            return UserJsonController.GetFriendIds(user, maxFriendsToRetrieve);
        }

        public static IEnumerable<string> GetFriendIds(IUserIdDTO userDTO, int maxFriendsToRetrieve = 5000)
        {
            return UserJsonController.GetFriendIds(userDTO, maxFriendsToRetrieve);
        }

        public static IEnumerable<string> GetFriendIds(long userId, int maxFriendsToRetrieve = 5000)
        {
            return UserJsonController.GetFriendIds(userId, maxFriendsToRetrieve);
        }

        public static IEnumerable<string> GetFriendIds(string userScreenName, int maxFriendsToRetrieve = 5000)
        {
            return UserJsonController.GetFriendIds(userScreenName, maxFriendsToRetrieve);
        }

        // Followers
        public static IEnumerable<string> GetFollowerIds(IUser user, int maxFollowersToRetrieve = 5000)
        {
            return UserJsonController.GetFollowerIds(user, maxFollowersToRetrieve);
        }

        public static IEnumerable<string> GetFollowerIds(IUserIdDTO userDTO, int maxFollowersToRetrieve = 5000)
        {
            return UserJsonController.GetFollowerIds(userDTO, maxFollowersToRetrieve);
        }

        public static IEnumerable<string> GetFollowerIds(long userId, int maxFollowersToRetrieve = 5000)
        {
            return UserJsonController.GetFollowerIds(userId, maxFollowersToRetrieve);
        }

        public static IEnumerable<string> GetFollowerIds(string userScreenName, int maxFollowersToRetrieve = 5000)
        {
            return UserJsonController.GetFollowerIds(userScreenName, maxFollowersToRetrieve);
        }

        // Favorites
        public static string GetFavouriteTweets(IUser user, int maxFavouritesToRetrieve = 40)
        {
            return UserJsonController.GetFavouriteTweets(user, maxFavouritesToRetrieve);
        }

        public static string GetFavouriteList(IUserIdDTO userDTO, int maxFavouritesToRetrieve = 40)
        {
            return UserJsonController.GetFavouriteTweets(userDTO, maxFavouritesToRetrieve);
        }

        public static string GetFavouriteList(long userId, int maxFavouritesToRetrieve = 40)
        {
            return UserJsonController.GetFavouriteTweets(userId, maxFavouritesToRetrieve);
        }

        public static string GetFavouriteList(string userScreenName, int maxFavouritesToRetrieve = 40)
        {
            return UserJsonController.GetFavouriteTweets(userScreenName, maxFavouritesToRetrieve);
        }
    }
}
