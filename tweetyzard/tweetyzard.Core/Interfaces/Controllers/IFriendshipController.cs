using System.Collections.Generic;
using TweetinviCore.Interfaces.DTO;

namespace TweetinviCore.Interfaces.Controllers
{
    public interface IFriendshipController
    {
        IEnumerable<long> GetUserIdsRequestingFriendship();
        IEnumerable<IUser> GetUsersRequestingFriendship();

        IEnumerable<long> GetUserIdsYouRequestedToFollow();
        IEnumerable<IUser> GetUsersYouRequestedToFollow();

        // Create Friendship with
        bool CreateFriendshipWith(IUser user);
        bool CreateFriendshipWith(IUserIdDTO userDTO);
        bool CreateFriendshipWith(long userId);
        bool CreateFriendshipWith(string userScreeName);

        // Destroy Friendship with
        bool DestroyFriendshipWith(IUser user);
        bool DestroyFriendshipWith(IUserIdDTO userDTO);
        bool DestroyFriendshipWith(long userId);
        bool DestroyFriendshipWith(string userScreeName);

        // Update Friendship Authorizations
        bool UpdateRelationshipAuthorizationsWith(IUser user, bool retweetsEnabled, bool deviceNotifictionEnabled);
        bool UpdateRelationshipAuthorizationsWith(IUserIdDTO userDTO, bool retweetsEnabled, bool deviceNotifictionEnabled);
        bool UpdateRelationshipAuthorizationsWith(long userId, bool retweetsEnabled, bool deviceNotifictionEnabled);
        bool UpdateRelationshipAuthorizationsWith(string userScreenName, bool retweetsEnabled, bool deviceNotifictionEnabled);
    }
}