using TweetinviCore.Enum;

namespace TweetinviCore.Interfaces.DTO
{
    public interface IUserDTO : IUserIdDTO
    {
        string Name { get; set; }

        ITweetDTO Status { get; set; }

        string Description { get; set; }

        string Location { get; set; }

        bool GeoEnabled { get; set; }

        string Url { get; set; }

        Language Language { get; set; }

        int StatusesCount { get; set; }

        int FollowersCount { get; set; }

        int FriendsCount { get; set; }

        bool Following { get; set; }

        bool Protected { get; set; }

        bool Verified { get; set; }

        bool Notifications { get; set; }

        string ProfileImageUrl { get; set; }

        string ProfileImageUrlHttps { get; set; }

        bool FollowRequestSent { get; set; }

        bool DefaultProfile { get; set; }

        bool DefaultProfileImage { get; set; }

        int? FavouritesCount { get; set; }

        int? ListedCount { get; set; }

        string ProfileSidebarFillColor { get; set; }

        string ProfileSidebarBorderColor { get; set; }

        string ProfileBackgroundTitle { get; set; }

        string ProfileBackgroundColor { get; set; }

        string ProfileBackgroundImageUrl { get; set; }

        string ProfileBackgroundImageUrlHttps { get; set; }

        string ProfileTextColor { get; set; }

        string ProfileLinkColor { get; set; }

        bool ProfileUseBackgroundImage { get; set; }

        bool IsTranslator { get; set; }

        bool ShowAllInlineMedia { get; set; }

        bool ContributorsEnabled { get; set; }

        int? UtcOffset { get; set; }

        string TimeZone { get; set; }
    }
}