﻿using Newtonsoft.Json;
using TweetinviCore.Interfaces.DTO;
using TweetinviLogic.JsonConverters;

namespace TweetinviLogic.DTO
{
    public class OEmbedTweetDTO : IOEmbedTweetDTO
    {
        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        [JsonProperty("author_url")]
        public string AuthorURL { get; set; }

        [JsonProperty("html")]
        public string HTML { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("provider_url")]
        public string ProviderURL { get; set; }

        [JsonProperty("width")]
        [JsonConverter(typeof(JsonPropertyConverterRepository))]
        public double Width { get; set; }

        [JsonProperty("height")]
        [JsonConverter(typeof(JsonPropertyConverterRepository))]
        public double Height { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("cache_age")]
        public string CacheAge { get; set; }
    }
}