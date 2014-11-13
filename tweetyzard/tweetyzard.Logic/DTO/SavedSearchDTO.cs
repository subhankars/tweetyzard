﻿using System;
using Newtonsoft.Json;
using TweetinviCore.Interfaces.DTO;
using TweetinviLogic.JsonConverters;

namespace TweetinviLogic.DTO
{
    public class SavedSearchDTO : ISavedSearchDTO
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("IdStr")]
        public string IdStr { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(JsonPropertyConverterRepository))]
        public DateTime CreatedAt { get; set; }
    }
}