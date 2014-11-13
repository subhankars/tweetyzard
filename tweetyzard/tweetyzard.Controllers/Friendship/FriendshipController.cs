using System;
using System.Collections.Generic;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;

namespace TweetinviControllers.Friendship
{
    public class FriendshipController : IFriendshipController
    {
        private readonly IFriendshipQueryExecutor _friendshipQueryExecutor;
        private readonly IUserFactory _userFactory;
        private readonly IFriendshipFactory _friendshipFactory;

        public FriendshipController(
            IFriendshipQueryExecutor friendshipQueryExecutor,
            IUserFactory userFactory,
            IFriendshipFactory friendshipFactory)
        {
            _friendshipQueryExecutor = friendshipQueryExecutor;
            _userFactory = userFactory;
            _friendshipFactory = friendshipFactory;
        }

        // Get Users Requesting Friendship
        public IEnumerable<long> GetUserIdsRequestingFriendship()
        {
            return _friendshipQueryExecutor.GetUserIdsRequestingFriendship();
        }

        public IEnumerable<IUser> GetUsersRequestingFriendship()
        {
            var userIds = GetUserIdsRequestingFriendship();
            return _userFactory.GetUsersFromIds(userIds);
        }

        // Get Users You requested to follow
        public IEnumerable<long> GetUserIdsYouRequestedToFollow()
        {
            return _friendshipQueryExecutor.GetUserIdsYouRequestedToFollow();
        }

        public IEnumerable<IUser> GetUsersYouRequestedToFollow()
        {
            var userIds = GetUserIdsYouRequestedToFollow();
            return _userFactory.GetUsersFromIds(userIds);
        }

        // Create Friendship with
        public bool CreateFriendshipWith(IUser user)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }

            return CreateFriendshipWith(user.UserDTO);
        }

        public bool CreateFriendshipWith(IUserIdDTO userDTO)
        {
            return _friendshipQueryExecutor.CreateFriendshipWith(userDTO);
        }

        public bool CreateFriendshipWith(long userId)
        {
            return _friendshipQueryExecutor.CreateFriendshipWith(userId);
        }

        public bool CreateFriendshipWith(string userScreeName)
        {
            return _friendshipQueryExecutor.CreateFriendshipWith(userScreeName);
        }

        // Destroy Friendship with
        public bool DestroyFriendshipWith(IUser user)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }

            return DestroyFriendshipWith(user.UserDTO);
        }

        public bool DestroyFriendshipWith(IUserIdDTO userDTO)
        {
            return _friendshipQueryExecutor.DestroyFriendshipWith(userDTO);
        }

        public bool DestroyFriendshipWith(long userId)
        {
            return _friendshipQueryExecutor.DestroyFriendshipWith(userId);
        }

        public bool DestroyFriendshipWith(string userScreeName)
        {
            return _friendshipQueryExecutor.DestroyFriendshipWith(userScreeName);
        }

        // Update Friendship Authorizations
        public bool UpdateRelationshipAuthorizationsWith(IUser user, bool retweetsEnabled, bool deviceNotifictionEnabled)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }

            return UpdateRelationshipAuthorizationsWith(user.UserDTO, retweetsEnabled, deviceNotifictionEnabled);
        }

        public bool UpdateRelationshipAuthorizationsWith(IUserIdDTO userDTO, bool retweetsEnabled, bool deviceNotifictionEnabled)
        {
            var friendshipAuthorizations = _friendshipFactory.GenerateFriendshipAuthorizations(retweetsEnabled, deviceNotifictionEnabled);
            return _friendshipQueryExecutor.UpdateRelationshipAuthorizationsWith(userDTO, friendshipAuthorizations);
        }

        public bool UpdateRelationshipAuthorizationsWith(long userId, bool retweetsEnabled, bool deviceNotifictionEnabled)
        {
            var friendshipAuthorizations = _friendshipFactory.GenerateFriendshipAuthorizations(retweetsEnabled, deviceNotifictionEnabled);
            return _friendshipQueryExecutor.UpdateRelationshipAuthorizationsWith(userId, friendshipAuthorizations);
        }

        public bool UpdateRelationshipAuthorizationsWith(string userScreenName, bool retweetsEnabled, bool deviceNotifictionEnabled)
        {
            var friendshipAuthorizations = _friendshipFactory.GenerateFriendshipAuthorizations(retweetsEnabled, deviceNotifictionEnabled);
            return _friendshipQueryExecutor.UpdateRelationshipAuthorizationsWith(userScreenName, friendshipAuthorizations);
        }
    }
}