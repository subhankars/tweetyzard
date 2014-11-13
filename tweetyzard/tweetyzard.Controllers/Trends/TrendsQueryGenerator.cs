using System;
using TweetinviControllers.Properties;
using TweetinviCore.Interfaces.Models;

namespace TweetinviControllers.Trends
{
    public interface ITrendsQueryGenerator
    {
        string GetPlaceTrendsAtQuery(long woeid);
        string GetPlaceTrendsAtQuery(IWoeIdLocation woeIdLocation);
    }

    public class TrendsQueryGenerator : ITrendsQueryGenerator
    {
        public string GetPlaceTrendsAtQuery(long woeid)
        {
            return String.Format(Resources.Trends_GetTrendsFromWoeId, woeid);
        }

        public string GetPlaceTrendsAtQuery(IWoeIdLocation woeIdLocation)
        {
            if (woeIdLocation == null)
            {
                throw new ArgumentException("WoeId cannot be null");
            }

            return GetPlaceTrendsAtQuery(woeIdLocation.WoeId);
        }
    }
}