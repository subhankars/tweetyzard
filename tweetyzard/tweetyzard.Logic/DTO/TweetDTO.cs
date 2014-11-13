using System;
using Newtonsoft.Json;
using TweetinviCore;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Models;
using TweetinviLogic.JsonConverters;

namespace TweetinviLogic.DTO
{
    public class TweetDTO : ITweetDTO
    {
        private long _id;
        
        public bool IsTweetPublished { get; set; }
        public bool IsTweetDestroyed { get; set; }

        public TweetDTO()
        {
            _id = TweetinviConstants.DEFAULT_ID;
        }

        [JsonProperty("id")]
        [JsonConverter(typeof(JsonPropertyConverterRepository))]
        public long Id
        {
            get { return _id; }
            set
            {
                _id = value;
                if (_id != TweetinviConstants.DEFAULT_ID)
                {
                    IsTweetPublished = true;
                }
            }
        }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("favorited")]
        public bool Favorited { get; set; }

        [JsonProperty("user")]
        [JsonConverter(typeof(JsonPropertyConverterRepository))]
        public IUserDTO Creator { get; set; }

        [JsonProperty("coordinates")]
        [JsonConverter(typeof(JsonPropertyConverterRepository))]
        public ICoordinates Coordinates { get; set; }

        [JsonProperty("entities")]
        [JsonConverter(typeof(JsonPropertyConverterRepository))]
        public ITweetEntities Entities { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(JsonTwitterDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("truncated")]
        public bool Truncated { get; set; }

        [JsonProperty("in_reply_to_status_id")]
        public long? InReplyToStatusId { get; set; }

        [JsonProperty("in_reply_to_status_id_str")]
        public string InReplyToStatusIdStr { get; set; }

        [JsonProperty("in_reply_to_user_id")]
        public long? InReplyToUserId { get; set; }

        [JsonProperty("in_reply_to_user_id_str")]
        public string InReplyToUserIdStr { get; set; }

        [JsonProperty("in_reply_to_screen_name")]
        public string InReplyToScreenName { get; set; }

        [JsonProperty("retweet_count")]
        public int RetweetCount { get; set; }

        [JsonProperty("retweeted")]
        public bool Retweeted { get; set; }

        [JsonProperty("retweeted_status")]
        [JsonConverter(typeof(JsonPropertyConverterRepository))]
        public ITweetDTO RetweetedTweetDTO { get; set; }

        [JsonProperty("possibly_sensitive")]
        public bool PossiblySensitive { get; set; }

        [JsonProperty("contributorsIds")]
        public int[] ContributorsIds { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("place")]
        public IPlace Place { get; set; }

        [JsonIgnore]
        public string PlaceId { get; set; }
    }
}