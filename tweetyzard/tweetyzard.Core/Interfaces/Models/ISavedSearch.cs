using System;
using TweetinviCore.Interfaces.DTO;

namespace TweetinviCore.Interfaces.Models
{
    public interface ISavedSearch
    {
        ISavedSearchDTO SavedSearchDTO { get; set; }

        long Id { get; }
        string IdStr { get; }
        string Name { get; set; }
        string Query { get; set; }
        DateTime CreatedAt { get; }
    }
}