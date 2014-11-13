using System.Collections.Generic;

namespace TweetinviCore.Interfaces.DTO
{
    public interface ITweetWitheldInfo
    {
        long Id { get; }
        long UserId { get; }
        IEnumerable<string> WitheldInCountries { get; }
    }
}