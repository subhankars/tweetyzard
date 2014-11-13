using System.Collections.Generic;

namespace TweetinviCore.Interfaces.DTO
{
    public interface IUserWitheldInfo
    {
        long Id { get; }
        IEnumerable<string> WitheldInCountries { get; }
    }
}