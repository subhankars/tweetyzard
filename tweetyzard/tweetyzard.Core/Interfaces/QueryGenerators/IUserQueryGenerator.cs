using TweetinviCore.Enum;
using TweetinviCore.Interfaces.DTO;

namespace TweetinviCore.Interfaces.QueryGenerators
{
    public interface IUserQueryGenerator
    {
        // Friend Ids
        string GetFriendIdsQuery(IUserIdDTO userDTO, int maxFriendsToRetrieve);
        string GetFriendIdsQuery(long userId, int maxFriendsToRetrieve);
        string GetFriendIdsQuery(string screenName, int maxFriendsToRetrieve);

        // Followers Ids
        string GetFollowerIdsQuery(IUserIdDTO userDTO, int maxFollowersToRetrieve);
        string GetFollowerIdsQuery(long userId, int maxFollowersToRetrieve);
        string GetFollowerIdsQuery(string screenName, int maxFollowersToRetrieve);

        // Favourites
        string GetFavouriteTweetsQuery(IUserIdDTO userDTO, int maxFavoritesToRetrieve);
        string GetFavouriteTweetsQuery(long userId, int maxFavoritesToRetrieve);
        string GetFavouriteTweetsQuery(string screenName, int maxFavoritesToRetrieve);

        // Block User
        string GetBlockUserQuery(IUserIdDTO userDTO);
        string GetBlockUserQuery(long userId);
        string GetBlockUserQuery(string userScreenName);

        // Download Profile Image
        string DownloadProfileImageURL(IUserDTO userDTO, ImageSize size = ImageSize.normal);
        string DownloadProfileImageInHttpURL(IUserDTO userDTO, ImageSize size = ImageSize.normal);
    }
}