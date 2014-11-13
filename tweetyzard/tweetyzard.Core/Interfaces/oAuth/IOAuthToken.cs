using System.Collections.Generic;
using System.Net;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces.Credentials;

namespace TweetinviCore.Interfaces.oAuth
{
    /// <summary>
    /// Generate a Token that can be used to perform OAuth queries
    /// </summary>
    public interface IOAuthToken
    {
        /// <summary>
        /// Get the HttpWebRequest expected from the given parameters
        /// </summary>
        /// <param name="url">URL we expect to send/retrieve information to/from</param>
        /// <param name="httpMethod">HTTP Method for the request</param>
        /// <param name="parameters">Parameters used to generate the query</param>
        /// <returns>The appropriate WebRequest</returns>
        HttpWebRequest GetQueryWebRequest(string url, HttpMethod httpMethod, IEnumerable<IOAuthQueryParameter> parameters = null);
        
        /// <summary>
        /// Get and send the result of a WebRequest
        /// </summary>
        /// <param name="url">URL we expect to send/retrieve information to/from</param>
        /// <param name="httpMethod">HTTP Method for the request</param>
        /// <returns>The appropriate WebRequest</returns>
        string ExecuteQuery(string url, HttpMethod httpMethod);
        string ExecuteQuery(string url, HttpMethod httpMethod, ITemporaryCredentials temporaryCredentials);

        string ExecuteQueryWithSpecificParametersAndTemporaryCredentials(
            string url, 
            HttpMethod httpMethod, 
            IEnumerable<IOAuthQueryParameter> parameters,
            ITemporaryCredentials temporaryCredentials);

        /// <summary>
        /// Get the HttpWebRequest expected from the given parameters
        /// </summary>
        /// <param name="url">URL we expect to send/retrieve information to/from</param>
        /// <param name="httpMethod">HTTP Method for the request</param>
        /// <param name="parameters">Parameters used to generate the query</param>
        /// <returns>The appropriate WebRequest</returns>
        string ExecuteQueryWithSpecificParameters(string url, HttpMethod httpMethod, IEnumerable<IOAuthQueryParameter> parameters);
    }
}