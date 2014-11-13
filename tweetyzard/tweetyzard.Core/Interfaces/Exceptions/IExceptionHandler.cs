using System;
using System.Collections.Generic;
using System.Net;
using TweetinviCore.Events;
using TweetinviCore.Exceptions;

namespace TweetinviCore.Interfaces.Exceptions
{
    public interface IExceptionHandler
    {
        event EventHandler<GenericEventArgs<ITwitterException>> WebExceptionReceived;

        IEnumerable<ITwitterException> ExceptionInfos { get; }
        ITwitterException LastExceptionInfos { get; }

        void AddWebException(WebException webException, string url);
    }
}