using Newtonsoft.Json;
using TweetinviCore.Interfaces.Models;

namespace TweetinviLogic.Model
{
    public class WoeIdLocation : IWoeIdLocation
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("woeid")]
        public long WoeId { get; set; }
    }
}