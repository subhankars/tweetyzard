using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TweetinviCore.Enum;
using TweetinviCore.Helpers;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.Credentials.QueryDTO;
using TweetinviCore.Interfaces.oAuth;
using TweetinviCore.Wrappers;
using TweetinviCredentials.QueryJsonConverters;

namespace TweetinviCredentials
{
    public class TwitterAccessor : ITwitterAccessor
    {
        private readonly IOAuthToken _oAuthToken;
        private readonly IJObjectStaticWrapper _wrapper;
        private readonly IJsonObjectConverter _jsonObjectConverter;

        public TwitterAccessor(
            IOAuthToken oAuthToken,
            IJObjectStaticWrapper wrapper,
            IJsonObjectConverter jsonObjectConverter)
        {
            _oAuthToken = oAuthToken;
            _wrapper = wrapper;
            _jsonObjectConverter = jsonObjectConverter;
        }

       // Execute<Json>
        public string ExecuteJsonGETQuery(string query)
        {
            return ExecuteQuery(query, HttpMethod.GET);
        }

        public string ExecuteJsonPOSTQuery(string query)
        {
            return ExecuteQuery(query, HttpMethod.POST);
        }

        // Try Execute<Json>
        public bool TryExecuteJsonGETQuery(string query, out string json)
        {
            try
            {
                json = ExecuteJsonGETQuery(query);
                return true;
            }
            catch (WebException)
            {
                json = null;
                return false;
            }
        }

        public bool TryExecuteJsonPOSTQuery(string query, out string json)
        {
            try
            {
                json = ExecuteJsonPOSTQuery(query);
                return true;
            }
            catch (WebException)
            {
                json = null;
                return false;
            }
        }

        // Execute<JObject>
        public JObject ExecuteGETQuery(string query)
        {
            string jsonResponse = ExecuteQuery(query, HttpMethod.GET);
            return _wrapper.GetJobjectFromJson(jsonResponse);
        }

        public JObject ExecutePOSTQuery(string query)
        {
            string jsonResponse = ExecuteQuery(query, HttpMethod.POST);
            return _wrapper.GetJobjectFromJson(jsonResponse);
        }

        // Execute<T>
        public T ExecuteGETQuery<T>(string query, JsonConverter[] converters = null) where T : class
        {
            string jsonResponse = ExecuteQuery(query, HttpMethod.GET);
            return _jsonObjectConverter.DeserializeObject<T>(jsonResponse, converters);
        }

        public T ExecutePOSTQuery<T>(string query, JsonConverter[] converters = null) where T : class
        {
            string jsonResponse = ExecuteQuery(query, HttpMethod.POST);
            return _jsonObjectConverter.DeserializeObject<T>(jsonResponse, converters);
        }

        // Try Execute
        public bool TryExecuteGETQuery(string query, JsonConverter[] converters = null)
        {
            try
            {
                var jObject = ExecuteGETQuery(query);
                return jObject != null;
            }
            catch (WebException)
            {
                return false;
            }
        }

        public bool TryExecutePOSTQuery(string query, JsonConverter[] converters = null)
        {
            try
            {
                var jObject = ExecutePOSTQuery(query);
                return jObject != null;
            }
            catch (WebException)
            {
                return false;
            }
        }

        // Try Execute<T>
        public bool TryExecuteGETQuery<T>(string query, out T resultObject, JsonConverter[] converters = null)
            where T : class
        {
            try
            {
                resultObject = ExecuteGETQuery<T>(query, converters);
                return resultObject != null;
            }
            catch (WebException)
            {
                resultObject = null;
                return false;
            }
        }

        public bool TryExecutePOSTQuery<T>(string query, out T resultObject, JsonConverter[] converters = null)
            where T : class
        {
            try
            {
                resultObject = ExecutePOSTQuery<T>(query, converters);
                return resultObject != null;
            }
            catch (WebException)
            {
                resultObject = null;
                return false;
            }
        }

        // Cursor Query
        public IEnumerable<string> ExecuteJsonCursorGETQuery<T>(
                string baseQuery,
                int maxObjectToRetrieve = Int32.MaxValue,
                long cursor = -1)
            where T : class, IBaseCursorQueryDTO
        {
            int nbOfObjectsProcessed = 0;
            long previousCursor = -2;
            long nextCursor = cursor;

            // add & for query parameters
            baseQuery = FormatBaseQuery(baseQuery);

            var result = new List<string>();
            while (previousCursor != nextCursor && nbOfObjectsProcessed < maxObjectToRetrieve)
            {
                T cursorResult = ExecuteCursorQuery<T>(baseQuery, cursor, true);
                if (cursorResult == null)
                {
                    return result;
                }

                nbOfObjectsProcessed += cursorResult.GetNumberOfObjectRetrieved();
                previousCursor = cursorResult.PreviousCursor;
                nextCursor = cursorResult.NextCursor;

                result.Add(cursorResult.RawJson);
            }

            return result;
        }

        public IEnumerable<T> ExecuteCursorGETQuery<T>(
                string baseQuery,
                int maxObjectToRetrieve = Int32.MaxValue,
                long cursor = -1)
            where T : class, IBaseCursorQueryDTO
        {
            int nbOfObjectsProcessed = 0;
            long previousCursor = -2;
            long nextCursor = cursor;

            // add & for query parameters
            baseQuery = FormatBaseQuery(baseQuery);

            var result = new List<T>();
            while (previousCursor != nextCursor && nbOfObjectsProcessed < maxObjectToRetrieve)
            {
                T cursorResult = ExecuteCursorQuery<T>(baseQuery, cursor, false);

                if (cursorResult == null)
                {
                    return result;
                }

                nbOfObjectsProcessed += cursorResult.GetNumberOfObjectRetrieved();
                previousCursor = cursorResult.PreviousCursor;
                nextCursor = cursorResult.NextCursor;

                result.Add(cursorResult);
            }

            return result;
        }

        private string FormatBaseQuery(string baseQuery)
        {
            if (baseQuery.Contains("?") && baseQuery.Last() != '?')
            {
                baseQuery += "&";
            }

            return baseQuery;
        }

        private T ExecuteCursorQuery<T>(string baseQuery, long cursor, bool storeJson) where T : class, IBaseCursorQueryDTO
        {
            var query = String.Format("{0}cursor={1}", baseQuery, cursor);

            string json;
            if (TryExecuteJsonGETQuery(query, out json))
            {
                var dtoResult = _jsonObjectConverter.DeserializeObject<T>(json, JsonQueryConverterRepository.Converters);

                if (storeJson)
                {
                    dtoResult.RawJson = json;
                }

                return dtoResult;
            }

            return null;
        }

        // Concrete Execute
        public string ExecuteQuery(string query, HttpMethod method)
        {
            if (query == null)
            {
                throw new ArgumentException("At least one of the arguments provided to the query was invalid.");
            }

            return _oAuthToken.ExecuteQuery(query, method);
        }
    }
}