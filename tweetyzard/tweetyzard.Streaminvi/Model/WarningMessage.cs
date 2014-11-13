using Newtonsoft.Json;
using TweetinviCore.Interfaces.Models;

namespace Streaminvi.Model
{
    public class WarningMessage : IWarningMessage
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}