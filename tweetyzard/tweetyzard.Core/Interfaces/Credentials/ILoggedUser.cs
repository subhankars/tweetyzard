using System.Collections.Generic;
using TweetinviCore.Interfaces.Models;
using TweetinviCore.Interfaces.oAuth;

namespace TweetinviCore.Interfaces.Credentials
{
    /// <summary>
    /// User associated with a Token, this "privileged" user
    /// has access private information like messages, timeline...
    /// </summary>
    public interface ILoggedUser : IUser
    {
        IOAuthCredentials Credentials { get; }

        #region Tweets

        /// <summary>
        /// Send a very simple Tweet with a simple message
        /// </summary>
        /// <param name="text">Text of the tweet</param>
        /// <returns>If the tweet has been sent returns the Tweet</returns>
        ITweet PublishTweet(string text);

        /// <summary>
        /// Send a very simple Tweet with a simple message
        /// </summary>
        /// <param name="tweet">Tweet to be sent</param>
        /// <returns>Tweet has been successfully sent</returns>
        bool PublishTweet(ITweet tweet);

        #endregion

        // Direct Messages
        /// <summary>
        /// List of Messages received
        /// </summary>
        IEnumerable<IMessage> LatestDirectMessagesReceived { get; }

        /// <summary>
        /// List of messages sent
        /// </summary>
        IEnumerable<IMessage> LatestDirectMessagesSent { get; }

        /// <summary>
        /// Get the list of direct messages received by the user
        /// Update the matching attribute of the current user if the parameter is true
        /// Return the list of direct messages received by the user
        /// </summary>
        /// <param name="count">Maximum number of messages retrieved</param>
        /// <returns>Collection of direct messages received by the user</returns>
        IEnumerable<IMessage> GetLatestMessagesReceived(int count = 40);

        IEnumerable<IMessage> GetLatestMessagesSent(int maximumMessages = 40);

        IMessage PublishMessage(IMessage message);

        // Timeline
        /// <summary>
        /// List of tweets as displayed on the Home timeline
        /// Storing the information so that it is not required 
        /// to fetch the data again
        /// </summary>
        IEnumerable<ITweet> LatestHomeTimeline { get; set; }

        /// <summary>
        /// Get the latest tweets of the TokenUser Home timeline
        /// </summary>
        IEnumerable<ITweet> GetHomeTimeline(int count = 40, bool excludeReplies = false);

        /// <summary>
        /// List of tweets as displayed on the Mentions timeline
        /// Storing the information so that it is not required 
        /// to fetch the data again
        /// </summary>
        IEnumerable<IMention> LatestMentionsTimeline { get; set; }

        /// <summary>
        /// Get the latest tweets of the TokenUser Mentions timeline
        /// </summary>
        /// <param name="count">Number of tweets expected</param>
        /// <returns>Tweets of the Mentions timeline of the connected user</returns>
        IEnumerable<IMention> GetMentionsTimeline(int count = 40);

        // Relationship
        IEnumerable<IRelationshipState> GetRelationshipStatesWith(IEnumerable<IUser> users);
        Dictionary<IUser, IRelationshipState> GetRelationshipStatesAssociatedWith(IEnumerable<IUser> users);

        // Friends - Followers
        IEnumerable<IUser> GetUsersRequestingFriendship();
        IEnumerable<IUser> GetUsersYouRequestedToFollow();
        
        bool FollowUser(IUser user);
        bool UnFollowUser(IUser user);
        bool UpdateRelationshipAuthorizationsWith(IUser user, bool retweetsEnabled, bool deviceNotificationsEnabled);

        // Saved Searches
        IEnumerable<ISavedSearch> GetSavedSearches();

        // Blocked Users
        /// <summary>
        /// User blocked list with their profile information
        /// </summary>
        IEnumerable<IUser> BlockedUsers { get; set; }

        /// <summary>
        /// User of user ids blocked
        /// </summary>
        IEnumerable<long> BlockedUsersIds { get; set; }

        /// <summary>
        /// Retrieve the users blocked by the current user.
        /// Populate the corresponding attributes according to the value of the boolean parameters.
        /// Return the list of users blocked by the current user.
        /// </summary>
        /// <param name="createBlockUsers">True by default. Update the attribute _blocked_users if this parameter is true</param>
        /// <param name="createBlockedUsersIds">True by default. Update the attribute _blocked_users_ids if this parameter is true</param>
        /// <returns>Null if there is no valid token for the current user. Otherwise, The list of users blocked by the current user.</returns>
        IEnumerable<IUser> GetBlockedUsers(bool createBlockUsers = true, bool createBlockedUsersIds = true);

        /// <summary>
        /// Retrieve the ids of the users blocked by the current user.
        /// Populate the corresponding attribute according to the value of the boolean parameter.
        /// Return the list of ids of the users blocked by the current user.
        /// </summary>
        /// <param name="createBlockedUsersIds">True by default. Update the attribute _blocked_users_ids if this parameter is true</param>
        /// <returns>Null if there is no valid token for the current user. Otherwise, The list of ids of the users blocked by the current user.</returns>
        IEnumerable<long> GetBlockedUsersIds(bool createBlockedUsersIds = true);

        #region List

        /// <summary>
        /// Provide a List users that are likely to be of interest
        /// </summary>
        IEnumerable<ISuggestedUserList> SuggestedUserList { get; set; }

        /// <summary>
        /// Retrieve the lists of suggested users associated to the current user from the Twitter API.
        /// Update the corresponding attribute according to the value of the parameter. 
        /// Return the lists of suggested users.
        /// </summary>
        /// <param name="createSuggestedUserList">update the _suggestedUserList if true</param>
        /// <returns>null if the token parameter is null, the lists of suggested users otherwise</returns>
        IEnumerable<ISuggestedUserList> GetSuggestedUserList(bool createSuggestedUserList = true);

        #endregion

        IAccountSettings AccountSettings { get; set; }
        IAccountSettings GetAccountSettings();
    }
}