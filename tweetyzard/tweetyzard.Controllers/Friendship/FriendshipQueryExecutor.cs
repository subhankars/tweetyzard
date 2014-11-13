using System.Collections.Generic;
using System.Linq;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.Credentials.QueryDTO;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Models;
using TweetinviCore.Interfaces.QueryValidators;

namespace TweetinviControllers.Friendship
{
    public interface IFriendshipQueryExecutor
    {
        IEnumerable<long> GetUserIdsRequestingFriendship();
        IEnumerable<long> GetUserIdsYouRequestedToFollow();

        // Create Friendship
        bool CreateFriendshipWith(IUserIdDTO userDTO);
        bool CreateFriendshipWith(long userId);
        bool CreateFriendshipWith(string userScreenName);

        // Destroy Friendship
        bool DestroyFriendshipWith(IUserIdDTO userDTO);
        bool DestroyFriendshipWith(long userId);
        bool DestroyFriendshipWith(string userScreenName);

        // Update Friendship Authorization
        bool UpdateRelationshipAuthorizationsWith(IUserIdDTO userDTO, IFriendshipAuthorizations friendshipAuthorizations);
        bool UpdateRelationshipAuthorizationsWith(long userId, IFriendshipAuthorizations friendshipAuthorizations);
        bool UpdateRelationshipAuthorizationsWith(string userScreenName, IFriendshipAuthorizations friendshipAuthorizations);
    }

    public class FriendshipQueryExecutor : IFriendshipQueryExecutor
    {
        private readonly IFriendshipQueryGenerator _friendshipQueryGenerator;
        private readonly IUserQueryValidator _userQueryValidator;
        private readonly ITwitterAccessor _twitterAccessor;

        public FriendshipQueryExecutor(
            IFriendshipQueryGenerator friendshipQueryGenerator,
            IUserQueryValidator userQueryValidator,
            ITwitterAccessor twitterAccessor)
        {
            _twitterAccessor = twitterAccessor;
            _friendshipQueryGenerator = friendshipQueryGenerator;
            _userQueryValidator = userQueryValidator;
        }

        public IEnumerable<long> GetUserIdsRequestingFriendship()
        {
            string query = _friendshipQueryGenerator.GetUserIdsRequestingFriendshipQuery();
            var userIdsDTO = _twitterAccessor.ExecuteCursorGETQuery<IIdsCursorQueryResultDTO>(query);

            if (userIdsDTO == null)
            {
                return null;
            }

            var userIdsDTOList = userIdsDTO.ToList();

            var userdIds = new List<long>();
            for (int i = 0; i < userIdsDTOList.Count; ++i)
            {
                userdIds.AddRange(userIdsDTOList.ElementAt(i).Ids);
            }

            return userdIds;
        }

        public IEnumerable<long> GetUserIdsYouRequestedToFollow()
        {
            string query = _friendshipQueryGenerator.GetUserIdsYouRequestedToFollowQuery();
            var userIdsDTO = _twitterAccessor.ExecuteCursorGETQuery<IIdsCursorQueryResultDTO>(query);

            if (userIdsDTO == null)
            {
                return null;
            }

            var userIdsDTOList = userIdsDTO.ToList();

            var userdIds = new List<long>();
            for (int i = 0; i < userIdsDTOList.Count; ++i)
            {
                userdIds.AddRange(userIdsDTOList.ElementAt(i).Ids);
            }

            return userdIds;
        }

        // Create Friendship
        public bool CreateFriendshipWith(IUserIdDTO userDTO)
        {
            string query = _friendshipQueryGenerator.GetCreateFriendshipWithQuery(userDTO);
            return _twitterAccessor.TryExecutePOSTQuery(query);
        }

        public bool CreateFriendshipWith(long userId)
        {
            string query = _friendshipQueryGenerator.GetCreateFriendshipWithQuery(userId);
            return _twitterAccessor.TryExecutePOSTQuery(query);
        }

        public bool CreateFriendshipWith(string userScreenName)
        {
            string query = _friendshipQueryGenerator.GetCreateFriendshipWithQuery(userScreenName);
            return _twitterAccessor.TryExecutePOSTQuery(query);
        }

        // Destroy Friendship
        public bool DestroyFriendshipWith(IUserIdDTO userDTO)
        {
            if (!_userQueryValidator.CanUserBeIdentified(userDTO))
            {
                return false;
            }

            string query = _friendshipQueryGenerator.GetDestroyFriendshipWithQuery(userDTO);
            return _twitterAccessor.TryExecutePOSTQuery(query);
        }

        public bool DestroyFriendshipWith(long userId)
        {
            string query = _friendshipQueryGenerator.GetDestroyFriendshipWithQuery(userId);
            return _twitterAccessor.TryExecutePOSTQuery(query);
        }

        public bool DestroyFriendshipWith(string userScreenName)
        {
            string query = _friendshipQueryGenerator.GetDestroyFriendshipWithQuery(userScreenName);
            return _twitterAccessor.TryExecutePOSTQuery(query);
        }

        // Update Friendship Authorizations
        public bool UpdateRelationshipAuthorizationsWith(IUserIdDTO userDTO, IFriendshipAuthorizations friendshipAuthorizations)
        {
            if (!_userQueryValidator.CanUserBeIdentified(userDTO))
            {
                return false;
            }

            string query = _friendshipQueryGenerator.GetUpdateRelationshipAuthorizationsWithQuery(userDTO, friendshipAuthorizations);
            return _twitterAccessor.TryExecutePOSTQuery(query);
        }

        public bool UpdateRelationshipAuthorizationsWith(long userId, IFriendshipAuthorizations friendshipAuthorizations)
        {
            string query = _friendshipQueryGenerator.GetUpdateRelationshipAuthorizationsWithQuery(userId, friendshipAuthorizations);
            return _twitterAccessor.TryExecutePOSTQuery(query);
        }

        public bool UpdateRelationshipAuthorizationsWith(string userScreenName, IFriendshipAuthorizations friendshipAuthorizations)
        {
            string query = _friendshipQueryGenerator.GetUpdateRelationshipAuthorizationsWithQuery(userScreenName, friendshipAuthorizations);
            return _twitterAccessor.TryExecutePOSTQuery(query);
        }
    }
}