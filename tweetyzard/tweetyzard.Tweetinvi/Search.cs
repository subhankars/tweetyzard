using System;
using System.Collections.Generic;
using System.Linq;
using TweetinviControllers.Search;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Models;

namespace Tweetinvi
{
    public static class Search
    {
        [ThreadStatic]
        private static ISearchController _searchController;
        public static ISearchController SearchController
        {
            get
            {
                if (_searchController == null)
                {
                    Initialize();
                }
                
                return _searchController;
            }
        }

        [ThreadStatic]
        private static ISearchQueryGenerator _searchQueryGenerator;
        public static ISearchQueryGenerator SearchQueryGenerator
        {
            get
            {
                if (_searchQueryGenerator == null)
                {
                    Initialize();
                }

                return _searchQueryGenerator;
            }
        }

        static Search()
        {
            Initialize();
        }

        private static void Initialize()
        {
            _searchController = TweetinviContainer.Resolve<ISearchController>();
            _searchQueryGenerator = TweetinviContainer.Resolve<ISearchQueryGenerator>();
        }

        public static List<ITweet> SearchTweets(string searchQuery)
        {
            return SearchController.SearchTweets(searchQuery).ToList();
        }

        public static List<ITweet> SearchTweets(ITweetSearchParameters tweetSearchParameters)
        {
            return SearchController.SearchTweets(tweetSearchParameters).ToList();
        }

        public static ITweetSearchParameters GenerateSearchTweetParameter(string query)
        {
            return SearchQueryGenerator.GenerateSearchTweetParameter(query);
        }

        public static ITweetSearchParameters GenerateSearchTweetParameter(IGeoCode geoCode)
        {
            return SearchQueryGenerator.GenerateSearchTweetParameter(geoCode);
        }

        public static ITweetSearchParameters GenerateSearchTweetParameter(ICoordinates coordinates, int radius, DistanceMeasure measure)
        {
            return SearchQueryGenerator.GenerateSearchTweetParameter(coordinates, radius, measure);
        }

        public static ITweetSearchParameters GenerateSearchTweetParameter(double longitude, double latitude, int radius, DistanceMeasure measure)
        {
            return SearchQueryGenerator.GenerateSearchTweetParameter(longitude, latitude, radius, measure);   
        }

        public static IEnumerable<ITweet> SearchDirectRepliesTo(ITweet tweet)
        {
            return SearchController.SearchDirectRepliesTo(tweet);
        }

        public static IEnumerable<ITweet> SearchRepliesTo(ITweet tweet, bool recursiveReplies)
        {
            return SearchController.SearchRepliesTo(tweet, recursiveReplies);
        }
    }
}