﻿namespace TweetinviCore.Interfaces.Models
{
    /// <summary>
    /// A hashtag is a keyword prefixed by # and representing a category of tweet
    /// </summary>
    public interface IHashtagEntity
    {
        /// <summary>
        /// HashTag name
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// The character positions the Hashtag was extracted from
        /// </summary>
        int[] Indices { get; set; }
    }
}