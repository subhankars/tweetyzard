using System;
using TweetinviControllers.Properties;
using TweetinviCore;
using TweetinviCore.Interfaces.Models;

namespace TweetinviControllers.Saved_Search
{
    public interface ISavedSearchQueryGenerator
    {
        string GetSavedSearchesQuery();

        string GetDestroySavedSearchQuery(ISavedSearch savedSearch);
        string GetDestroySavedSearchQuery(long searchId);
    }

    public class SavedSearchQueryGenerator : ISavedSearchQueryGenerator
    {
        public string GetSavedSearchesQuery()
        {
            return Resources.SavedSearches_GetList;
        }

        public string GetDestroySavedSearchQuery(ISavedSearch savedSearch)
        {
            if (savedSearch == null)
            {
                return null;
            }

            return GetDestroySavedSearchQuery(savedSearch.Id);
        }

        public string GetDestroySavedSearchQuery(long searchId)
        {
            if (searchId == TweetinviConstants.DEFAULT_ID)
            {
                return null;
            }

            return String.Format(Resources.SavedSearch_Destroy, searchId);
        }
    }
}