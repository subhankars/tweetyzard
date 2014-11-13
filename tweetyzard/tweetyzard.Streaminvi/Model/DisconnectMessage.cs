using Newtonsoft.Json;
using TweetinviCore.Interfaces.DTO;

namespace Streaminvi.Model
{
    public class DisconnectMessage : IDisconnectMessage
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("stream_name")]
        public string StreamName { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}