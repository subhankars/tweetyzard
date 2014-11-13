using TweetinviCore.Enum;
using TweetinviCore.Interfaces.Models;

namespace TweetinviCore.Interfaces.DTO
{
    public interface IAccountSettingsDTO
    {
        string ScreenName { get; set; }
        PrivacyMode PrivacyMode { get; set; }
        Language Language { get; set; }

        bool AlwaysUseHttps { get; set; }
        bool DiscoverableByEmail { get; set; }
        bool GeoEnabled { get; set; }
        bool ShowAllInlineMedia { get; set; }
        bool UseCookiePersonalization { get; set; }

        ITimeZone TimeZone { get; set; }
        ITrendLocation TrendLocation { get; set; }

        bool SleepTimeEnabled { get; set; }
        int SleepTimeStartHour { get; set; }
        int SleepTimeEndHour { get; set; }
    }
}