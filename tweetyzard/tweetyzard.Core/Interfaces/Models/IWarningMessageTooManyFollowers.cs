using System.Collections.Generic;

namespace TweetinviCore.Interfaces.Models
{
    public interface IWarningMessageTooManyFollowers : IWarningMessage
    {
        IEnumerable<long> UserIds { get; }
    }
}