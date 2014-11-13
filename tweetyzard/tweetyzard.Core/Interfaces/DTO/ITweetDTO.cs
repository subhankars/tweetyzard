using System;
using TweetinviCore.Interfaces.Models;

namespace TweetinviCore.Interfaces.DTO
{
    public interface ITweetDTO
    {
        bool IsTweetPublished { get; set; }
        bool IsTweetDestroyed { get; set; }

        long Id { get; set; }

        string IdStr { get; set; }

        string Text { get; set; }

        bool Favorited { get; set; }

        IUserDTO Creator { get; set; }

        ICoordinates Coordinates { get; set; }

        ITweetEntities Entities { get; set; }

        DateTime CreatedAt { get; set; }

        bool Truncated { get; set; }

        long? InReplyToStatusId { get; set; }

        string InReplyToStatusIdStr { get; set; }

        long? InReplyToUserId { get; set; }

        string InReplyToUserIdStr { get; set; }

        string InReplyToScreenName { get; set; }

        int RetweetCount { get; set; }

        bool Retweeted { get; set; }

        ITweetDTO RetweetedTweetDTO { get; set; }

        bool PossiblySensitive { get; set; }

        int[] ContributorsIds { get; set; }

        string Source { get; set; }

        string PlaceId { get; set; }

        IPlace Place { get; set; }
    }
}