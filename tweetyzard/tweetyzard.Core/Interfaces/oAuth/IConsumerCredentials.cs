﻿namespace TweetinviCore.Interfaces.oAuth
{
    public interface IConsumerCredentials
    {
        /// <summary>
        /// Key identifying a specific consumer service
        /// </summary>
        string ConsumerKey { get; set; }

        /// <summary>
        /// Secret Key identifying a specific consumer service
        /// </summary>
        string ConsumerSecret { get; set; }
    }
}