using System.Collections.Generic;
using TweetinviCore.Interfaces.Models;

namespace TweetinviCore.Interfaces.Controllers
{
    public interface ISavedSearchController
    {
        IEnumerable<ISavedSearch> GetSavedSearches();
        bool DestroySavedSearch(ISavedSearch savedSearch);
        bool DestroySavedSearch(long searchId);
    }
}