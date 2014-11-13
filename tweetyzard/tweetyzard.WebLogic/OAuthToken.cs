using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Cache;
using Microsoft.Practices.Unity;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.Exceptions;
using TweetinviCore.Interfaces.oAuth;

namespace TweetinviWebLogic
{
    /// <summary>
    /// Generate a Token that can be used to perform OAuth queries
    /// </summary>
    public class OAuthToken : IOAuthToken
    {
        private readonly ICredentialsAccessor _credentialsAccessor;
        private readonly IOAuthWebRequestGenerator _oAuthWebRequestGenerator;
        private readonly IExceptionHandler _exceptionHandler;

        /// <summary>
        /// Headers of the latest WebResponse
        /// </summary>
        protected WebHeaderCollection _lastHeadersResult { get; private set; }

        [InjectionConstructor]
        public OAuthToken(
            ICredentialsAccessor credentialsStore, 
            IOAuthWebRequestGenerator oAuthWebRequestGenerator,
            IExceptionHandler exceptionHandler)
        {
            _credentialsAccessor = credentialsStore;
            _oAuthWebRequestGenerator = oAuthWebRequestGenerator;
            _exceptionHandler = exceptionHandler;
        }

        public virtual HttpWebRequest GetQueryWebRequest(string url, HttpMethod httpMethod, IEnumerable<IOAuthQueryParameter> headers = null)
        {
            if (headers == null)
            {
                headers = _oAuthWebRequestGenerator.GenerateParameters(_credentialsAccessor.CurrentThreadCredentials);
            }

            return _oAuthWebRequestGenerator.GenerateWebRequest(url, httpMethod, headers);
        }

        public string ExecuteQueryWithSpecificParametersAndTemporaryCredentials(
            string url, 
            HttpMethod httpMethod, 
            IEnumerable<IOAuthQueryParameter> parameters, 
            ITemporaryCredentials temporaryCredentials)
        {
            var queryParameters = _oAuthWebRequestGenerator.GenerateApplicationParameters(temporaryCredentials, parameters);
            return ExecuteQueryWithSpecificParameters(url, httpMethod, queryParameters);
        }

        public virtual string ExecuteQueryWithSpecificParameters(string url, HttpMethod httpMethod, IEnumerable<IOAuthQueryParameter> headers)
        {
            HttpWebRequest httpWebRequest = GetQueryWebRequest(url, httpMethod, headers);
            return ExecuteWebRequest(httpWebRequest);
        }

        // Execute a generic simple query
        public virtual string ExecuteQuery(string url, HttpMethod httpMethod)
        {
            return ExecuteQueryWithSpecificParameters(url, httpMethod, _oAuthWebRequestGenerator.GenerateParameters(_credentialsAccessor.CurrentThreadCredentials));
        }

        public string ExecuteQuery(string url, HttpMethod httpMethod, ITemporaryCredentials temporaryCredentials)
        {
            return ExecuteQueryWithSpecificParameters(url, httpMethod, _oAuthWebRequestGenerator.GenerateApplicationParameters(temporaryCredentials));
        }

        // Execute WebRequest
        private string ExecuteWebRequest(HttpWebRequest httpWebRequest)
        {
            string result = null;
            WebResponse response = null;

            try
            {
                httpWebRequest.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

                // Opening the connection
                response = httpWebRequest.GetResponse();
                Stream stream = response.GetResponseStream();

                _lastHeadersResult = response.Headers;

                if (stream != null)
                {
                    // Getting the result
                    var responseReader = new StreamReader(stream);
                    result = responseReader.ReadLine();
                }

                // Closing the connection
                response.Close();
                httpWebRequest.Abort();
            }
            catch (WebException wex)
            {
                if (httpWebRequest != null)
                {
                    _exceptionHandler.AddWebException(wex, httpWebRequest.RequestUri.AbsoluteUri);
                }

                if (response != null)
                {
                    response.Close();
                }

                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                }
            }

            return result;
        }
    }
}