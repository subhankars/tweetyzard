using System;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Models;

namespace tweetyzard.winform
{
    public class CustomTweetDomain
    {
        ////Search phrase
        public string SearchPhrase { get; set; }

        public string CreatorName { get; set; }

        //Tweet related info
        public long Id { get; set; }

        public string IdStr { get; set; }

        public string Text { get; set; }

        public bool Favorited { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Truncated { get; set; }

        public long? InReplyToStatusId { get; set; }

        public string InReplyToStatusIdStr { get; set; }

        public long? InReplyToUserId { get; set; }

        public string InReplyToUserIdStr { get; set; }

        public string InReplyToScreenName { get; set; }

        public int RetweetCount { get; set; }

        public bool Retweeted { get; set; }

        // User related info
        public string Description { get; set; }

        public string Location { get; set; }

        public bool GeoEnabled { get; set; }

        public string Url { get; set; }

        public int StatusesCount { get; set; }

        public int FollowersCount { get; set; }

        public int FriendsCount { get; set; }

        public bool Following { get; set; }

        public bool Protected { get; set; }

        public bool Verified { get; set; }

        public bool Notifications { get; set; }

        public string ProfileImageUrl { get; set; }

        public string ProfileImageUrlHttps { get; set; }

        public bool FollowRequestSent { get; set; }

        public bool DefaultProfile { get; set; }

        public bool DefaultProfileImage { get; set; }

        public int? FavouritesCount { get; set; }

        public int? ListedCount { get; set; }

        public string ProfileSidebarFillColor { get; set; }

        public string ProfileSidebarBorderColor { get; set; }

        public string ProfileBackgroundTitle { get; set; }

        public string ProfileBackgroundColor { get; set; }

        public string ProfileBackgroundImageUrl { get; set; }

        public string ProfileBackgroundImageUrlHttps { get; set; }

        public string ProfileTextColor { get; set; }

        public string ProfileLinkColor { get; set; }

        public bool ProfileUseBackgroundImage { get; set; }

        public bool IsTranslator { get; set; }

        public bool ShowAllInlineMedia { get; set; }

        public bool ContributorsEnabled { get; set; }

        public int? UtcOffset { get; set; }

        public string TimeZone { get; set; }

        //// Location Info
        public double Longitude { get; set; }

        public double Latitude { get; set; }


    }
}
