using System.Collections.Generic;
using System.Net;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces.Credentials;

namespace TweetinviCore.Interfaces.oAuth
{
    /// <summary>
    /// Generator of HttpWebRequest using OAuth specification
    /// </summary>
    public interface IOAuthWebRequestGenerator
    {
        IOAuthQueryParameter GenerateParameter(string key, string value, bool requiredForSignature, bool requiredForHeader, bool isPartOfOAuthSecretKey);
        
        IEnumerable<IOAuthQueryParameter> GenerateConsumerParameters(IConsumerCredentials consumerCredentials);
        IEnumerable<IOAuthQueryParameter> GenerateApplicationParameters(ITemporaryCredentials temporaryCredentials, IEnumerable<IOAuthQueryParameter> additionalParameters = null);
        IEnumerable<IOAuthQueryParameter> GenerateParameters(IOAuthCredentials credentials, IEnumerable<IOAuthQueryParameter> additionalParameters = null);

        /// <summary>
        /// Generates an HttpWebRequest by giving minimal information
        /// </summary>
        /// <param name="url">URL we expect to send/retrieve information to/from</param>
        /// <param name="httpMethod">HTTP Method for the request</param>
        /// <param name="parameters">Parameters used to generate the query</param>
        /// <returns>The appropriate WebRequest</returns>
        HttpWebRequest GenerateWebRequest(string url, HttpMethod httpMethod, IEnumerable<IOAuthQueryParameter> parameters);
    }
}