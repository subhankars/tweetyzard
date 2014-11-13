using System;
using System.Collections.Generic;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.Credentials.QueryDTO;

namespace Tweetinvi
{
    public static class TwitterAccessor
    {
        [ThreadStatic] 
        private static ITwitterAccessor _twitterAccessor;
        public static ITwitterAccessor TweetListFactory
        {
            get
            {
                if (_twitterAccessor == null)
                {
                    Initialize();
                }

                return _twitterAccessor;
            }
        }

        static TwitterAccessor()
        {
            Initialize();
        }

        private static void Initialize()
        {
            _twitterAccessor = TweetinviContainer.Resolve<ITwitterAccessor>();
        }

        // Get json response from query
        public static string ExecuteJsonGETQuery(string query)
        {
            return _twitterAccessor.ExecuteJsonGETQuery(query);
        }

        public static string ExecuteJsonPOSTQuery(string query)
        {
            return _twitterAccessor.ExecuteJsonPOSTQuery(query);
        }

        // Get object (DTO) form query
        public static T ExecuteGETQuery<T>(string query) where T : class
        {
            return _twitterAccessor.ExecuteGETQuery<T>(query);
        }

        public static T ExecutePOSTQuery<T>(string query) where T : class
        {
            return _twitterAccessor.ExecutePOSTQuery<T>(query);
        }

        // Try Get object (DTO) from query
        public static bool TryExecuteGETQuery<T>(string query, out T resultObject) where T : class
        {
            return _twitterAccessor.TryExecuteGETQuery(query, out resultObject);
        }

        public static bool TryExecutePOSTQuery<T>(string query, out T resultObject) where T : class
        {
            return _twitterAccessor.TryExecutePOSTQuery(query, out resultObject);
        }

        // Try Operation and check success
        public static bool TryExecuteGETQuery(string query)
        {
            return _twitterAccessor.TryExecuteGETQuery(query);
        }

        public static bool TryExecutePOSTQuery(string query)
        {
            return _twitterAccessor.TryExecutePOSTQuery(query);
        }

        // Cusror Query
        public static IEnumerable<string> ExecuteJsonCursorGETQuery<T>(
            string baseQuery,
            int maxObjectToRetrieve = Int32.MaxValue,
            long cursor = -1)
            where T : class, IBaseCursorQueryDTO
        {
            return _twitterAccessor.ExecuteJsonCursorGETQuery<T>(baseQuery, maxObjectToRetrieve, cursor);
        }

        public static IEnumerable<T> ExecuteCursorGETQuery<T>(
            string query,
            int maxObjectToRetrieve = Int32.MaxValue,
            long cursor = -1)
            where T : class, IBaseCursorQueryDTO
        {
            return _twitterAccessor.ExecuteCursorGETQuery<T>(query, maxObjectToRetrieve, cursor);
        }

        // Base call
        public static string ExecuteQuery(string query, HttpMethod method)
        {
            return _twitterAccessor.ExecuteQuery(query, method);
        }
    }
}