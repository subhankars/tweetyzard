using System.Collections.Generic;
using Newtonsoft.Json;
using TweetinviCore.Interfaces.DTO;

namespace Streaminvi.Model
{
    public class TweetWitheldInfo : ITweetWitheldInfo
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("withheld_in_countries")]
        public IEnumerable<string> WitheldInCountries { get; set; }
    }
}