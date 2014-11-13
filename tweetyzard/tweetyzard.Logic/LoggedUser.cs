using System;
using System.Collections.Generic;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.Models;
using TweetinviCore.Interfaces.oAuth;

namespace TweetinviLogic
{
    /// <summary>
    /// A token user is unique to a Token and provides action that will
    /// be executed from the connected user and that are not available
    /// from another user like (read my messages)
    /// </summary>
    public class LoggedUser : User, ILoggedUser
    {
        private readonly ICredentialsAccessor _credentialsAccessor;
        private readonly ITweetController _tweetController;
        private readonly ITweetFactory _tweetFactory;
        private readonly IMessageController _messageController;
        private readonly IFriendshipController _friendshipController;
        private readonly IAccountController _accountController;
        private readonly ISavedSearchController _savedSearchController;

        private IOAuthCredentials _savedCredentials;

        public LoggedUser(
            IUserDTO userDTO,
            ICredentialsAccessor credentialsAccessor,
            ITimelineController timelineController,
            ITweetController tweetController,
            ITweetFactory tweetFactory,
            IUserController userController,
            IMessageController messageController,
            IFriendshipFactory friendshipFactory,
            IFriendshipController friendshipController,
            IAccountController accountController,
            ISavedSearchController savedSearchController)

            : base(userDTO, timelineController, userController, friendshipFactory)
        {
            _credentialsAccessor = credentialsAccessor;
            _tweetController = tweetController;
            _tweetFactory = tweetFactory;
            _messageController = messageController;
            _friendshipController = friendshipController;
            _accountController = accountController;
            _savedSearchController = savedSearchController;

            Credentials = _credentialsAccessor.CurrentThreadCredentials;
        }

        public IOAuthCredentials Credentials { get; private set; }
        public IEnumerable<IMessage> LatestDirectMessagesReceived { get; set; }
        public IEnumerable<IMessage> LatestDirectMessagesSent { get; set; }
        public IEnumerable<IMention> LatestMentionsTimeline { get; set; }
        public IEnumerable<ITweet> LatestHomeTimeline { get; set; }
        public IEnumerable<ISuggestedUserList> SuggestedUserList { get; set; }
        public IEnumerable<IUser> BlockedUsers { get; set; }
        public IEnumerable<long> BlockedUsersIds { get; set; }

        private void StartLoggedUserOperation()
        {
            _savedCredentials = _credentialsAccessor.CurrentThreadCredentials;
            _credentialsAccessor.CurrentThreadCredentials = Credentials;
        }

        private void CompletedLoggedUserOperation()
        {
            _credentialsAccessor.CurrentThreadCredentials = _savedCredentials;
        }

        private T StartLoggedUserOperation<T>(Func<T> operation)
        {
            StartLoggedUserOperation();
            var result = operation();
            CompletedLoggedUserOperation();
            return result;
        }

        private void StartLoggedUserOperation(Action operation)
        {
            StartLoggedUserOperation();
            operation();
            CompletedLoggedUserOperation();
        }

        // Home Timeline
        public IEnumerable<ITweet> GetHomeTimeline(int count = 40, bool excludeReplies = false)
        {
            return StartLoggedUserOperation(() => _timelineController.GetHomeTimeline(count, excludeReplies));
        }

        public IEnumerable<IMention> GetMentionsTimeline(int count = 40)
        {
            return StartLoggedUserOperation(() => _timelineController.GetMentionsTimeline(count));
        }

        // Frienships
        public IEnumerable<IRelationshipState> GetRelationshipStatesWith(IEnumerable<IUser> users)
        {
            return StartLoggedUserOperation(() => _friendshipFactory.GetRelationshipStatesWith(users));
        }

        public Dictionary<IUser, IRelationshipState> GetRelationshipStatesAssociatedWith(IEnumerable<IUser> users)
        {
            return StartLoggedUserOperation(() => _friendshipFactory.GetRelationshipStatesAssociatedWith(users));
        }

        // Friends - Followers
        public IEnumerable<IUser> GetUsersRequestingFriendship()
        {
            return StartLoggedUserOperation(() => _friendshipController.GetUsersRequestingFriendship());
        }

        public IEnumerable<IUser> GetUsersYouRequestedToFollow()
        {
            return StartLoggedUserOperation(() => _friendshipController.GetUsersYouRequestedToFollow());
        }

        // Follow
        public bool FollowUser(IUser user)
        {
            return StartLoggedUserOperation(() => _friendshipController.CreateFriendshipWith(user));
        }

        public bool UnFollowUser(IUser user)
        {
            return StartLoggedUserOperation(() => _friendshipController.DestroyFriendshipWith(user));
        }

        public bool UpdateRelationshipAuthorizationsWith(IUser user, bool retweetsEnabled, bool deviceNotificationsEnabled)
        {
            return StartLoggedUserOperation(() => _friendshipController.UpdateRelationshipAuthorizationsWith(user, retweetsEnabled, deviceNotificationsEnabled));
        }

        public IEnumerable<ISavedSearch> GetSavedSearches()
        {
            return StartLoggedUserOperation(() => _savedSearchController.GetSavedSearches());
        }

        // Direct Messages
        public IEnumerable<IMessage> GetLatestMessagesReceived(int maximumMessages = 40)
        {
            return StartLoggedUserOperation(() => _messageController.GetLatestMessagesReceived(maximumMessages));
        }

        public IEnumerable<IMessage> GetLatestMessagesSent(int maximumMessages = 40)
        {
            return StartLoggedUserOperation(() => _messageController.GetLatestMessagesSent(maximumMessages));
        }

        public IMessage PublishMessage(IMessage message)
        {
            return StartLoggedUserOperation(() => _messageController.PublishMessage(message.MessageDTO));
        }

        // Tweet
        public ITweet PublishTweet(string text)
        {
            var tweet = _tweetFactory.CreateTweet(text);
            PublishTweet(tweet);
            return tweet;
        }

        public bool PublishTweet(ITweet tweet)
        {
            _tweetController.PublishTweet(tweet);
            StartLoggedUserOperation(() => _tweetController.PublishTweet(tweet));
            return tweet.IsTweetPublished;
        }

        #region Get Blocked Users

        public IEnumerable<IUser> GetBlockedUsers(bool createBlockUsers = true, bool createBlockedUsersIds = true)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<long> GetBlockedUsersIds(bool createBlockedUsersIds = true)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Get Suggested List

        public IEnumerable<ISuggestedUserList> GetSuggestedUserList(bool createSuggestedUserList = true)
        {
            throw new NotImplementedException();
        }
        #endregion


        public IAccountSettings AccountSettings { get; set; }

        /// <summary>
        /// Retrieve the settings of the Token's owner
        /// </summary>
        public IAccountSettings GetAccountSettings()
        {
            return StartLoggedUserOperation(() => _accountController.GetLoggedUserSettings());
        }
    }
}