using System;
using System.Collections.Generic;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.QueryGenerators;
using TweetinviFactories.Properties;
using TweetinviFactories.User;


namespace TweetinviFactories.Friendship
{
    public interface IFriendshipFactoryQueryExecutor
    {
        // Get Existing Relationship
        IRelationshipDTO GetRelationshipBetween(IUserIdDTO sourceUserDTO, IUserIdDTO targetUserDTO);
        IRelationshipDTO GetRelationshipBetween(long sourceUserId, long targetUserId);
        IRelationshipDTO GetRelationshipBetween(long sourceUserId, string targetUserScreenName);
        IRelationshipDTO GetRelationshipBetween(string sourceUserScreenName, long targetUserId);
        IRelationshipDTO GetRelationshipBetween(string sourceUserScreenName, string targetUserScreenName);

        // Get Multiple Relationships
        IEnumerable<IRelationshipStateDTO> GetRelationshipStatesWith(IEnumerable<IUserIdDTO> targetUsersDTO);
        IEnumerable<IRelationshipStateDTO> GetRelationshipStatesWith(IEnumerable<long> targetUsersId);
        IEnumerable<IRelationshipStateDTO> GetRelationshipStatesWith(IEnumerable<string> targetUsersScreenName);
    }

    public class FriendshipFactoryQueryExecutor : IFriendshipFactoryQueryExecutor
    {
        private readonly ITwitterAccessor _twitterAccessor;
        private readonly IUserQueryParameterGenerator _userQueryParameterGenerator;
        private readonly IUserFactoryQueryGenerator _userFactoryQueryGenerator;

        public FriendshipFactoryQueryExecutor(
            ITwitterAccessor twitterAccessor,
            IUserQueryParameterGenerator userQueryParameterGenerator,
            IUserFactoryQueryGenerator userFactoryQueryGenerator)
        {
            _twitterAccessor = twitterAccessor;
            _userQueryParameterGenerator = userQueryParameterGenerator;
            _userFactoryQueryGenerator = userFactoryQueryGenerator;
        }

        // Get Existing Relationship
        public IRelationshipDTO GetRelationshipBetween(IUserIdDTO sourceUserDTO, IUserIdDTO targetUserDTO)
        {
            string sourceParameter = _userQueryParameterGenerator.GenerateIdOrScreenNameParameter(sourceUserDTO, "source_id", "source_screen_name");
            string targetParameter = _userQueryParameterGenerator.GenerateIdOrScreenNameParameter(targetUserDTO, "target_id", "target_screen_name");
            string query = String.Format(Resources.Friendship_GetRelationship, sourceParameter, targetParameter);

            return _twitterAccessor.ExecuteGETQuery<IRelationshipDTO>(query);
        }

        public IRelationshipDTO GetRelationshipBetween(long sourceUserId, long targetUserId)
        {
            string sourceParameter = _userQueryParameterGenerator.GenerateUserIdParameter(sourceUserId);
            string targetParameter = _userQueryParameterGenerator.GenerateUserIdParameter(targetUserId);
            string query = String.Format(Resources.Friendship_GetRelationship, sourceParameter, targetParameter);

            return _twitterAccessor.ExecuteGETQuery<IRelationshipDTO>(query);
        }

        public IRelationshipDTO GetRelationshipBetween(long sourceUserId, string targetUserScreenName)
        {
            string sourceParameter = _userQueryParameterGenerator.GenerateUserIdParameter(sourceUserId);
            string targetParameter = _userQueryParameterGenerator.GenerateScreenNameParameter(targetUserScreenName);
            string query = String.Format(Resources.Friendship_GetRelationship, sourceParameter, targetParameter);

            return _twitterAccessor.ExecuteGETQuery<IRelationshipDTO>(query);
        }

        public IRelationshipDTO GetRelationshipBetween(string sourceUserScreenName, long targetUserId)
        {
            string sourceParameter = _userQueryParameterGenerator.GenerateScreenNameParameter(sourceUserScreenName);
            string targetParameter = _userQueryParameterGenerator.GenerateUserIdParameter(targetUserId);
            string query = String.Format(Resources.Friendship_GetRelationship, sourceParameter, targetParameter);

            return _twitterAccessor.ExecuteGETQuery<IRelationshipDTO>(query);
        }

        public IRelationshipDTO GetRelationshipBetween(string sourceUserScreenName, string targetUserScreenName)
        {
            string sourceParameter = _userQueryParameterGenerator.GenerateScreenNameParameter(sourceUserScreenName);
            string targetParameter = _userQueryParameterGenerator.GenerateScreenNameParameter(targetUserScreenName);
            string query = String.Format(Resources.Friendship_GetRelationship, sourceParameter, targetParameter);

            return _twitterAccessor.ExecuteGETQuery<IRelationshipDTO>(query);
        }

        // Get Relationship with
        public IEnumerable<IRelationshipStateDTO> GetRelationshipStatesWith(IEnumerable<IUserIdDTO> targetUsersDTO)
        {
            string userIdsAndScreenNameParameter = _userFactoryQueryGenerator.GenerateListOfUserDTOParameter(targetUsersDTO);
            string query = String.Format(Resources.Friendship_GetRelationships, userIdsAndScreenNameParameter);

            return _twitterAccessor.ExecuteGETQuery<IEnumerable<IRelationshipStateDTO>>(query);
        }

        public IEnumerable<IRelationshipStateDTO> GetRelationshipStatesWith(IEnumerable<long> targetUsersId)
        {
            string userIds = _userFactoryQueryGenerator.GenerateListOfIdsParameter(targetUsersId);
            string userIdsParameter = String.Format("user_id={0}", userIds);
            string query = String.Format(Resources.Friendship_GetRelationships, userIdsParameter);

            return _twitterAccessor.ExecuteGETQuery<IEnumerable<IRelationshipStateDTO>>(query);
        }

        public IEnumerable<IRelationshipStateDTO> GetRelationshipStatesWith(IEnumerable<string> targetUsersScreenName)
        {
            string userScreenNames = _userFactoryQueryGenerator.GenerateListOfScreenNameParameter(targetUsersScreenName);
            string userScreenNamesParameter = String.Format("screen_name={0}", userScreenNames);
            string query = String.Format(Resources.Friendship_GetRelationships, userScreenNamesParameter);

            return _twitterAccessor.ExecuteGETQuery<IEnumerable<IRelationshipStateDTO>>(query);
        }
    }
}