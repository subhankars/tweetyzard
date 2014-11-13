using System.Collections.Generic;
using System.Linq;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.Models;

namespace TweetinviFactories.SavedSearch
{
   public class SavedSearchFactory : ISavedSearchFactory
    {
        private readonly ISavedSearchQueryExecutor _savedSearchQueryExecutor;
        private readonly IUnityFactory<ISavedSearch> _savedSearchUnityFactory;

        public SavedSearchFactory(
            ISavedSearchQueryExecutor savedSearchQueryExecutor,
            IUnityFactory<ISavedSearch> savedSearchUnityFactory)
        {
            _savedSearchQueryExecutor = savedSearchQueryExecutor;
            _savedSearchUnityFactory = savedSearchUnityFactory;
        }

        public ISavedSearch CreateSavedSearch(string searchQuery)
        {
            return _savedSearchQueryExecutor.CreateSavedSearch(searchQuery);
        }

        public ISavedSearch GetSavedSearch(long searchId)
        {
            return _savedSearchQueryExecutor.GetSavedSearch(searchId);
        }

        public ISavedSearch GenerateSavedSearchFromDTO(ISavedSearchDTO savedSearchDTO)
        {
            if (savedSearchDTO == null)
            {
                return null;
            }

            var savedSearchDTOParameter = _savedSearchUnityFactory.GenerateParameterOverrideWrapper("savedSearchQueryExecutor", savedSearchDTO);
            return _savedSearchUnityFactory.Create(savedSearchDTOParameter);
        }

       public IEnumerable<ISavedSearch> GenerateSavedSearchesFromDTOs(IEnumerable<ISavedSearchDTO> savedSearchDTO)
       {
           return savedSearchDTO.Select(GenerateSavedSearchFromDTO);
       }
    }
}