using System;
using System.Collections.Generic;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Models;

namespace TweetinviCore.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface ITweet : IEquatable<ITweet>
    {
        #region Twitter API Properties

        /// <summary>
        /// id of the Tweet
        /// </summary>
        long Id { get; set; }

        /// <summary>
        /// Id of tweet as a string
        /// </summary>
        string IdStr { get; set; }

        /// <summary>
        /// Formatted text of the tweet
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Coordinates of the location from where the tweet has been sent
        /// </summary>
        ICoordinates Coordinates { get; set; }

        /// <summary>
        /// source field
        /// </summary>
        string Source { get; set; }

        /// <summary>
        /// Whether a tweet
        /// </summary>
        bool Truncated { get; }

        /// <summary>
        /// In_reply_to_status_id
        /// </summary>
        long? InReplyToStatusId { get; set; }

        /// <summary>
        /// In_reply_to_status_id_str
        /// </summary>
        string InReplyToStatusIdStr { get; set; }

        /// <summary>
        /// In_reply_to_user_id
        /// </summary>
        long? InReplyToUserId { get; set; }

        /// <summary>
        /// In_reply_to_user_id_str
        /// </summary>
        string InReplyToUserIdStr { get; set; }

        /// <summary>
        /// In_reply_to_screen_name
        /// </summary>
        string InReplyToScreenName { get; set; }

        /// <summary>
        /// User who created the Tweet
        /// </summary>
        IUser Creator { get; set; }

        /// <summary>
        /// Ids of the users who contributed in the Tweet
        /// </summary>
        int[] ContributorsIds { get; set; }

        /// <summary>
        /// Number of retweets related with this tweet
        /// </summary>
        int RetweetCount { get; }

        /// <summary>
        /// Entities contained in the tweet
        /// </summary>
        ITweetEntities Entities { get; set; }

        /// <summary>
        /// Is the tweet favourited
        /// </summary>
        bool Favourited { get; }

        /// <summary>
        /// Has the tweet been retweeted
        /// </summary>
        bool Retweeted { get; }

        /// <summary>
        /// Is the tweet potentialy sensitive
        /// </summary>
        bool PossiblySensitive { get; }

        #endregion

        #region Tweetinvi API Properties

        ITweetDTO TweetDTO { get; set; }

        /// <summary>
        /// Determine the length of the Text using Twitter algorithm
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Date at which the Tweet has been created in the application
        /// </summary>
        DateTime CreatedAt { get; }

        /// <summary>
        /// Collection of hashtags associated with a Tweet
        /// </summary>
        List<IHashtagEntity> Hashtags { get; set; }

        /// <summary>
        /// Collection of urls associated with a tweet
        /// </summary>
        List<IUrlEntity> Urls { get; set; }

        /// <summary>
        /// Collection of medias associated with a tweet
        /// </summary>
        List<IMediaEntity> Media { get; set; }

        /// <summary>
        /// Collection of tweets mentioning this tweet
        /// </summary>
        List<IUserMentionEntity> UserMentions { get; set; }

        /// <summary>
        /// Collection of tweets retweeting this tweet
        /// </summary>
        List<ITweet> Retweets { get; set; }

        bool IsRetweet { get; }

        /// <summary>
        /// If the tweet is a retweet this field provides
        /// the tweet that it retweeted
        /// </summary>
        ITweet RetweetedTweet { get; }

        /// <summary>
        /// Inform us if this tweet has been published on Twitter
        /// </summary>
        bool IsTweetPublished { get; }

        /// <summary>
        /// Inform us if this tweet was destroyed
        /// </summary>
        bool IsTweetDestroyed { get; }

        #endregion

        int TweetRemainingCharacters();

        #region Publish

        bool Publish();

        // Publish Reply
        ITweet PublishReply(string text);

        bool PublishReply(ITweet replyTweet);

        // Publish In Reply To
        /// <summary>
        /// Publish a Tweet created from the API
        /// </summary>
        /// <param name="replyToTweetId">Tweet id of the tweet we reply to</param>
        /// <returns>Whether the Tweet has successfully been sent</returns>
        bool PublishInReplyTo(long replyToTweetId);

        /// <summary>
        /// Publish a Tweet created from the API
        /// </summary>
        /// <param name="replyToTweet">Tweet we reply to</param>
        /// <returns>Whether the Tweet has successfully been sent</returns>
        bool PublishInReplyTo(ITweet replyToTweet);

        /// <summary>
        /// Publish a Tweet created from the API
        /// </summary>
        /// <param name="coordinates">Coordinates</param>
        /// <returns>Whether the Tweet has successfully been sent</returns>
        bool PublishWithGeo(ICoordinates coordinates);

        /// <summary>
        /// Publish a Tweet created from the API
        /// </summary>
        /// <param name="latitude">Lattitude</param>
        /// <param name="longitude">Longitude</param>
        /// <returns>Whether the Tweet has successfully been sent</returns>
        bool PublishWithGeo(double longitude, double latitude);

        /// <summary>
        /// Publish a Tweet created from the API
        /// </summary>
        /// <param name="latitude">Lattitude</param>
        /// <param name="longitude">Longitude</param>
        /// <param name="replyToTweetId">Tweet id of the tweet we reply to</param>
        /// <returns>Whether the Tweet has successfully been sent</returns>
        bool PublishWithGeoInReplyTo(double latitude, double longitude, long replyToTweetId);

        /// <summary>
        /// Publish a Tweet created from the API
        /// </summary>
        /// <param name="latitude">Lattitude</param>
        /// <param name="longitude">Longitude</param>
        /// <param name="replyToTweet">Tweet we want reply to</param>
        /// <returns>Whether the Tweet has successfully been sent</returns>
        bool PublishWithGeoInReplyTo(double latitude, double longitude, ITweet replyToTweet);

        #endregion

        #region Favourites

        /// <summary>
        /// Favorites the tweet
        /// </summary>
        void Favourite();

        /// <summary>
        /// Remove the tweet from favourites
        /// </summary>
        void UnFavourite();

        #endregion

        IOEmbedTweet GenerateOEmbedTweet();

        bool Destroy();

        // Retweets

        /// <summary>
        /// Retweet from the current Token User
        /// </summary>
        /// <returns>If the retweet has been successfully publish</returns>
        ITweet PublishRetweet();

        List<ITweet> GetRetweets();
    }
}