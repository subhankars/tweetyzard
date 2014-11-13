using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;

namespace TweetinviControllers.User
{
    /// <summary>
    /// Reason for change : Twitter changes the operation exposed on its REST API
    /// </summary>
    public class UserController : IUserController
    {
        private readonly IUserQueryExecutor _userQueryExecutor;
        private readonly ITweetFactory _tweetFactory;
        private readonly IUserFactory _userFactory;

        public UserController(
            IUserQueryExecutor userQueryExecutor,
            ITweetFactory tweetFactory,
            IUserFactory userFactory)
        {
            _userQueryExecutor = userQueryExecutor;
            _tweetFactory = tweetFactory;
            _userFactory = userFactory;
        }

        // Friend Ids
        public IEnumerable<long> GetFriendIds(IUser user, int maxFriendsToRetrieve = 5000)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }

            return GetFriendIds(user.UserDTO, maxFriendsToRetrieve);
        }

        public IEnumerable<long> GetFriendIds(IUserIdDTO userDTO, int maxFriendsToRetrieve = 5000)
        {
            return _userQueryExecutor.GetFriendIds(userDTO, maxFriendsToRetrieve);
        }

        public IEnumerable<long> GetFriendIds(long userId, int maxFriendsToRetrieve = 5000)
        {
            return _userQueryExecutor.GetFriendIds(userId, maxFriendsToRetrieve);
        }

        public IEnumerable<long> GetFriendIds(string userScreenName, int maxFriendsToRetrieve = 5000)
        {
            return _userQueryExecutor.GetFriendIds(userScreenName, maxFriendsToRetrieve);
        }

        // Friends
        public IEnumerable<IUser> GetFriends(IUser user, int maxFriendsToRetrieve = 250)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }

            return GetFriends(user.UserDTO, maxFriendsToRetrieve);
        }

        public IEnumerable<IUser> GetFriends(IUserIdDTO userDTO, int maxFriendsToRetrieve = 250)
        {
            var friendIds = GetFriendIds(userDTO, maxFriendsToRetrieve);
            return _userFactory.GetUsersFromIds(friendIds);
        }

        public IEnumerable<IUser> GetFriends(long userId, int maxFriendsToRetrieve = 250)
        {
            var friendIds = GetFriendIds(userId, maxFriendsToRetrieve);
            return _userFactory.GetUsersFromIds(friendIds);
        }

        public IEnumerable<IUser> GetFriends(string userScreenName, int maxFriendsToRetrieve = 250)
        {
            var friendIds = GetFriendIds(userScreenName, maxFriendsToRetrieve);
            return _userFactory.GetUsersFromIds(friendIds);
        }

        // Follower Ids
        public IEnumerable<long> GetFollowerIds(IUser user, int maxFollowersToRetrieve = 5000)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }

            return GetFollowerIds(user.UserDTO, maxFollowersToRetrieve);
        }

        public IEnumerable<long> GetFollowerIds(IUserIdDTO userDTO, int maxFollowersToRetrieve = 5000)
        {
            return _userQueryExecutor.GetFollowerIds(userDTO, maxFollowersToRetrieve);
        }

        public IEnumerable<long> GetFollowerIds(long userId, int maxFollowersToRetrieve = 5000)
        {
            return _userQueryExecutor.GetFollowerIds(userId, maxFollowersToRetrieve);
        }

        public IEnumerable<long> GetFollowerIds(string userScreenName, int maxFollowersToRetrieve = 5000)
        {
            return _userQueryExecutor.GetFollowerIds(userScreenName, maxFollowersToRetrieve);
        }

        // Followers
        public IEnumerable<IUser> GetFollowers(IUser user, int maxFollowersToRetrieve = 250)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }

            return GetFollowers(user.UserDTO, maxFollowersToRetrieve);
        }

        public IEnumerable<IUser> GetFollowers(IUserIdDTO userDTO, int maxFollowersToRetrieve = 250)
        {
            var followerIds = GetFollowerIds(userDTO, maxFollowersToRetrieve);
            return _userFactory.GetUsersFromIds(followerIds);
        }

        public IEnumerable<IUser> GetFollowers(long userId, int maxFollowersToRetrieve = 250)
        {
            var followerIds = GetFollowerIds(userId, maxFollowersToRetrieve);
            return _userFactory.GetUsersFromIds(followerIds);
        }

        public IEnumerable<IUser> GetFollowers(string userScreenName, int maxFollowersToRetrieve = 250)
        {
            var followerIds = GetFollowerIds(userScreenName, maxFollowersToRetrieve);
            return _userFactory.GetUsersFromIds(followerIds);
        }

        // Favourites
        public IEnumerable<ITweet> GetFavouriteTweets(IUser user, int maxFavouritesToRetrieve = 40)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }

            return GetFavouriteTweets(user.UserDTO, maxFavouritesToRetrieve);
        }

        public IEnumerable<ITweet> GetFavouriteTweets(IUserIdDTO userDTO, int maxFavouritesToRetrieve = 40)
        {
            var favoriteTweetsDTO = _userQueryExecutor.GetFavouriteTweets(userDTO, maxFavouritesToRetrieve);
            return _tweetFactory.GenerateTweetsFromDTO(favoriteTweetsDTO);
        }

        public IEnumerable<ITweet> GetFavouriteTweets(long userId, int maxFavouritesToRetrieve = 40)
        {
            var favoriteTweetsDTO = _userQueryExecutor.GetFavouriteTweets(userId, maxFavouritesToRetrieve);
            return _tweetFactory.GenerateTweetsFromDTO(favoriteTweetsDTO);
        }

        public IEnumerable<ITweet> GetFavouriteTweets(string userScreenName, int maxFavouritesToRetrieve = 40)
        {
            var favoriteTweetsDTO = _userQueryExecutor.GetFavouriteTweets(userScreenName, maxFavouritesToRetrieve);
            return _tweetFactory.GenerateTweetsFromDTO(favoriteTweetsDTO);
        }

        // Block User
        public bool BlockUser(IUser user)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }

            return BlockUser(user.UserDTO);
        }

        public bool BlockUser(IUserIdDTO userDTO)
        {
            return _userQueryExecutor.BlockUser(userDTO);
        }

        public bool BlockUser(long userId)
        {
            return _userQueryExecutor.BlockUser(userId);
        }

        public bool BlockUser(string userScreenName)
        {
            return _userQueryExecutor.BlockUser(userScreenName);
        }

        // Get Local Profile Image

        [ExcludeFromCodeCoverage]
        public Bitmap GenerateProfileImageBitmap(IUser user, ImageSize imageSize = ImageSize.normal)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }

            return GenerateProfileImageBitmap(user.UserDTO, imageSize);
        }

        [ExcludeFromCodeCoverage]
        public Bitmap GenerateProfileImageBitmap(IUserDTO userDTO, ImageSize imageSize = ImageSize.normal)
        {
            return _userQueryExecutor.GenerateProfileImageBitmap(userDTO, imageSize);
        }

        // Stream Profile Image
        public Stream GenerateProfileImageStream(IUser user, ImageSize imageSize = ImageSize.normal)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }

            return GenerateProfileImageStream(user.UserDTO, imageSize);
        }

        public Stream GenerateProfileImageStream(IUserDTO userDTO, ImageSize imageSize = ImageSize.normal)
        {
            return _userQueryExecutor.GenerateProfileImageStream(userDTO, imageSize);
        }

        // Download Profile Image
        public bool DownloadProfileImage(IUser user, string filePath, ImageSize imageSize = ImageSize.normal)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }

            return DownloadProfileImage(user.UserDTO, filePath, imageSize);
        }

        public bool DownloadProfileImage(IUserDTO userDTO, string filePath, ImageSize imageSize = ImageSize.normal)
        {
            return _userQueryExecutor.DownloadProfileImage(userDTO, filePath, imageSize);
        }

        public bool DownloadProfileImageInHttp(IUser user, string filePath, ImageSize imageSize = ImageSize.normal)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }

            return DownloadProfileImageInHttp(user.UserDTO, filePath, imageSize);
        }

        public bool DownloadProfileImageInHttp(IUserDTO userDTO, string filePath, ImageSize imageSize = ImageSize.normal)
        {
            return _userQueryExecutor.DownloadProfileImageInHttp(userDTO, filePath, imageSize);
        }

        public void DownloadProfileImageAsync(
            IUser user, 
            string filePath,
            Action<bool> successAction,
            Action<long, long> progressChangedAction = null,
            ImageSize imageSize = ImageSize.normal)
        {
            if (user == null)
            {
                if (successAction != null)
                {
                    successAction(false);
                }

                return;
            }

            DownloadProfileImageAsync(user.UserDTO, filePath, successAction, progressChangedAction, imageSize);
        }

        public void DownloadProfileImageAsync(
            IUserDTO userDTO, 
            string filePath,
            Action<bool> successAction,
            Action<long, long> progressChangedAction = null,
            ImageSize imageSize = ImageSize.normal)
        {
            _userQueryExecutor.DownloadProfileImageAsync(userDTO, filePath, successAction, progressChangedAction, imageSize);
        }
    }
}