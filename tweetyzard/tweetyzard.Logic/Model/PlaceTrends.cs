using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TweetinviCore.Interfaces.Models;
using TweetinviLogic.JsonConverters;

namespace TweetinviLogic.Model
{
    public class PlaceTrends : IPlaceTrends
    {
        [JsonProperty("as_of")]
        [JsonConverter(typeof(JsonPropertyConverterRepository))]
        public DateTime AsOf { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(JsonPropertyConverterRepository))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("locations")]
        public List<IWoeIdLocation> woeIdLocations { get; set; }

        [JsonProperty("trends")]
        public List<ITrend> Trends { get; set; }
    }
}