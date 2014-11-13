﻿using System;
using System.Collections.Generic;
using System.Linq;
using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.Models;

namespace Tweetinvi
{
    public static class SavedSearch
    {
        [ThreadStatic]
        private static ISavedSearchFactory _savedSearchFactory;
        public static ISavedSearchFactory SavedSearchFactory
        {
            get
            {
                if (_savedSearchFactory == null)
                {
                    Initialize();
                }

                return _savedSearchFactory;
            }
        }

        [ThreadStatic]
        private static ISavedSearchController _savedSearchController;
        public static ISavedSearchController SavedSearchController
        {
            get
            {
                if (_savedSearchController == null)
                {
                    Initialize();
                }

                return _savedSearchController;
            }
        }

        static SavedSearch()
        {
            Initialize();
        }

        private static void Initialize()
        {
            _savedSearchFactory = TweetinviContainer.Resolve<ISavedSearchFactory>();
            _savedSearchController = TweetinviContainer.Resolve<ISavedSearchController>();
        }

        // Factory
        public static ISavedSearch CreateSavedSearch(string query)
        {
            return SavedSearchFactory.CreateSavedSearch(query);
        }

        public static ISavedSearch GetSavedSearch(long searchId)
        {
            return SavedSearchFactory.GetSavedSearch(searchId);
        }

        // Controller
        public static List<ISavedSearch> GetSavedSearches()
        {
            return SavedSearchController.GetSavedSearches().ToList();
        }

        public static bool DestroySavedSearch(ISavedSearch savedSearch)
        {
            return SavedSearchController.DestroySavedSearch(savedSearch);
        }

        public static bool DestroySavedSearch(long searchId)
        {
            return SavedSearchController.DestroySavedSearch(searchId);
        }
    }
}