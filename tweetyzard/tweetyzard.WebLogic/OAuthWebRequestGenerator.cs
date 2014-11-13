using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using TweetinviCore.Enum;
using TweetinviCore.Extensions;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.oAuth;

namespace TweetinviWebLogic
{
    public class OAuthWebRequestGenerator : IOAuthWebRequestGenerator
    {
        // Set this varialbe to true if you want to see what the queries sent to Twitter
        private const bool DEBUG = true;

        #region Algorithms

        private string GenerateSignature(
            Uri uri,
            HttpMethod httpMethod,
            IEnumerable<IOAuthQueryParameter> queryParameters,
            SortedDictionary<string, string> urlParameters)
        {
            List<KeyValuePair<String, String>> signatureParameters = urlParameters.ToList();

            #region Store the paramaters that will be used

            // Add all the parameters that are required to generate a signature
            var oAuthQueryParameters = queryParameters as IList<IOAuthQueryParameter> ?? queryParameters.ToList();
            foreach (var header in (from h in oAuthQueryParameters
                                    where h.RequiredForSignature
                                    orderby h.Key
                                    select h))
            {
                signatureParameters.Add(new KeyValuePair<string, string>(header.Key, header.Value));
            }

            #endregion

            #region Generate OAuthRequest Parameters

            StringBuilder urlParametersFormatted = new StringBuilder();
            foreach (KeyValuePair<string, string> param in (from p in signatureParameters orderby p.Key select p))
            {
                if (urlParametersFormatted.Length > 0)
                {
                    urlParametersFormatted.Append("&");
                }

                urlParametersFormatted.Append(string.Format("{0}={1}", param.Key, param.Value));
            }

            #endregion

            #region Generate OAuthRequest

            string url = uri.Query == "" ? uri.AbsoluteUri : uri.AbsoluteUri.Replace(uri.Query, "");

            string oAuthRequest = string.Format("{0}&{1}&{2}",
                httpMethod,
                StringFormater.UrlEncode(url),
                StringFormater.UrlEncode(urlParametersFormatted.ToString()));

            #endregion

            #region Generate OAuthSecretKey
            // Generate OAuthSecret that is required to generate a signature
            IEnumerable<IOAuthQueryParameter> oAuthSecretKeyHeaders = from h in oAuthQueryParameters
                                                                      where h.IsPartOfOAuthSecretKey
                                                                      orderby h.Key
                                                                      select h;
            string oAuthSecretkey = "";

            for (int i = 0; i < oAuthSecretKeyHeaders.Count(); ++i)
            {
                oAuthSecretkey += String.Format("{0}{1}",
                    StringFormater.UrlEncode(oAuthSecretKeyHeaders.ElementAt(i).Value),
                    (i == oAuthSecretKeyHeaders.Count() - 1) ? "" : "&");
            }

            #endregion

            // Create and return signature
            HMACSHA1 hasher = new HMACSHA1(new ASCIIEncoding().GetBytes(oAuthSecretkey));
            return StringFormater.UrlEncode(Convert.ToBase64String(hasher.ComputeHash(new ASCIIEncoding().GetBytes(oAuthRequest))));
        }

        private string GenerateHeader(
            Uri uri,
            HttpMethod httpMethod,
            List<IOAuthQueryParameter> queryParameters,
            SortedDictionary<string, string> urlParameters)
        {
            string signature = GenerateSignature(uri, httpMethod, queryParameters, urlParameters);

            IOAuthQueryParameter oAuthSignature = new OAuthQueryParameter("oauth_signature", signature, false, false, false);
            queryParameters.Add(oAuthSignature);

            StringBuilder header = new StringBuilder("OAuth ");

            foreach (var param in (from p in queryParameters
                                   where p.RequiredForHeader
                                   orderby p.Key
                                   select p))
            {
                if (header.Length > 6)
                {
                    header.Append(",");
                }

                header.Append(string.Format("{0}=\"{1}\"", param.Key, param.Value));
            }

            header.AppendFormat(",oauth_signature=\"{0}\"", signature);

            return header.ToString();
        }

        /// <summary>
        /// Method Allowing to initialize a SortedDictionnary to enable oAuth query to be generated with
        /// these parameters
        /// </summary>
        /// <returns>Call the method defined in the _generateDelegate and return a string result
        /// This result will be the header of the WebRequest.</returns>
        private List<IOAuthQueryParameter> GenerateHeaderParameters(IEnumerable<IOAuthQueryParameter> queryParameters)
        {
            List<IOAuthQueryParameter> result = queryParameters.ToList();

            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            string oauthTimestamp = Convert.ToInt64(ts.TotalSeconds).ToString(CultureInfo.InvariantCulture);
            string oauthNonce = new Random().Next(123400, 9999999).ToString(CultureInfo.InvariantCulture);

            // Required information
            result.Add(new OAuthQueryParameter("oauth_nonce", oauthNonce, true, true, false));
            result.Add(new OAuthQueryParameter("oauth_timestamp", oauthTimestamp, true, true, false));
            result.Add(new OAuthQueryParameter("oauth_version", "1.0", true, true, false));
            result.Add(new OAuthQueryParameter("oauth_signature_method", "HMAC-SHA1", true, true, false));

            return result;
        }

        #endregion

        public IEnumerable<IOAuthQueryParameter> GenerateConsumerParameters(IConsumerCredentials consumerCredentials)
        {
            var consumerHeaders = new List<IOAuthQueryParameter>();

            // Add Header for every connection to a Twitter Application
            if (consumerCredentials != null && !String.IsNullOrEmpty(consumerCredentials.ConsumerKey) && !String.IsNullOrEmpty(consumerCredentials.ConsumerSecret))
            {
                consumerHeaders.Add(new OAuthQueryParameter("oauth_consumer_key", StringFormater.UrlEncode(consumerCredentials.ConsumerKey), true, true, false));
                consumerHeaders.Add(new OAuthQueryParameter("oauth_consumer_secret", StringFormater.UrlEncode(consumerCredentials.ConsumerSecret), false, false, true));
            }

            return consumerHeaders;
        }

        public IEnumerable<IOAuthQueryParameter> GenerateApplicationParameters(ITemporaryCredentials temporaryCredentials, IEnumerable<IOAuthQueryParameter> additionalParameters = null)
        {
            var headers = GenerateConsumerParameters(temporaryCredentials).ToList();

            // Add Header for authenticated connection to a Twitter Application
            if (temporaryCredentials != null && !String.IsNullOrEmpty(temporaryCredentials.AuthorizationKey) && !String.IsNullOrEmpty(temporaryCredentials.AuthorizationSecret))
            {
                headers.Add(new OAuthQueryParameter("oauth_token", StringFormater.UrlEncode(temporaryCredentials.AuthorizationKey), true, true, false));
                headers.Add(new OAuthQueryParameter("oauth_token_secret", StringFormater.UrlEncode(temporaryCredentials.AuthorizationSecret), false, false, true));
            }
            else
            {
                headers.Add(new OAuthQueryParameter("oauth_token", "", false, false, true));
            }

            if (additionalParameters != null)
            {
                headers.AddRange(additionalParameters);
            }

            return headers;
        }

        public IEnumerable<IOAuthQueryParameter> GenerateParameters(IOAuthCredentials credentials, IEnumerable<IOAuthQueryParameter> additionalParameters = null)
        {
            var headers = GenerateConsumerParameters(credentials).ToList();

            // Add Header for authenticated connection to a Twitter Application
            if (credentials != null && !String.IsNullOrEmpty(credentials.AccessToken) && !String.IsNullOrEmpty(credentials.AccessTokenSecret))
            {
                headers.Add(new OAuthQueryParameter("oauth_token", StringFormater.UrlEncode(credentials.AccessToken), true, true, false));
                headers.Add(new OAuthQueryParameter("oauth_token_secret", StringFormater.UrlEncode(credentials.AccessTokenSecret), false, false, true));
            }
            else
            {
                headers.Add(new OAuthQueryParameter("oauth_token", "", false, false, true));
            }

            if (additionalParameters != null)
            {
                headers.AddRange(additionalParameters);
            }

            return headers;
        }

        public IOAuthQueryParameter GenerateParameter(string key, string value, bool requiredForSignature, bool requiredForHeader, bool isPartOfOAuthSecretKey)
        {
            return new OAuthQueryParameter(key, StringFormater.UrlEncode(value), requiredForSignature, requiredForHeader, isPartOfOAuthSecretKey);
        }

        public HttpWebRequest GenerateWebRequest(string url, HttpMethod httpMethod, IEnumerable<IOAuthQueryParameter> parameters)
        {
            Uri uri = new Uri(url);

            List<IOAuthQueryParameter> queryParameters = GenerateHeaderParameters(parameters);
            SortedDictionary<string, string> urlParameters = new SortedDictionary<string, string>();

            if (!string.IsNullOrEmpty(uri.Query))
            {
                foreach (Match variable in Regex.Matches(uri.Query, @"(?<varName>[^&?=]+)=(?<value>[^&?=]*)"))
                {
                    urlParameters.Add(variable.Groups["varName"].Value, variable.Groups["value"].Value);
                }
            }

            string header = GenerateHeader(uri, httpMethod, queryParameters, urlParameters);

            // This debug is only compiled in debug mode and display the executed queries
# if DEBUG
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (DEBUG)
            // ReSharper disable once CSharpWarnings::CS0162
            {
                Debug.WriteLine("{0} : {1}", httpMethod, uri.AbsoluteUri);
                Debug.WriteLine(String.Format("Header {0}", header));
            }
# endif

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri.AbsoluteUri);
            webRequest.Method = httpMethod.ToString();
            webRequest.Headers.Add("Authorization", header);
            //webRequest.ServicePoint.Expect100Continue = false;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

            return webRequest;
        }
    }
}