using System;
using Newtonsoft.Json;
using TweetinviCore.Interfaces.Credentials;

namespace TweetinviCore.Interfaces.Models
{
    public class TokenRateLimit : ITokenRateLimit
    {
        private long _reset;
        
        [JsonProperty("remaining")]
        public int Remaining { get; private set; }

        [JsonProperty("reset")]
        public long Reset
        {
            get { return _reset; }
            set
            {
                _reset = value;
                ResetDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                ResetDateTime = ResetDateTime.AddSeconds(_reset).ToLocalTime();
                ResetDateTimeInSeconds = (ResetDateTime - DateTime.Now).TotalSeconds;
            }
        }

        [JsonProperty("limit")]
        public int Limit { get; private set; }

        [JsonIgnore]
        public double ResetDateTimeInSeconds { get; private set; }

        [JsonIgnore]
        public DateTime ResetDateTime { get; private set; }
    }
}