using System.Collections.Generic;
using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.Models;

namespace TweetinviControllers.Saved_Search
{
    public class SavedSearchController : ISavedSearchController
    {
        private readonly ISavedSearchQueryExecutor _savedSearchQueryExecutor;
        private readonly ISavedSearchFactory _savedSearchFactory;

        public SavedSearchController(
            ISavedSearchQueryExecutor savedSearchQueryExecutor,
            ISavedSearchFactory savedSearchFactory)
        {
            _savedSearchQueryExecutor = savedSearchQueryExecutor;
            _savedSearchFactory = savedSearchFactory;
        }

        public IEnumerable<ISavedSearch> GetSavedSearches()
        {
            var savedSearchesDTO = _savedSearchQueryExecutor.GetSavedSearches();
            return _savedSearchFactory.GenerateSavedSearchesFromDTOs(savedSearchesDTO);
        }

        public bool DestroySavedSearch(ISavedSearch savedSearch)
        {
            return _savedSearchQueryExecutor.DestroySavedSearch(savedSearch);
        }

        public bool DestroySavedSearch(long searchId)
        {
            return _savedSearchQueryExecutor.DestroySavedSearch(searchId);
        }
    }
}