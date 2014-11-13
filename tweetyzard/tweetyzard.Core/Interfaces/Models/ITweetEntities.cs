using System.Collections.Generic;

namespace TweetinviCore.Interfaces.Models
{
    /// <summary>
    /// Entities are special elements that can be given to an ITweet
    /// </summary>
    public interface ITweetEntities
    {
        /// <summary>
        /// Collection of urls associated with a tweet
        /// </summary>
        List<IUrlEntity> Urls { get; set; }

        /// <summary>
        /// Collection of medias associated with a tweet
        /// </summary>
        List<IMediaEntity> Medias { get; set; }

        /// <summary>
        /// Collection of tweets mentioning this tweet
        /// </summary>
        List<IUserMentionEntity> UserMentions { get; set; }

        /// <summary>
        /// Collection of hashtags associated with a Tweet
        /// </summary>
        List<IHashtagEntity> Hashtags { get; set; }
    }
}