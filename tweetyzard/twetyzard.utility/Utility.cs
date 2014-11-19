using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using TweetinviCore.Interfaces;
using tweetyzard.domain;

namespace tweetyzard.utility
{
    public static class Utility
    {
        public static TweetStore MapStreamedTweetToTweetDomain(ITweet streamedTweet, string searchPhrase)
        {
          
            TweetStore tweetDomain = new TweetStore();

            if (streamedTweet == null)
            {
                throw new ArgumentNullException("ITweet is null");
            }

            
            tweetDomain = new TweetStore();

            tweetDomain.SearchPhrase = searchPhrase;

            if (streamedTweet.Creator != null)
            {
                tweetDomain.CreatorName = streamedTweet.Creator.Name;
            }

            if (streamedTweet.TweetDTO != null)
            {
                tweetDomain.Id = streamedTweet.TweetDTO.Id;
                tweetDomain.IdStr = streamedTweet.TweetDTO.IdStr;
                tweetDomain.Text = streamedTweet.TweetDTO.Text;
                tweetDomain.Favorited = streamedTweet.TweetDTO.Favorited;
                tweetDomain.CreatedAt = streamedTweet.CreatedAt;
                tweetDomain.Truncated = streamedTweet.Truncated;
                tweetDomain.InReplyToStatusId = streamedTweet.InReplyToStatusId;
                tweetDomain.InReplyToStatusIdStr = streamedTweet.InReplyToStatusIdStr;
                tweetDomain.InReplyToUserId = streamedTweet.InReplyToUserId;
                tweetDomain.InReplyToUserIdStr = streamedTweet.InReplyToUserIdStr;
                tweetDomain.InReplyToScreenName = streamedTweet.InReplyToScreenName;
                tweetDomain.RetweetCount = streamedTweet.RetweetCount;
                tweetDomain.Retweeted = streamedTweet.Retweeted;

                if (streamedTweet.TweetDTO.Creator != null)
                {
                    tweetDomain.Description = streamedTweet.TweetDTO.Creator.Description;
                    tweetDomain.Location = streamedTweet.TweetDTO.Creator.Location;
                    tweetDomain.GeoEnabled = streamedTweet.TweetDTO.Creator.GeoEnabled;
                    tweetDomain.Url = streamedTweet.TweetDTO.Creator.Url;
                    tweetDomain.StatusesCount = streamedTweet.TweetDTO.Creator.StatusesCount;
                    tweetDomain.FollowersCount = streamedTweet.TweetDTO.Creator.FollowersCount;
                    tweetDomain.FriendsCount = streamedTweet.TweetDTO.Creator.FriendsCount;
                    tweetDomain.Following = streamedTweet.TweetDTO.Creator.Following;
                    tweetDomain.Protected = streamedTweet.TweetDTO.Creator.Protected;
                    tweetDomain.Verified = streamedTweet.TweetDTO.Creator.Verified;
                    tweetDomain.Notifications = streamedTweet.TweetDTO.Creator.Notifications;
                    tweetDomain.ProfileImageUrl = streamedTweet.TweetDTO.Creator.ProfileImageUrl;
                    tweetDomain.ProfileImageUrlHttps = streamedTweet.TweetDTO.Creator.ProfileImageUrlHttps;
                    tweetDomain.FollowRequestSent = streamedTweet.TweetDTO.Creator.FollowRequestSent;
                    tweetDomain.DefaultProfile = streamedTweet.TweetDTO.Creator.DefaultProfile;
                    tweetDomain.DefaultProfileImage = streamedTweet.TweetDTO.Creator.DefaultProfileImage;
                    tweetDomain.FavouritesCount = streamedTweet.TweetDTO.Creator.FavouritesCount;
                    tweetDomain.ListedCount = streamedTweet.TweetDTO.Creator.ListedCount;
                    tweetDomain.ProfileSidebarFillColor = streamedTweet.TweetDTO.Creator.ProfileSidebarFillColor;
                    tweetDomain.ProfileSidebarBorderColor = streamedTweet.TweetDTO.Creator.ProfileSidebarBorderColor;
                    tweetDomain.ProfileBackgroundTitle = streamedTweet.TweetDTO.Creator.ProfileBackgroundTitle;
                    tweetDomain.ProfileBackgroundColor = streamedTweet.TweetDTO.Creator.ProfileBackgroundColor;
                    tweetDomain.ProfileBackgroundImageUrl = streamedTweet.TweetDTO.Creator.ProfileBackgroundImageUrl;
                    tweetDomain.ProfileBackgroundImageUrlHttps = streamedTweet.TweetDTO.Creator.ProfileBackgroundImageUrlHttps;
                    tweetDomain.ProfileTextColor = streamedTweet.TweetDTO.Creator.ProfileTextColor;
                    tweetDomain.ProfileLinkColor = streamedTweet.TweetDTO.Creator.ProfileLinkColor;
                    tweetDomain.ProfileUseBackgroundImage = streamedTweet.TweetDTO.Creator.ProfileUseBackgroundImage;
                    tweetDomain.IsTranslator = streamedTweet.TweetDTO.Creator.IsTranslator;
                    tweetDomain.ShowAllInlineMedia = streamedTweet.TweetDTO.Creator.ShowAllInlineMedia;
                    tweetDomain.ContributorsEnabled = streamedTweet.TweetDTO.Creator.ContributorsEnabled;
                    tweetDomain.UtcOffset = streamedTweet.TweetDTO.Creator.UtcOffset;
                    tweetDomain.TimeZone = streamedTweet.TweetDTO.Creator.TimeZone;
                }

                if (streamedTweet.TweetDTO.Coordinates != null)
                {
                    tweetDomain.Longitude = streamedTweet.TweetDTO.Coordinates.Longitude;
                    tweetDomain.Latitude = streamedTweet.TweetDTO.Coordinates.Latitude;
                }
            }



            return tweetDomain;
        }

        public static DataTable ConvertToDataTable<T>(this IList<T> listData, string tableName)
        {
            PropertyInfo[] propInfo = null;

            if (listData != null)
            {
                var sortedProperties = listData[0].GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .OrderBy(x => x.MetadataToken);
                propInfo = sortedProperties.ToArray();
            }

            using (DataTable table = new DataTable())
            {
                table.Locale = CultureInfo.InvariantCulture;
                table.TableName = tableName;
                long count = propInfo.LongLength;
                for (int i = 0; i < count; i++)
                {
                    PropertyInfo prop = propInfo[i];
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }

                object[] values = new object[table.Columns.Count];
                if (listData != null)
                {
                    foreach (T item in listData)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            values[i] = propInfo[i].GetValue(item) ?? DBNull.Value;
                        }
                        table.Rows.Add(values);
                    }
                }

                return table;
            }
        }

    }
}
