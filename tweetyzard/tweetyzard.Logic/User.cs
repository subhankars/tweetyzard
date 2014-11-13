using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Microsoft.Practices.Unity;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;

namespace TweetinviLogic
{
    /// <summary>
    /// Provide information and functions that a user can do
    /// </summary>
    public class User : IUser
    {
        protected IUserDTO _userDTO;
        protected readonly ITimelineController _timelineController;
        protected readonly IUserController _userController;
        protected readonly IFriendshipFactory _friendshipFactory;

        public IUserDTO UserDTO
        {
            get { return _userDTO; }
            set { _userDTO = value; }
        }

        #region Public Attributes

        #region Twitter API Attributes

        // This region represents the information accessible from a Twitter API
        // when querying for a User

        public long Id
        {
            get { return _userDTO.Id; }
        }

        public string IdStr
        {
            get { return _userDTO.IdStr; }
        }

        public string Name
        {
            get { return _userDTO.Name; }
        }

        public string ScreenName
        {
            get { return _userDTO.ScreenName; }
        }

        public string Description
        {
            get { return _userDTO.Description; }
        }

        public ITweetDTO Status
        {
            get { return _userDTO.Status; }
        }

        public string Location
        {
            get { return _userDTO.Location; }
        }

        public bool GeoEnabled
        {
            get { return _userDTO.GeoEnabled; }
        }

        public string Url
        {
            get { return _userDTO.Url; }
        }

        public Language Language
        {
            get { return _userDTO.Language; }
        }

        public int StatusesCount
        {
            get { return _userDTO.StatusesCount; }
        }

        public int FollowersCount
        {
            get { return _userDTO.FollowersCount; }
        }

        public int FriendsCount
        {
            get { return _userDTO.FriendsCount; }
        }

        public bool Following
        {
            get { return _userDTO.Following; }
        }

        public bool Protected
        {
            get { return _userDTO.Protected; }
        }

        public bool Verified
        {
            get { return _userDTO.Verified; }
        }

        public string ProfileImageUrl
        {
            get { return _userDTO.ProfileImageUrl; }
        }

        public string ProfileImageUrlHttps
        {
            get { return _userDTO.ProfileImageUrlHttps; }
        }

        public bool FollowRequestSent
        {
            get { return _userDTO.FollowRequestSent; }
        }

        public bool DefaultProfile
        {
            get { return _userDTO.DefaultProfile; }
        }

        public bool DefaultProfileImage
        {
            get { return _userDTO.DefaultProfileImage; }
        }

        public int FavouritesCount
        {
            get { return _userDTO.FavouritesCount ?? 0; }
        }

        public int ListedCount
        {
            get { return _userDTO.ListedCount ?? 0; }
        }

        public string ProfileSidebarFillColor
        {
            get { return _userDTO.ProfileSidebarFillColor; }
        }

        public string ProfileSidebarBorderColor
        {
            get { return _userDTO.ProfileSidebarBorderColor; }
        }

        public string ProfileBackgroundTitle
        {
            get { return _userDTO.ProfileBackgroundTitle; }
        }

        public string ProfileBackgroundColor
        {
            get { return _userDTO.ProfileBackgroundColor; }
        }

        public string ProfileBackgroundImageUrl
        {
            get { return _userDTO.ProfileBackgroundImageUrl; }
        }

        public string ProfileBackgroundImageUrlHttps
        {
            get { return _userDTO.ProfileBackgroundImageUrlHttps; }
        }

        public string ProfileTextColor
        {
            get { return _userDTO.ProfileTextColor; }
        }

        public string ProfileLinkColor
        {
            get { return _userDTO.ProfileLinkColor; }
        }

        public bool ProfileUseBackgroundImage
        {
            get { return _userDTO.ProfileUseBackgroundImage; }
        }

        public bool IsTranslator
        {
            get { return _userDTO.IsTranslator; }
        }

        public bool ShowAllInlineMedia
        {
            get { return _userDTO.ShowAllInlineMedia; }
        }

        public bool ContributorsEnabled
        {
            get { return _userDTO.ContributorsEnabled; }
        }

        public int? UtcOffset
        {
            get { return _userDTO.UtcOffset; }
        }

        public string TimeZone
        {
            get { return _userDTO.TimeZone; }
        }


        [Obsolete("Twitter's documentation states that this property is deprecated")]
        public bool Notifications
        {
            get { return _userDTO.Notifications; }
        }

        #endregion

        #region Tweetinvi API Attributes

        public List<long> FriendIds { get; set; }
        public List<IUser> Friends { get; set; }
        public List<long> FollowerIds { get; set; }
        public List<IUser> Followers { get; set; }
        public List<IUser> Contributors { get; set; }
        public List<IUser> Contributees { get; set; }
        public List<ITweet> Timeline { get; set; }
        public List<ITweet> Retweets { get; set; }
        public List<ITweet> FriendsRetweets { get; set; }
        public List<ITweet> TweetsRetweetedByFollowers { get; set; }

        #endregion

        #endregion

        [InjectionConstructor]
        public User(
            IUserDTO userDTO,
            ITimelineController timelineController,
            IUserController userController,
            IFriendshipFactory friendshipFactory)
        {
            _userDTO = userDTO;
            _timelineController = timelineController;
            _userController = userController;
            _friendshipFactory = friendshipFactory;
        }

        // Friends
        public virtual IEnumerable<long> GetFriendIds(int maxFriendsToRetrieve = 5000)
        {
            return _userController.GetFriendIds(_userDTO, maxFriendsToRetrieve);
        }

        public virtual IEnumerable<IUser> GetFriends(int maxFriendsToRetrieve = 250)
        {
            return _userController.GetFriends(_userDTO, maxFriendsToRetrieve);
        }

        // Followers
        public virtual IEnumerable<long> GetFollowerIds(int maxFriendsToRetrieve = 5000)
        {
            return _userController.GetFollowerIds(_userDTO, maxFriendsToRetrieve);
        }

        public virtual IEnumerable<IUser> GetFollowers(int maxFriendsToRetrieve = 250)
        {
            return _userController.GetFollowers(_userDTO, maxFriendsToRetrieve);
        }

        // Relationship
        public virtual IRelationship GetRelationshipWith(IUser targetUser)
        {
            if (targetUser == null)
            {
                return null;
            }

            return _friendshipFactory.GetRelationshipBetween(_userDTO, targetUser.UserDTO);
        }

        // Timeline
        public virtual IEnumerable<ITweet> GetUserTimeline(int maximumTweets = 20, bool excludeReplies = false)
        {
            return _timelineController.GetUserTimeline(_userDTO, maximumTweets, excludeReplies);
        }

        // Favorites
        public virtual IEnumerable<ITweet> GetFavorites(int maximumTweets = 40)
        {
            return _userController.GetFavouriteTweets(_userDTO, maximumTweets);
        }

        // Block User
        public virtual bool Block()
        {
            return _userController.BlockUser(_userDTO);
        }

        // Bitmap Profile Image
        public Bitmap GenerateProfileImageBitmap(ImageSize imageSize = ImageSize.normal)
        {
            return _userController.GenerateProfileImageBitmap(_userDTO, imageSize);
        }

        // Stream Profile IMage
        public Stream GenerateProfileImageStream(ImageSize imageSize = ImageSize.normal)
        {
            return _userController.GenerateProfileImageStream(_userDTO, imageSize);
        }

        // Download Profile Image
        public bool DownloadProfileImage(string filePath, ImageSize imageSize = ImageSize.normal)
        {
            return _userController.DownloadProfileImage(_userDTO, filePath, imageSize);
        }

        public void DownloadProfileImageAsync(string filePath, Action<bool> successAction, Action<long, long> progressChangedAction = null, ImageSize imageSize = ImageSize.normal)
        {
            _userController.DownloadProfileImageAsync(_userDTO, filePath, successAction, progressChangedAction, imageSize);
        }

        #region Get Contributors
        /// <summary>
        /// Get the list of contributors to the account of the current user
        /// Update the matching attribute of the current user if the parameter is true
        /// Return the list of contributors
        /// </summary>
        /// <param name="createContributorList">False by default. Indicates if the _contributors attribute needs to be updated with the result</param>
        /// <returns>The list of contributors to the account of the current user</returns>
        public IEnumerable<IUser> GetContributors(bool createContributorList = false)
        {
            // string query = Resources.User_GetContributors;
            throw new NotImplementedException();
        }
        #endregion

        #region Get Contributees
        /// <summary>
        /// Get the list of accounts the current user is allowed to update
        /// Update the matching attribute of the current user if the parameter is true
        /// Return the list of contributees
        /// </summary>
        /// <param name="createContributeeList">False by default. Indicates if the _contributees attribute needs to be updated with the result</param>
        /// <returns>The list of accounts the current user is allowed to update</returns>
        public IEnumerable<IUser> GetContributees(bool createContributeeList = false)
        {
            // string query = Resources.User_GetContributees;
            throw new NotImplementedException();
        }

        #endregion

        public override string ToString()
        {
            return _userDTO != null ? _userDTO.Name : "Undefined";
        }

        #region IEquatable<IUser> Members

        /// <summary>
        /// Compare 2 different members and verify if they are the same
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IUser other)
        {
            return Id == other.Id || ScreenName == other.ScreenName;
        }

        #endregion
    }
}