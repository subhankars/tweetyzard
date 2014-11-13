using System.Collections.Generic;
using System.Linq;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.Models;

namespace TweetinviFactories.Friendship
{
    public class FriendshipFactory : IFriendshipFactory
    {
        private readonly IUnityFactory<IRelationship> _unityRelationshipFactory;
        private readonly IUnityFactory<IRelationshipState> _unityRelationshipStateFactory;
        private readonly IFriendshipFactoryQueryExecutor _friendshipFactoryQueryExecutor;
        private readonly IUnityFactory<IFriendshipAuthorizations> _friendshipAuthorizationUnityFactory;


        public FriendshipFactory(
            IUnityFactory<IRelationship> unityRelationshipFactory,
            IUnityFactory<IRelationshipState> unityRelationshipStateFactory,
            IFriendshipFactoryQueryExecutor friendshipFactoryQueryExecutor,
            IUnityFactory<IFriendshipAuthorizations> friendshipAuthorizationUnityFactory)
        {
            _unityRelationshipFactory = unityRelationshipFactory;
            _unityRelationshipStateFactory = unityRelationshipStateFactory;
            _friendshipFactoryQueryExecutor = friendshipFactoryQueryExecutor;
            _friendshipAuthorizationUnityFactory = friendshipAuthorizationUnityFactory;
        }

        // Get Relationship (get between 2 users as there is no id for a relationship)
        public IRelationship GetRelationshipBetween(IUser sourceUser, IUser targetUser)
        {
            if (sourceUser == null || targetUser == null)
            {
                return null;
            }

            return GetRelationshipBetween(sourceUser.UserDTO, targetUser.UserDTO);
        }

        public IRelationship GetRelationshipBetween(IUserIdDTO sourceUserDTO, IUserIdDTO targetUserDTO)
        {
            var relationshipDTO = _friendshipFactoryQueryExecutor.GetRelationshipBetween(sourceUserDTO, targetUserDTO);
            return GenerateRelationshipFromRelationshipDTO(relationshipDTO);
        }

        public IRelationship GetRelationshipBetween(long sourceUserId, long targetUserId)
        {
            var relationshipDTO = _friendshipFactoryQueryExecutor.GetRelationshipBetween(sourceUserId, targetUserId);
            return GenerateRelationshipFromRelationshipDTO(relationshipDTO);
        }

        public IRelationship GetRelationshipBetween(long sourceUserId, string targetUserScreenName)
        {
            var relationshipDTO = _friendshipFactoryQueryExecutor.GetRelationshipBetween(sourceUserId, targetUserScreenName);
            return GenerateRelationshipFromRelationshipDTO(relationshipDTO);
        }

        public IRelationship GetRelationshipBetween(string sourceUserScreenName, string targetUserScreenName)
        {
            var relationshipDTO = _friendshipFactoryQueryExecutor.GetRelationshipBetween(sourceUserScreenName, targetUserScreenName);
            return GenerateRelationshipFromRelationshipDTO(relationshipDTO);
        }

        public IRelationship GetRelationshipBetween(string sourceUserScreenName, long targetUserId)
        {
            var relationshipDTO = _friendshipFactoryQueryExecutor.GetRelationshipBetween(sourceUserScreenName, targetUserId);
            return GenerateRelationshipFromRelationshipDTO(relationshipDTO);
        }

        // Get multiple relationships
        public Dictionary<IUser, IRelationshipState> GetRelationshipStatesAssociatedWith(IEnumerable<IUser> targetUsers)
        {
            if (targetUsers == null)
            {
                return null;
            }

            var relationshipStates = GetRelationshipStatesWith(targetUsers.Select(x => x.UserDTO).ToList());
            var userRelationshipState = new Dictionary<IUser, IRelationshipState>();

            foreach (var targetUser in targetUsers)
            {
                var userRelationship = relationshipStates.FirstOrDefault(x => x.TargetId == targetUser.Id ||
                                                                              x.TargetScreenName == targetUser.ScreenName);
                userRelationshipState.Add(targetUser, userRelationship);
            }

            return userRelationshipState;
        }

        public IEnumerable<IRelationshipState> GetRelationshipStatesWith(IEnumerable<IUser> targetUsers)
        {
            if (targetUsers == null)
            {
                return null;
            }

            return GetRelationshipStatesWith(targetUsers.Select(x => x.UserDTO).ToList());
        }

        public IEnumerable<IRelationshipState> GetRelationshipStatesWith(IEnumerable<IUserIdDTO> targetUsersDTO)
        {
            var relationshipDTO = _friendshipFactoryQueryExecutor.GetRelationshipStatesWith(targetUsersDTO);
            return GenerateRelationshipStatesFromRelationshipStatesDTO(relationshipDTO);
        }

        public IEnumerable<IRelationshipState> GetRelationshipStatesWith(IEnumerable<long> targetUsersId)
        {
            var relationshipDTO = _friendshipFactoryQueryExecutor.GetRelationshipStatesWith(targetUsersId);
            return GenerateRelationshipStatesFromRelationshipStatesDTO(relationshipDTO);
        }

        public IEnumerable<IRelationshipState> GetRelationshipStatesWith(IEnumerable<string> targetUsersScreenName)
        {
            var relationshipDTO = _friendshipFactoryQueryExecutor.GetRelationshipStatesWith(targetUsersScreenName);
            return GenerateRelationshipStatesFromRelationshipStatesDTO(relationshipDTO);
        }

        // Generate From DTO
        public IRelationship GenerateRelationshipFromRelationshipDTO(IRelationshipDTO relationshipDTO)
        {
            var relationshipParameter = _unityRelationshipFactory.GenerateParameterOverrideWrapper("relationshipDTO", relationshipDTO);
            return _unityRelationshipFactory.Create(relationshipParameter);
        }

        public IEnumerable<IRelationship> GenerateRelationshipsFromRelationshipsDTO(IEnumerable<IRelationshipDTO> relationshipDTO)
        {
            if (relationshipDTO == null)
            {
                return null;
            }

            return relationshipDTO.Select(GenerateRelationshipFromRelationshipDTO).ToList();
        }

        // Generate Relationship state from DTO
        public IRelationshipState GenerateRelationshipStateFromRelationshipStateDTO(IRelationshipStateDTO relationshipStateDTO)
        {
            var relationshipStateParameter = _unityRelationshipFactory.GenerateParameterOverrideWrapper("relationshipStateDTO", relationshipStateDTO);
            return _unityRelationshipStateFactory.Create(relationshipStateParameter);
        }

        public List<IRelationshipState> GenerateRelationshipStatesFromRelationshipStatesDTO(IEnumerable<IRelationshipStateDTO> relationshipStateDTO)
        {
            if (relationshipStateDTO == null)
            {
                return null;
            }

            return relationshipStateDTO.Select(GenerateRelationshipStateFromRelationshipStateDTO).ToList();
        }

        // Generate RelationshipAuthorizations
        public IFriendshipAuthorizations GenerateFriendshipAuthorizations(bool retweetsEnabled, bool deviceNotificationEnabled)
        {
            var friendshipAuthorization = _friendshipAuthorizationUnityFactory.Create();

            friendshipAuthorization.RetweetsEnabled = retweetsEnabled;
            friendshipAuthorization.DeviceNotificationEnabled = deviceNotificationEnabled;

            return friendshipAuthorization;
        }
    }
}