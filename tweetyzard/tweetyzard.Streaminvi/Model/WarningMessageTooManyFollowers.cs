using System.Collections.Generic;
using Newtonsoft.Json;
using TweetinviCore.Interfaces.Models;

namespace Streaminvi.Model
{
    public class WarningMessageTooManyFollowers : WarningMessage, IWarningMessageTooManyFollowers
    {
        [JsonProperty("user_id")]
        public IEnumerable<long> UserIds { get; set; }
    }
}