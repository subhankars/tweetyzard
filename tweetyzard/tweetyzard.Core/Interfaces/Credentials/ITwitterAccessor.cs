using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces.Credentials.QueryDTO;

namespace TweetinviCore.Interfaces.Credentials
{
    public interface ITwitterAccessor
    {
        // Get Json
        string ExecuteJsonGETQuery(string query);
        string ExecuteJsonPOSTQuery(string query);

        // Try Execute<Json>
        bool TryExecuteJsonGETQuery(string query, out string json);
        bool TryExecuteJsonPOSTQuery(string query, out string json);

        // Get unknown type of objects
        JObject ExecuteGETQuery(string query);
        JObject ExecutePOSTQuery(string query);

        // Get specific type of object
        T ExecuteGETQuery<T>(string query, JsonConverter[] converters = null) where T : class;
        T ExecutePOSTQuery<T>(string query, JsonConverter[] converters = null) where T : class;

        // Try Execute
        bool TryExecuteGETQuery(string query, JsonConverter[] converters = null);
        bool TryExecutePOSTQuery(string query, JsonConverter[] converters = null);

        // Try Get Result
        bool TryExecuteGETQuery<T>(string query, out T resultObject, JsonConverter[] converters = null) where T : class;
        bool TryExecutePOSTQuery<T>(string query, out T resultObject, JsonConverter[] converters = null) where T : class;

        // Cursor Query
        IEnumerable<string> ExecuteJsonCursorGETQuery<T>(
            string baseQuery,
            int maxObjectToRetrieve = Int32.MaxValue,
            long cursor = -1)
            where T : class, IBaseCursorQueryDTO;

        IEnumerable<T> ExecuteCursorGETQuery<T>(
            string query,
            int maxObjectToRetrieve = Int32.MaxValue,
            long cursor = -1)
            where T : class, IBaseCursorQueryDTO;

        // Get Json from Twitter
        string ExecuteQuery(string query, HttpMethod method);
    }
}