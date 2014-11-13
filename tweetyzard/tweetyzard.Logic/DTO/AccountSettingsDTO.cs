using Newtonsoft.Json;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Models;
using TweetinviLogic.JsonConverters;

namespace TweetinviLogic.DTO
{
    public class AccountSettingsDTO : IAccountSettingsDTO
    {
        private class SleepTimeDTO
        {
            [JsonProperty("enabled")]
            public bool Enabled { get; set; }

            [JsonProperty("start_time")]
            [JsonConverter(typeof(JsonPropertyConverterRepository))]
            public int StartTime { get; set; }

            [JsonProperty("end_time")]
            [JsonConverter(typeof(JsonPropertyConverterRepository))]
            public int EndTime { get; set; }
        }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("protected")]
        [JsonConverter(typeof(JsonPropertyConverterRepository))]
        public PrivacyMode PrivacyMode { get; set; }

        [JsonProperty("language")]
        [JsonConverter(typeof(JsonPropertyConverterRepository))]
        public Language Language { get; set; }

        [JsonProperty("always_use_https")]
        public bool AlwaysUseHttps { get; set; }

        [JsonProperty("discoverable_by_email")]
        public bool DiscoverableByEmail { get; set; }

        [JsonProperty("geo_enabled")]
        public bool GeoEnabled { get; set; }

        [JsonProperty("show_all_inline_media")]
        public bool ShowAllInlineMedia { get; set; }

        [JsonProperty("use_cookie_personalization")]
        public bool UseCookiePersonalization { get; set; }

        [JsonProperty("time_zone")]
        [JsonConverter(typeof(JsonPropertyConverterRepository))]
        public ITimeZone TimeZone { get; set; }

        [JsonProperty("trend_location")]
        [JsonConverter(typeof(JsonPropertyConverterRepository))]
        public ITrendLocation TrendLocation { get; set; }

        [JsonProperty("sleep_time")]
        private SleepTimeDTO _sleepTime { get; set; }

        public bool SleepTimeEnabled
        {
            get { return _sleepTime.Enabled; }
            set { _sleepTime.Enabled = value; }
        }

        public int SleepTimeStartHour
        {
            get { return _sleepTime.StartTime; }
            set { _sleepTime.StartTime = value; }
        }

        public int SleepTimeEndHour
        {
            get { return _sleepTime.EndTime; }
            set { _sleepTime.EndTime = value; }
        }
    }
}