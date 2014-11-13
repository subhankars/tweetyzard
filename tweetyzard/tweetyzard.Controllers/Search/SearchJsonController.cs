using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Credentials;

namespace TweetinviControllers.Search
{
    public interface ISearchJsonController
    {
        string SearchTweets(string searchQuery);
        string SearchTweets(ITweetSearchParameters tweetSearchParameters);
    }

    public class SearchJsonController : ISearchJsonController
    {
        private readonly ISearchQueryGenerator _searchQueryGenerator;
        private readonly ITwitterAccessor _twitterAccessor;

        public SearchJsonController(
            ISearchQueryGenerator searchQueryGenerator,
            ITwitterAccessor twitterAccessor)
        {
            _searchQueryGenerator = searchQueryGenerator;
            _twitterAccessor = twitterAccessor;
        }

        public string SearchTweets(string searchQuery)
        {
            string query = _searchQueryGenerator.GetSearchTweetsQuery(searchQuery);
            return _twitterAccessor.ExecuteJsonGETQuery(query);
        }

        public string SearchTweets(ITweetSearchParameters tweetSearchParameters)
        {
            string query = _searchQueryGenerator.GetSearchTweetsQuery(tweetSearchParameters);
            return _twitterAccessor.ExecuteJsonGETQuery(query);
        }
    }
}