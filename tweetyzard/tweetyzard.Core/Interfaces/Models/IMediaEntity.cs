using System;
using System.Collections.Generic;

namespace TweetinviCore.Interfaces.Models
{
    /// <summary>
    /// Media element posted in Twitter
    /// </summary>
    public interface IMediaEntity
    {
        /// <summary>
        /// Media Id
        /// </summary>
        long? Id { get; set; }

        /// <summary>
        /// Media Id as a string
        /// </summary>
        string IdStr { get; set; }

        /// <summary>
        /// Url of the media
        /// </summary>
        string MediaURL { get; set; }

        /// <summary>
        /// Secured Url of the media
        /// </summary>
        string MediaURLHttps { get; set; }

        string URL { get; set; }

        string DisplayURL { get; set; }

        string ExpandedURL { get; set; }

        /// <summary>
        /// Type of Media
        /// </summary>
        string MediaType { get; set; }

        int[] Indices { get; set; }

        /// <summary>
        /// Dimensions related with the different possible views of 
        /// a same Media element
        /// </summary>
        Dictionary<String, IMediaEntitySize> Sizes { get; set; } 
    }
}