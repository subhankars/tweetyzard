using System;
using System.Collections.Generic;
using System.Drawing;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.oAuth;

namespace Tweetinvi
{
    public static class User
    {
        [ThreadStatic]
        private static IUserFactory _userFactory;
        public static IUserFactory UserFactory
        {
            get
            {
                if (_userFactory == null)
                {
                    Initialize();
                }

                return _userFactory;
            }
        }

        [ThreadStatic]
        private static IUserController _userController;
        public static IUserController UserController
        {
            get
            {
                if (_userController == null)
                {
                    Initialize();
                }

                return _userController;
            }
        }

        static User()
        {
            Initialize();
        }

        private static void Initialize()
        {
            _userController = TweetinviContainer.Resolve<IUserController>();
            _userFactory = TweetinviContainer.Resolve<IUserFactory>();
        }

        #region User Factory

        public static ILoggedUser GetLoggedUser()
        {
            return _userFactory.GetLoggedUser();
        }

        public static ILoggedUser GetLoggedUser(IOAuthCredentials credentials)
        {
            return _userFactory.GetLoggedUser(credentials);
        }

        public static IUser GetUserFromId(long userId)
        {
            return _userFactory.GetUserFromId(userId);
        }

        public static IUser GetUserFromScreenName(string userName)
        {
            return _userFactory.GetUserFromScreenName(userName);
        }

        public static IUser GenerateUserFromDTO(IUserDTO userDTO)
        {
            return _userFactory.GenerateUserFromDTO(userDTO);
        }

        public static IEnumerable<IUser> GenerateUsersFromDTO(IEnumerable<IUserDTO> usersDTO)
        {
            return _userFactory.GenerateUsersFromDTO(usersDTO);
        }

        #endregion

        #region User Controller

        // Friend Ids
        public static IEnumerable<long> GetFriendIds(IUser user, int maxFriendsToRetrieve = 5000)
        {
            return UserController.GetFriendIds(user, maxFriendsToRetrieve);
        }

        public static IEnumerable<long> GetFriendIds(IUserIdDTO userDTO, int maxFriendsToRetrieve = 5000)
        {
            return UserController.GetFriendIds(userDTO, maxFriendsToRetrieve);
        }

        public static IEnumerable<long> GetFriendIds(long userId, int maxFriendsToRetrieve = 5000)
        {
            return UserController.GetFriendIds(userId, maxFriendsToRetrieve);
        }

        public static IEnumerable<long> GetFriendIds(string userScreenName, int maxFriendsToRetrieve = 5000)
        {
            return UserController.GetFriendIds(userScreenName, maxFriendsToRetrieve);
        }

        // Friends
        public static IEnumerable<IUser> GetFriends(IUser user, int maxFriendsToRetrieve = 250)
        {
            return UserController.GetFriends(user, maxFriendsToRetrieve);
        }

        public static IEnumerable<IUser> GetFriends(IUserIdDTO userDTO, int maxFriendsToRetrieve = 250)
        {
            return UserController.GetFriends(userDTO, maxFriendsToRetrieve);
        }

        public static IEnumerable<IUser> GetFriends(long userId, int maxFriendsToRetrieve = 250)
        {
            return UserController.GetFriends(userId, maxFriendsToRetrieve);
        }

        public static IEnumerable<IUser> GetFriends(string userScreenName, int maxFriendsToRetrieve = 250)
        {
            return UserController.GetFriends(userScreenName, maxFriendsToRetrieve);
        }

        // Follower Ids
        public static IEnumerable<long> GetFollowerIds(IUser user, int maxFollowersToRetrieve = 5000)
        {
            return UserController.GetFollowerIds(user, maxFollowersToRetrieve);
        }

        public static IEnumerable<long> GetFollowerIds(IUserIdDTO userDTO, int maxFollowersToRetrieve = 5000)
        {
            return UserController.GetFollowerIds(userDTO, maxFollowersToRetrieve);
        }

        public static IEnumerable<long> GetFollowerIds(long userId, int maxFollowersToRetrieve = 5000)
        {
            return UserController.GetFollowerIds(userId, maxFollowersToRetrieve);
        }

        public static IEnumerable<long> GetFollowerIds(string userScreenName, int maxFollowersToRetrieve = 5000)
        {
            return UserController.GetFollowerIds(userScreenName, maxFollowersToRetrieve);
        }

        // Followers
        public static IEnumerable<IUser> GetFollowers(IUser user, int maxFollowersToRetrieve = 250)
        {
            return UserController.GetFollowers(user, maxFollowersToRetrieve);
        }

        public static IEnumerable<IUser> GetFollowers(IUserIdDTO userDTO, int maxFollowersToRetrieve = 250)
        {
            return UserController.GetFollowers(userDTO, maxFollowersToRetrieve);
        }

        public static IEnumerable<IUser> GetFollowers(long userId, int maxFollowersToRetrieve = 250)
        {
            return UserController.GetFollowers(userId, maxFollowersToRetrieve);
        }

        public static IEnumerable<IUser> GetFollowers(string userScreenName, int maxFollowersToRetrieve = 250)
        {
            return UserController.GetFollowers(userScreenName, maxFollowersToRetrieve);
        }

        // Favourites
        public static IEnumerable<ITweet> GetFavouriteTweets(IUser user, int maxFavouriteTweetsToRetrieve = 40)
        {
            return UserController.GetFavouriteTweets(user, maxFavouriteTweetsToRetrieve);
        }

        public static IEnumerable<ITweet> GetFavouriteTweets(IUserIdDTO userDTO, int maxFavouriteTweetsToRetrieve = 40)
        {
            return UserController.GetFavouriteTweets(userDTO, maxFavouriteTweetsToRetrieve);
        }

        public static IEnumerable<ITweet> GetFavouriteTweets(long userId, int maxFavouriteTweetsToRetrieve = 40)
        {
            return UserController.GetFavouriteTweets(userId, maxFavouriteTweetsToRetrieve);
        }

        public static IEnumerable<ITweet> GetFavouriteTweets(string userScreenName, int maxFavouriteTweetsToRetrieve = 40)
        {
            return UserController.GetFavouriteTweets(userScreenName, maxFavouriteTweetsToRetrieve);
        }

        // Block User
        public static bool BlockUser(IUser user)
        {
            return _userController.BlockUser(user);
        }

        public static bool BlockUser(IUserIdDTO userDTO)
        {
            return _userController.BlockUser(userDTO);
        }

        public static bool BlockUser(long userId)
        {
            return _userController.BlockUser(userId);
        }

        public static bool BlockUser(string userScreenName)
        {
            return _userController.BlockUser(userScreenName);
        }

        // Get Local Image
        public static Bitmap CreateProfileImageBitmap(IUser user, ImageSize imageSize = ImageSize.normal)
        {
            return _userController.GenerateProfileImageBitmap(user, imageSize);
        }

        public static Bitmap CreateProfileImageBitmap(IUserDTO userDTO, ImageSize imageSize = ImageSize.normal)
        {
            return _userController.GenerateProfileImageBitmap(userDTO, imageSize);
        }

        // Stream Profile Image 
        public static System.IO.Stream GetProfileImageStream(IUser user, ImageSize imageSize = ImageSize.normal)
        {
            return _userController.GenerateProfileImageStream(user, imageSize);
        }

        public static System.IO.Stream GetProfileImageStream(IUserDTO userDTO, ImageSize imageSize = ImageSize.normal)
        {
            return _userController.GenerateProfileImageStream(userDTO, imageSize);
        }

        // Download Profile Image
        public static bool DownloadProfileImage(IUser user, string filePath, ImageSize imageSize = ImageSize.normal)
        {
            return _userController.DownloadProfileImage(user, filePath, imageSize);
        }

        public static bool DownloadProfileImage(IUserDTO userDTO, string filePath, ImageSize imageSize = ImageSize.normal)
        {
            return _userController.DownloadProfileImage(userDTO, filePath, imageSize);
        }

        public static bool DownloadProfileImageInHttp(IUser user, string filePath, ImageSize imageSize = ImageSize.normal)
        {
            return _userController.DownloadProfileImageInHttp(user, filePath, imageSize);
        }

        public static bool DownloadProfileImageInHttp(IUserDTO userDTO, string filePath, ImageSize imageSize = ImageSize.normal)
        {
            return _userController.DownloadProfileImageInHttp(userDTO, filePath, imageSize);
        }

        public static void DownloadProfileImageAsync(
            IUser user,
            string filePath,
            Action<bool> successAction,
            Action<long, long> progressChangedAction = null,
            ImageSize imageSize = ImageSize.normal)
        {
            _userController.DownloadProfileImageAsync(user, filePath, successAction, progressChangedAction, imageSize);
        }

        public static void DownloadProfileImageAsync(
            IUserDTO userDTO,
            string filePath,
            Action<bool> successAction,
            Action<long, long> progressChangedAction = null,
            ImageSize imageSize = ImageSize.normal)
        {
            _userController.DownloadProfileImageAsync(userDTO, filePath, successAction, progressChangedAction, imageSize);
        }

        #endregion
    }
}