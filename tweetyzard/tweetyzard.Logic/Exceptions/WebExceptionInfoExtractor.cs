using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TweetinviCore.Exceptions;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Wrappers;

namespace TweetinviLogic.Exceptions
{
    public interface IWebExceptionInfoExtractor
    {
        int GetWebExceptionStatusNumber(WebException wex);
        IEnumerable<ITwitterExceptionInfo> GetTwitterExceptionInfo(WebException wex);
    }

    public class WebExceptionInfoExtractor : IWebExceptionInfoExtractor
    {
        private readonly IJObjectStaticWrapper _jObjectStaticWrapper;
        private readonly IUnityFactory<ITwitterExceptionInfo> _twitterExceptionInfoUnityFactory;

        public WebExceptionInfoExtractor(
            IJObjectStaticWrapper jObjectStaticWrapper,
            IUnityFactory<ITwitterExceptionInfo> twitterExceptionInfoUnityFactory)
        {
            _jObjectStaticWrapper = jObjectStaticWrapper;
            _twitterExceptionInfoUnityFactory = twitterExceptionInfoUnityFactory;
        }

        public int GetWebExceptionStatusNumber(WebException wex)
        {
            var wexResponse = wex.Response as HttpWebResponse;
            if (wexResponse != null)
            {
                return (int)wexResponse.StatusCode;
            }

            return -1;
        }

        public IEnumerable<ITwitterExceptionInfo> GetTwitterExceptionInfo(WebException wex)
        {
            var wexResponse = wex.Response as HttpWebResponse;

            if (wexResponse == null)
            {
                return null;
            }

            try
            {
                return GetStreamInfo(wexResponse);
            }
            catch (WebException) { }

            return null;
        }

        private IEnumerable<ITwitterExceptionInfo> GetStreamInfo(HttpWebResponse wexResponse)
        {
            using (var stream = wexResponse.GetResponseStream())
            {
                if (stream == null)
                {
                    return null;
                }

                string twitterExceptionInfo = null;
                try
                {

                    using (var reader = new StreamReader(stream))
                    {
                        twitterExceptionInfo = reader.ReadToEnd();
                        var jObject = _jObjectStaticWrapper.GetJobjectFromJson(twitterExceptionInfo);
                        return _jObjectStaticWrapper.ToObject<IEnumerable<ITwitterExceptionInfo>>(jObject["errors"]);
                    }
                }
                catch (Exception)
                {
                    var twitterInfo = _twitterExceptionInfoUnityFactory.Create();
                    twitterInfo.Message = twitterExceptionInfo;
                    return new[] { twitterInfo };
                }
            }
        }
    }
}