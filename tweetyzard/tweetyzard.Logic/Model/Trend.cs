﻿using Newtonsoft.Json;
using TweetinviCore.Interfaces.Models;

namespace TweetinviLogic.Model
{
    public class Trend : ITrend
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("promoted_content")]
        public string PromotedContent { get; set; }
    }
}