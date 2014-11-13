using System.Collections.Generic;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Models;

namespace TweetinviCore.Interfaces.Factories
{
    public interface ISavedSearchFactory
    {
        ISavedSearch CreateSavedSearch(string searchQuery);
        ISavedSearch GetSavedSearch(long searchId);
        ISavedSearch GenerateSavedSearchFromDTO(ISavedSearchDTO savedSearchDTO);
        IEnumerable<ISavedSearch> GenerateSavedSearchesFromDTOs(IEnumerable<ISavedSearchDTO> savedSearchDTO);
    }
}