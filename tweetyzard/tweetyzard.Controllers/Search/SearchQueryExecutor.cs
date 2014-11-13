using System;
using System.Collections.Generic;
using System.Linq;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Wrappers;

namespace TweetinviControllers.Search
{
    public interface ISearchQueryExecutor
    {
        IEnumerable<ITweetDTO> SearchTweets(string query);
        IEnumerable<ITweetDTO> SearchTweets(ITweetSearchParameters tweetSearchParameters);
        IEnumerable<ITweetDTO> SearchRepliesTo(ITweetDTO tweetDTO, bool getRecursiveReplies);
    }

    public class SearchQueryExecutor : ISearchQueryExecutor
    {
        private readonly ISearchQueryGenerator _searchQueryGenerator;
        private readonly IJObjectStaticWrapper _wrapper;
        private readonly ITwitterAccessor _twitterAccessor;
        private readonly IUnityFactory<ITweetSearchParameters> _tweetSearchParameterUnityFactory;

        public SearchQueryExecutor(
            ISearchQueryGenerator searchQueryGenerator,
            IJObjectStaticWrapper wrapper,
            ITwitterAccessor twitterAccessor,
            IUnityFactory<ITweetSearchParameters> tweetSearchParameterUnityFactory)
        {
            _searchQueryGenerator = searchQueryGenerator;
            _wrapper = wrapper;
            _twitterAccessor = twitterAccessor;
            _tweetSearchParameterUnityFactory = tweetSearchParameterUnityFactory;
        }

        public IEnumerable<ITweetDTO> SearchTweets(string searchQuery)
        {
            string httpQuery = _searchQueryGenerator.GetSearchTweetsQuery(searchQuery);
            return GetTweetDTOsFromSearch(httpQuery);
        }

        public IEnumerable<ITweetDTO> SearchTweets(ITweetSearchParameters tweetSearchParameters)
        {
            if (tweetSearchParameters == null)
            {
                throw new ArgumentException("TweetSearch Parameters cannot be null");
            }

            IEnumerable<ITweetDTO> result;
            if (tweetSearchParameters.MaximumNumberOfResults > 100)
            {
                result = SearchTweetsRecursively(tweetSearchParameters);
            }
            else
            {
                string httpQuery = _searchQueryGenerator.GetSearchTweetsQuery(tweetSearchParameters);
                result = GetTweetDTOsFromSearch(httpQuery);
            }
            

            if (tweetSearchParameters.TweetSearchFilter == TweetSearchFilter.OriginalTweetsOnly)
            {
                return result.Where(x => x.RetweetedTweetDTO == null);
            }

            if (tweetSearchParameters.TweetSearchFilter == TweetSearchFilter.RetweetsOnly)
            {
                return result.Where(x => x.RetweetedTweetDTO != null);
            }

            return result;
        }

        private IEnumerable<ITweetDTO> SearchTweetsRecursively(ITweetSearchParameters tweetSearchParameters)
        {
            var searchParameter = CloneTweetSearchParameters(tweetSearchParameters);
            searchParameter.MaximumNumberOfResults = Math.Min(searchParameter.MaximumNumberOfResults, 100);

            string httpQuery = _searchQueryGenerator.GetSearchTweetsQuery(searchParameter);
            var currentResult = GetTweetDTOsFromSearch(httpQuery);
            List<ITweetDTO> result = currentResult;

            while (result.Count < tweetSearchParameters.MaximumNumberOfResults)
            {
                var oldestTweetId = GetOldestTweetId(currentResult);
                searchParameter.MaxId = oldestTweetId;
                searchParameter.MaximumNumberOfResults = Math.Min(tweetSearchParameters.MaximumNumberOfResults - result.Count, 100);
                httpQuery = _searchQueryGenerator.GetSearchTweetsQuery(searchParameter);
                currentResult = GetTweetDTOsFromSearch(httpQuery);
                result.AddRange(currentResult);

                if (currentResult.Count < searchParameter.MaximumNumberOfResults)
                {
                    // There is no other result
                    break;
                }
            }

            return result;
        }

        public IEnumerable<ITweetDTO> SearchRepliesTo(ITweetDTO tweetDTO, bool recursiveReplies)
        {
            if (tweetDTO == null)
            {
                throw new ArgumentException("TweetDTO cannot be null");
            }

            var searchTweets = SearchTweets(String.Format(tweetDTO.Creator.ScreenName)).ToList();

            if (recursiveReplies)
            {
                return GetRecursiveReplies(searchTweets, tweetDTO.Id);
            }

            var repliesDTO = searchTweets.Where(x => x.InReplyToStatusId == tweetDTO.Id);
            return repliesDTO;
        }

        private IEnumerable<ITweetDTO> GetRecursiveReplies(List<ITweetDTO> searchTweets, long sourceId)
        {
            var directReplies = searchTweets.Where(x => x.InReplyToStatusId == sourceId).ToList();
            List<ITweetDTO> results = directReplies.ToList();

            var recursiveReplies = searchTweets.Where(x => directReplies.Select(r => r.Id as long?).Contains(x.InReplyToStatusId));
            results.AddRange(recursiveReplies);

            while (recursiveReplies.Any())
            {
                var repliesFromPreviousLevel = recursiveReplies;
                recursiveReplies = searchTweets.Where(x => repliesFromPreviousLevel.Select(r => r.Id as long?).Contains(x.InReplyToStatusId));
                results.AddRange(recursiveReplies);
            }
            
            return results;
        }

        private long GetOldestTweetId(IEnumerable<ITweetDTO> tweetDTOs)
        {
            if (tweetDTOs.Count() > 0)
            {
                return tweetDTOs.Min(x => x.Id);
            }
            else
            {
                return 0;
            }
        }

        private ITweetSearchParameters CloneTweetSearchParameters(ITweetSearchParameters tweetSearchParameters)
        {
            var clone = _tweetSearchParameterUnityFactory.Create();
            
            clone.GeoCode = tweetSearchParameters.GeoCode;
            clone.Lang = tweetSearchParameters.Lang;
            clone.Locale = tweetSearchParameters.Locale;
            clone.MaxId = tweetSearchParameters.MaxId;
            clone.SearchType = tweetSearchParameters.SearchType;
            clone.MaximumNumberOfResults = tweetSearchParameters.MaximumNumberOfResults;
            clone.SearchQuery = tweetSearchParameters.SearchQuery;
            clone.SinceId = tweetSearchParameters.SinceId;
            clone.TweetSearchFilter = tweetSearchParameters.TweetSearchFilter;
            clone.Until = tweetSearchParameters.Until;

            return clone;
        }

        private List<ITweetDTO> GetTweetDTOsFromSearch(string httpQuery)
        {
            var jObject = _twitterAccessor.ExecuteGETQuery(httpQuery);
            return _wrapper.ToObject<List<ITweetDTO>>(jObject["statuses"]);
        }
    }
}