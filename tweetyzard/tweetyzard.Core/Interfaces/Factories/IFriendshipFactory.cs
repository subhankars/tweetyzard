using System.Collections.Generic;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Models;

namespace TweetinviCore.Interfaces.Factories
{
    public interface IFriendshipFactory
    {
        // Relationship
        IRelationship GetRelationshipBetween(IUser sourceUser, IUser targetUser);
        IRelationship GetRelationshipBetween(IUserIdDTO sourceUserDTO, IUserIdDTO targetUserDTO);

        IRelationship GetRelationshipBetween(long sourceUserId, long targetUserId);
        IRelationship GetRelationshipBetween(long sourceUserId, string targetUserScreenName);

        IRelationship GetRelationshipBetween(string sourceUserScreenName, string targetUserScreenName);
        IRelationship GetRelationshipBetween(string sourceUserScreenName, long targetUserId);

        // Get Relationships between the current user and a list of users
        Dictionary<IUser, IRelationshipState> GetRelationshipStatesAssociatedWith(IEnumerable<IUser> targetUsers);

        IEnumerable<IRelationshipState> GetRelationshipStatesWith(IEnumerable<IUser> targetUsers);
        IEnumerable<IRelationshipState> GetRelationshipStatesWith(IEnumerable<IUserIdDTO> targetUsersDTO);
        IEnumerable<IRelationshipState> GetRelationshipStatesWith(IEnumerable<long> targetUsersId);
        IEnumerable<IRelationshipState> GetRelationshipStatesWith(IEnumerable<string> targetUsersScreenName);

        // Generate from DTO
        IRelationship GenerateRelationshipFromRelationshipDTO(IRelationshipDTO relationshipDTO);
        IEnumerable<IRelationship> GenerateRelationshipsFromRelationshipsDTO(IEnumerable<IRelationshipDTO> relationshipDTO);

        // Generate RelationshipAuthorizations
        IFriendshipAuthorizations GenerateFriendshipAuthorizations(bool retweetsEnabled, bool deviceNotificationEnabled);
    }
}