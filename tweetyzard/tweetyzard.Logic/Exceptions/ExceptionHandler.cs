using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TweetinviCore.Events;
using TweetinviCore.Exceptions;
using TweetinviCore.Interfaces.Exceptions;
using TweetinviCore.Interfaces.Factories;

namespace TweetinviLogic.Exceptions
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly IUnityFactory<ITwitterException> _twitterExceptionUnityFactory;

        private readonly List<ITwitterException> _getExceptionInfos;
        public event EventHandler<GenericEventArgs<ITwitterException>> WebExceptionReceived;

        public IEnumerable<ITwitterException> ExceptionInfos
        {
            get { return _getExceptionInfos; }
        }

        public ITwitterException LastExceptionInfos
        {
            get { return _getExceptionInfos.LastOrDefault(); }
        }

        public ExceptionHandler(IUnityFactory<ITwitterException> twitterExceptionUnityFactory)
        {
            _twitterExceptionUnityFactory = twitterExceptionUnityFactory;
            _getExceptionInfos = new List<ITwitterException>();
        }

        public void AddWebException(WebException webException, string url)
        {
            var webExceptionParameterOverride = _twitterExceptionUnityFactory.GenerateParameterOverrideWrapper("webException", webException);
            var urlParameterOverride = _twitterExceptionUnityFactory.GenerateParameterOverrideWrapper("url", url);
            var twitterException = _twitterExceptionUnityFactory.Create(webExceptionParameterOverride, urlParameterOverride);
            _getExceptionInfos.Add(twitterException);

            this.Raise(WebExceptionReceived, twitterException);
        }
    }
}