using System;
using System.Globalization;
using System.Text;
using TweetinviControllers.Properties;
using TweetinviCore.Enum;
using TweetinviCore.Extensions;
using TweetinviCore.Helpers;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.Models;

namespace TweetinviControllers.Search
{
    public interface ISearchQueryGenerator
    {
        ITweetSearchParameters GenerateSearchTweetParameter(string query);
        ITweetSearchParameters GenerateSearchTweetParameter(IGeoCode geoCode);
        ITweetSearchParameters GenerateSearchTweetParameter(ICoordinates coordinates, int radius, DistanceMeasure measure);
        ITweetSearchParameters GenerateSearchTweetParameter(double longitude, double latitude, int radius, DistanceMeasure measure);

        string GetSearchTweetsQuery(string query);
        string GetSearchTweetsQuery(ITweetSearchParameters tweetSearchParameters);
    }

    public class SearchQueryGenerator : ISearchQueryGenerator
    {
        private readonly ISearchQueryValidator _searchQueryValidator;
        private readonly ITwitterStringFormatter _twitterStringFormatter;
        private readonly IUnityFactory<ITweetSearchParameters> _tweetSearchParameterFactory;

        public SearchQueryGenerator(
            ISearchQueryValidator searchQueryValidator,
            ITwitterStringFormatter twitterStringFormatter,
            IUnityFactory<ITweetSearchParameters> tweetSearchParameterFactory)
        {
            _searchQueryValidator = searchQueryValidator;
            _twitterStringFormatter = twitterStringFormatter;
            _tweetSearchParameterFactory = tweetSearchParameterFactory;
        }

        public string GetSearchTweetsQuery(string query)
        {
            var searchParameter = GenerateSearchTweetParameter(query);
            return GetSearchTweetsQuery(searchParameter);
        }

        public string GetSearchTweetsQuery(ITweetSearchParameters tweetSearchParameters)
        {
            if (tweetSearchParameters == null ||
                !_searchQueryValidator.IsSearchParameterValid(tweetSearchParameters) ||
                !_searchQueryValidator.IsSearchQueryValid(tweetSearchParameters.SearchQuery))
            {
                return null;
            }

            var formattedQuery = _twitterStringFormatter.TwitterEncode(tweetSearchParameters.SearchQuery);
            StringBuilder query = new StringBuilder(String.Format(Resources.Search_SearchTweets, formattedQuery));
            query.Append(String.Format(Resources.SearchParameter_ResultType, tweetSearchParameters.SearchType));

            if (_searchQueryValidator.IsGeoCodeValid(tweetSearchParameters.GeoCode))
            {
                string latitude = tweetSearchParameters.GeoCode.Coordinates.Latitude.ToString(CultureInfo.InvariantCulture);
                string longitude = tweetSearchParameters.GeoCode.Coordinates.Longitude.ToString(CultureInfo.InvariantCulture);
                string radius = tweetSearchParameters.GeoCode.Radius.ToString(CultureInfo.InvariantCulture);
                string measure = tweetSearchParameters.GeoCode.DistanceMeasure == DistanceMeasure.Kilometers ? "km" : "mi";
                query.Append(String.Format(Resources.SearchParameter_GeoCode, latitude, longitude, radius, measure, CultureInfo.InvariantCulture));
            }

            if (_searchQueryValidator.IsLangDefined(tweetSearchParameters.Lang))
            {
                query.Append(String.Format(Resources.SearchParameter_Lang, tweetSearchParameters.Lang.GetDescriptionAttribute()));
            }

            if (_searchQueryValidator.IsLocaleParameterValid(tweetSearchParameters.Locale))
            {
                query.Append(String.Format(tweetSearchParameters.Locale));
            }

            if (_searchQueryValidator.IsSinceIdDefined(tweetSearchParameters.SinceId))
            {
                query.Append(String.Format(Resources.QueryParameter_SinceId, tweetSearchParameters.SinceId));
            }

            if (_searchQueryValidator.IsMaxIdDefined(tweetSearchParameters.MaxId))
            {
                query.Append(String.Format(Resources.QueryParameter_MaxId, tweetSearchParameters.MaxId));
            }

            if (_searchQueryValidator.IsMaximumNumberOfResultsDefined(tweetSearchParameters.MaximumNumberOfResults))
            {
                query.Append(String.Format(Resources.QueryParameter_Count, tweetSearchParameters.MaximumNumberOfResults));
            }

            if (_searchQueryValidator.IsUntilDefined(tweetSearchParameters.Until))
            {
                query.Append(String.Format(Resources.SearchParameter_Until, tweetSearchParameters.Until.ToString("yyyy-MM-dd")));
            }

            return query.ToString();
        }

        public ITweetSearchParameters GenerateSearchTweetParameter(string query)
        {
            var searchParameter = _tweetSearchParameterFactory.Create();
            searchParameter.SearchQuery = query;
            return searchParameter;
        }

        public ITweetSearchParameters GenerateSearchTweetParameter(IGeoCode geoCode)
        {
            var searchParameter = _tweetSearchParameterFactory.Create();
            searchParameter.GeoCode = geoCode;
            return searchParameter;
        }

        public ITweetSearchParameters GenerateSearchTweetParameter(ICoordinates coordinates, int radius, DistanceMeasure measure)
        {
            var searchParameter = _tweetSearchParameterFactory.Create();
            searchParameter.SetGeoCode(coordinates, radius, measure);
            return searchParameter;
        }

        public ITweetSearchParameters GenerateSearchTweetParameter(double longitude, double latitude, int radius, DistanceMeasure measure)
        {
            var searchParameter = _tweetSearchParameterFactory.Create();
            searchParameter.SetGeoCode(longitude, latitude, radius, measure);
            return searchParameter;
        }
    }
}