using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces.DTO;

namespace TweetinviCore.Interfaces
{
    /// <summary>
    /// Contract defining what a user on twitter can do
    /// </summary>
    public interface IUser : IEquatable<IUser>
    {
        IUserDTO UserDTO { get; set; }

        #region Twitter API Fields

        long Id { get; }

        string IdStr { get; }

        string Name { get; }

        string ScreenName { get; }

        string Description { get; }

        ITweetDTO Status { get; }

        string Location { get; }

        bool GeoEnabled { get; }

        string Url { get; }

        Language Language { get; }

        int StatusesCount { get; }

        int FollowersCount { get; }

        int FriendsCount { get; }

        bool Following { get; }

        bool Protected { get; }

        bool Verified { get; }

        bool Notifications { get; }

        string ProfileImageUrl { get; }

        string ProfileImageUrlHttps { get; }

        bool FollowRequestSent { get; }

        bool DefaultProfile { get; }

        bool DefaultProfileImage { get; }

        int FavouritesCount { get; }

        int ListedCount { get; }

        string ProfileSidebarFillColor { get; }

        string ProfileSidebarBorderColor { get; }

        string ProfileBackgroundTitle { get; }

        string ProfileBackgroundColor { get; }

        string ProfileBackgroundImageUrl { get; }

        string ProfileBackgroundImageUrlHttps { get; }

        string ProfileTextColor { get; }

        string ProfileLinkColor { get; }

        bool ProfileUseBackgroundImage { get; }

        bool IsTranslator { get; }

        bool ShowAllInlineMedia { get; }

        bool ContributorsEnabled { get; }

        int? UtcOffset { get; }

        string TimeZone { get; }

        #endregion

        #region Tweetinvi API Fields

        /// <summary>
        /// List of friend Ids
        /// </summary>
        List<long> FriendIds { get; set; }

        /// <summary>
        /// List of friends with their profile information
        /// Requires a query per friend user
        /// </summary>
        List<IUser> Friends { get; }

        /// <summary>
        /// List of follower ids
        /// </summary>
        List<long> FollowerIds { get; set; }

        /// <summary>
        /// List of followers with their profile information
        /// Requires a query per friend user
        /// </summary>
        List<IUser> Followers { get; }

        /// <summary>
        /// List of contributors of the account
        /// </summary>
        List<IUser> Contributors { get; set; }

        /// <summary>
        /// List of the account the user is contributing to
        /// </summary>
        List<IUser> Contributees { get; set; }

        /// <summary>
        /// List of tweets as displayed on the timeline
        /// </summary>
        List<ITweet> Timeline { get; set; }

        /// <summary>
        /// List of tweets that actually are retweets
        /// </summary>
        List<ITweet> Retweets { get; set; }

        /// <summary>
        /// List of retweets from friends
        /// </summary>
        List<ITweet> FriendsRetweets { get; set; }

        /// <summary>
        /// Tweets the user created that have been retweeted by followers
        /// </summary>
        List<ITweet> TweetsRetweetedByFollowers { get; set; }

        #endregion

        // Friends
        IEnumerable<long> GetFriendIds(int maxFriendsToRetrieve = 5000);
        IEnumerable<IUser> GetFriends(int maxFriendsToRetrieve = 250);

        // Followers
        IEnumerable<long> GetFollowerIds(int maxFriendsToRetrieve = 5000);
        IEnumerable<IUser> GetFollowers(int maxFriendsToRetrieve = 250);

        // Friendship
        IRelationship GetRelationshipWith(IUser user);

        // Timeline
        IEnumerable<ITweet> GetUserTimeline(int maximumTweet = 20, bool excludeReplies = false);

        // Get Favorites
        IEnumerable<ITweet> GetFavorites(int maximumTweets = 40);

        // Block
        bool Block();

        // Get Local Profile Image
        Bitmap GenerateProfileImageBitmap(ImageSize imageSize = ImageSize.normal);

        // Stream Profile Image
        Stream GenerateProfileImageStream(ImageSize imageSize = ImageSize.normal);

        // Download Profile Image
        /// <summary>
        /// Download the user profile image
        /// </summary>
        bool DownloadProfileImage(string filePath, ImageSize imageSize = ImageSize.normal);

        void DownloadProfileImageAsync(
            string filePath,
            Action<bool> successAction,
            Action<long, long> progressChangedAction = null,
            ImageSize imageSize = ImageSize.normal);

        #region Contributors

        /// <summary>
        /// Get the list of contributors to the account of the current user
        /// Update the matching attribute of the current user if the parameter is true
        /// Return the list of contributors
        /// </summary>
        /// <param name="createContributorList">False by default. Indicates if the _contributors attribute needs to be updated with the result</param>
        /// <returns>The list of contributors to the account of the current user</returns>
        IEnumerable<IUser> GetContributors(bool createContributorList = false);

        /// <summary>
        /// Get the list of accounts the current user is allowed to update
        /// Update the matching attribute of the current user if the parameter is true
        /// Return the list of contributees
        /// </summary>
        /// <param name="createContributeeList">False by default. Indicates if the _contributees attribute needs to be updated with the result</param>
        /// <returns>The list of accounts the current user is allowed to update</returns>
        IEnumerable<IUser> GetContributees(bool createContributeeList = false);

        #endregion
    }
}