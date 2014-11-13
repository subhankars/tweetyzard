using Newtonsoft.Json;
using TweetinviCore.Interfaces.Models;

namespace Streaminvi.Model
{
    public class WarningMessageFallingBehind : WarningMessage, IWarningMessageFallingBehind
    {
        [JsonProperty("percent_full")]
        public int PercentFull { get; set; }
    }
}