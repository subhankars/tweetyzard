using System;
using TweetinviFactories.Properties;

namespace TweetinviFactories.SavedSearch
{
    public interface ISavedSearchQueryGenerator
    {
        string GetCreateSavedSearchQuery(string searchQuery);
        string GetSavedSearchQuery(long searchId);
    }

    public class SavedSearchFactoryQueryGenerator : ISavedSearchQueryGenerator
    {
        public string GetCreateSavedSearchQuery(string searchQuery)
        {
            return String.Format(Resources.SavedSearch_Create, searchQuery);
        }

        public string GetSavedSearchQuery(long searchId)
        {
            return String.Format(Resources.SavedSearch_Get, searchId);
        }
    }
}