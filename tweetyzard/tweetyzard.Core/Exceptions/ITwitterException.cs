using System.Collections.Generic;
using System.Net;

namespace TweetinviCore.Exceptions
{
    public interface ITwitterException
    {
        WebException WebException { get; }

        string URL { get; }
        int StatusCode { get; }
        string TwitterDescription { get; }
        IEnumerable<ITwitterExceptionInfo> TwitterExceptionInfos { get; }
    }
}