using System;

namespace TweetinviCore.Interfaces.Credentials
{
    public interface ITokenRateLimit
    {
        int Remaining { get; }
        long Reset { get; }
        int Limit { get; }

        double ResetDateTimeInSeconds { get; }
        DateTime ResetDateTime { get; }
    }
}