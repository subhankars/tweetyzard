using System;
using TweetinviControllers.Saved_Search;
using TweetinviCore.Interfaces.Models;
using TweetinviFactories.SavedSearch;

namespace Tweetinvi.Json
{
    public static class SavedSearchJson
    {
        [ThreadStatic]
        private static ISavedSearchJsonFactory _savedSearchJsonFactory;
        public static ISavedSearchJsonFactory SavedSearchJsonFactory
        {
            get
            {
                if (_savedSearchJsonFactory == null)
                {
                    Initialize();
                }

                return _savedSearchJsonFactory;
            }
        }

        [ThreadStatic]
        private static ISavedSearchJsonController _savedSearchJsonController;
        public static ISavedSearchJsonController SavedSearchJsonController
        {
            get
            {
                if (_savedSearchJsonController == null)
                {
                    Initialize();
                }

                return _savedSearchJsonController;
            }
        }

        static SavedSearchJson()
        {
            Initialize();
        }

        private static void Initialize()
        {
            _savedSearchJsonFactory = TweetinviContainer.Resolve<ISavedSearchJsonFactory>();
            _savedSearchJsonController = TweetinviContainer.Resolve<ISavedSearchJsonController>();
        }

        // Factory
        public static string CreateSavedSearch(string query)
        {
            return SavedSearchJsonFactory.CreateSavedSearch(query);
        }

        public static string GetSavedSearch(long searchId)
        {
            return SavedSearchJsonFactory.GetSavedSearch(searchId);
        }

        // Controller
        public static string GetSavedSearches()
        {
            return SavedSearchJsonController.GetSavedSearches();
        }

        public static string DestroySavedSearch(ISavedSearch savedSearch)
        {
            return SavedSearchJsonController.DestroySavedSearch(savedSearch);
        }

        public static string DestroySavedSearch(long searchId)
        {
            return SavedSearchJsonController.DestroySavedSearch(searchId);
        }
    }
}