﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TweetinviCore.Extensions;
using TweetinviCore.Interfaces.Models;

namespace Streaminvi.Helpers
{
    public class QueryGeneratorHelper
    {
        public static string GenerateFilterTrackRequest(List<string> tracks)
        {
            if (tracks == null || !tracks.Any())
            {
                return String.Empty;
            }

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("track=");
            for (int i = 0; i < tracks.Count - 1; ++i)
            {
                queryBuilder.Append(Uri.EscapeDataString(String.Format("{0},", tracks.ElementAt(i))));
            }

            queryBuilder.Append(Uri.EscapeDataString(tracks.ElementAt(tracks.Count - 1)));

            return queryBuilder.ToString();
        }

        public static string GenerateFilterFollowRequest(List<long?> followUserIds)
        {
            if (followUserIds == null || !followUserIds.Any())
            {
                return String.Empty;
            }

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("follow=");
            for (int i = 0; i < followUserIds.Count - 1; ++i)
            {
                queryBuilder.Append(Uri.EscapeDataString(String.Format("{0},", followUserIds.ElementAt(i))));
            }

            queryBuilder.Append(followUserIds.ElementAt(followUserIds.Count - 1));

            return queryBuilder.ToString();
        }

        private static string GenerateLocationParameters(ILocation location, bool isLastLocation)
        {
            var maxLongitude = Math.Max(location.Coordinate1.Longitude, location.Coordinate2.Longitude);
            var maxLatitude = Math.Max(location.Coordinate1.Latitude, location.Coordinate2.Latitude);

            var minLongitude = Math.Min(location.Coordinate1.Longitude, location.Coordinate2.Longitude);
            var minLatitude = Math.Min(location.Coordinate1.Latitude, location.Coordinate2.Latitude);

            return String.Format("{0},{1},{2},{3}{4}", minLongitude.ToString(CultureInfo.InvariantCulture),
                                                       minLatitude.ToString(CultureInfo.InvariantCulture),
                                                       maxLongitude.ToString(CultureInfo.InvariantCulture), 
                                                       maxLatitude.ToString(CultureInfo.InvariantCulture),
                                                       isLastLocation ? "" : ",");
        }

        public static string GenerateFilterLocationRequest(List<ILocation> locations)
        {
            if (locations == null || !locations.Any())
            {
                return String.Empty;
            }

            StringBuilder queryBuilder = new StringBuilder();
            // queryBuilder.Append("locations=");
            for (int i = 0; i < locations.Count - 1; ++i)
            {
                queryBuilder.Append(GenerateLocationParameters(locations[i], false));
            }

            queryBuilder.Append(GenerateLocationParameters(locations[locations.Count - 1], true));

            return String.Format("locations={0}", StringFormater.UrlEncode(queryBuilder.ToString()));
        }
    }
}