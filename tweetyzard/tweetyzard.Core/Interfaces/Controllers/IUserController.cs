using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces.DTO;

namespace TweetinviCore.Interfaces.Controllers
{
    public interface IUserController
    {
        // Friends
        IEnumerable<long> GetFriendIds(IUser user, int maxFriendsToRetrieve = 5000);
        IEnumerable<long> GetFriendIds(IUserIdDTO userDTO, int maxFriendsToRetrieve = 5000);
        IEnumerable<long> GetFriendIds(long userId, int maxFriendsToRetrieve = 5000);
        IEnumerable<long> GetFriendIds(string userScreenName, int maxFriendsToRetrieve = 5000);
        
        IEnumerable<IUser> GetFriends(IUser user, int maxFriendsToRetrieve = 250);
        IEnumerable<IUser> GetFriends(IUserIdDTO userDTO, int maxFriendsToRetrieve = 250);
        IEnumerable<IUser> GetFriends(long userId, int maxFriendsToRetrieve = 250);
        IEnumerable<IUser> GetFriends(string userScreenName, int maxFriendsToRetrieve = 250);

        // Followers
        IEnumerable<long> GetFollowerIds(IUser user, int maxFollowersToRetrieve = 5000);
        IEnumerable<long> GetFollowerIds(IUserIdDTO userDTO, int maxFollowersToRetrieve = 5000);
        IEnumerable<long> GetFollowerIds(long userId, int maxFollowersToRetrieve = 5000);
        IEnumerable<long> GetFollowerIds(string userScreenName, int maxFollowersToRetrieve = 5000);

        IEnumerable<IUser> GetFollowers(IUser user, int maxFollowersToRetrieve = 250);
        IEnumerable<IUser> GetFollowers(IUserIdDTO userDTO, int maxFollowersToRetrieve = 250);
        IEnumerable<IUser> GetFollowers(long userId, int maxFollowersToRetrieve = 250);
        IEnumerable<IUser> GetFollowers(string userScreenName, int maxFollowersToRetrieve = 250);

        // Favourites
        IEnumerable<ITweet> GetFavouriteTweets(IUser user, int maxFavouritesToRetrieve = 40);
        IEnumerable<ITweet> GetFavouriteTweets(IUserIdDTO userDTO, int maxFavouritesToRetrieve = 40);
        IEnumerable<ITweet> GetFavouriteTweets(long userId, int maxFavouritesToRetrieve = 40);
        IEnumerable<ITweet> GetFavouriteTweets(string userScreenName, int maxFavouritesToRetrieve = 40);

        // Block User
        bool BlockUser(IUser user);
        bool BlockUser(IUserIdDTO userDTO);
        bool BlockUser(long userId);
        bool BlockUser(string userScreenName);

        // Get Local Image
        Bitmap GenerateProfileImageBitmap(IUser user, ImageSize imageSize = ImageSize.normal);
        Bitmap GenerateProfileImageBitmap(IUserDTO userDTO, ImageSize imageSize = ImageSize.normal);

        // Stream Profile Image
        Stream GenerateProfileImageStream(IUser user, ImageSize imageSize = ImageSize.normal);
        Stream GenerateProfileImageStream(IUserDTO userDTO, ImageSize imageSize = ImageSize.normal);

        // Download Profile Image
        bool DownloadProfileImage(IUser user, string filePath, ImageSize size = ImageSize.normal);
        bool DownloadProfileImage(IUserDTO userDTO, string filePath, ImageSize size = ImageSize.normal);

        bool DownloadProfileImageInHttp(IUser user, string filePath, ImageSize size = ImageSize.normal);
        bool DownloadProfileImageInHttp(IUserDTO userDTO, string filePath, ImageSize size = ImageSize.normal);

        void DownloadProfileImageAsync(
            IUser user,
            string filePath,
            Action<bool> successAction,
            Action<long, long> progressChangedAction = null,
            ImageSize imageSize = ImageSize.normal);

        void DownloadProfileImageAsync(
            IUserDTO userDTO,
            string filePath,
            Action<bool> successAction,
            Action<long, long> progressChangedAction = null,
            ImageSize imageSize = ImageSize.normal);
    }
}