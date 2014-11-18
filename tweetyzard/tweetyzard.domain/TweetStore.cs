//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace tweetyzard.domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class TweetStore
    {
        public int TweetId { get; set; }
        public decimal Id { get; set; }
        public string SearchPhrase { get; set; }
        public string CreatorName { get; set; }
        public string IdStr { get; set; }
        public string Text { get; set; }
        public Nullable<bool> Favorited { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<bool> Truncated { get; set; }
        public Nullable<decimal> InReplyToStatusId { get; set; }
        public string InReplyToStatusIdStr { get; set; }
        public Nullable<decimal> InReplyToUserId { get; set; }
        public string InReplyToUserIdStr { get; set; }
        public string InReplyToScreenName { get; set; }
        public Nullable<int> RetweetCount { get; set; }
        public Nullable<bool> Retweeted { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public Nullable<bool> GeoEnabled { get; set; }
        public string Url { get; set; }
        public Nullable<int> StatusesCount { get; set; }
        public Nullable<int> FollowersCount { get; set; }
        public Nullable<int> FriendsCount { get; set; }
        public Nullable<bool> Following { get; set; }
        public Nullable<bool> Protected { get; set; }
        public Nullable<bool> Verified { get; set; }
        public Nullable<bool> Notifications { get; set; }
        public string ProfileImageUrl { get; set; }
        public string ProfileImageUrlHttps { get; set; }
        public Nullable<bool> FollowRequestSent { get; set; }
        public Nullable<bool> DefaultProfile { get; set; }
        public Nullable<bool> DefaultProfileImage { get; set; }
        public Nullable<int> FavouritesCount { get; set; }
        public Nullable<int> ListedCount { get; set; }
        public string ProfileSidebarFillColor { get; set; }
        public string ProfileSidebarBorderColor { get; set; }
        public string ProfileBackgroundTitle { get; set; }
        public string ProfileBackgroundColor { get; set; }
        public string ProfileBackgroundImageUrl { get; set; }
        public string ProfileBackgroundImageUrlHttps { get; set; }
        public string ProfileTextColor { get; set; }
        public string ProfileLinkColor { get; set; }
        public Nullable<bool> ProfileUseBackgroundImage { get; set; }
        public Nullable<bool> IsTranslator { get; set; }
        public Nullable<bool> ShowAllInlineMedia { get; set; }
        public Nullable<bool> ContributorsEnabled { get; set; }
        public Nullable<int> UtcOffset { get; set; }
        public string TimeZone { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<double> Latitude { get; set; }
    }
}
