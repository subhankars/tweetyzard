using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.Models;

namespace TweetinviFactories.SavedSearch
{
    public interface ISavedSearchQueryExecutor
    {
        ISavedSearch CreateSavedSearch(string searchQuery);
        ISavedSearch GetSavedSearch(long searchId);
    }

    public class SavedSearchFactoryQueryExecutor : ISavedSearchQueryExecutor
    {
        private readonly ISavedSearchQueryGenerator _savedSearchQueryGenerator;
        private readonly ITwitterAccessor _twitterAccessor;

        public SavedSearchFactoryQueryExecutor(
            ISavedSearchQueryGenerator savedSearchQueryGenerator,
            ITwitterAccessor twitterAccessor)
        {
            _savedSearchQueryGenerator = savedSearchQueryGenerator;
            _twitterAccessor = twitterAccessor;
        }

        public ISavedSearch CreateSavedSearch(string searchQuery)
        {
            string query = _savedSearchQueryGenerator.GetCreateSavedSearchQuery(searchQuery);
            return _twitterAccessor.ExecutePOSTQuery<ISavedSearch>(query);
        }

        public ISavedSearch GetSavedSearch(long searchId)
        {
            string query = _savedSearchQueryGenerator.GetSavedSearchQuery(searchId);
            return _twitterAccessor.ExecuteGETQuery<ISavedSearch>(query);
        }
    }
}