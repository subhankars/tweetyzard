using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TweetinviCore.Extensions
{
    /// <summary>
    /// Provide Extension Methods to manage Dictionary
    /// </summary>
    public static class DictionaryExtension
    {
        /// <summary>
        /// Provide an extension that do a TryGetValue and return the result
        /// </summary>
        /// <param name="dictionary">Dictionary hosting the KeyValuePair</param>
        /// <param name="propName">Key to be looked for</param>
        /// <returns>Value of the key if exists</returns>
        public static object GetProp(this Dictionary<String, object> dictionary, string propName)
        {
            object result;
            dictionary.TryGetValue(propName, out result);            
            return result;
        }

        /// <summary>
        /// Provide an extension that do a TryGetValue and return the result
        /// </summary>
        /// <param name="dictionary">Dictionary hosting the KeyValuePair</param>
        /// <param name="propName">Key to be looked for</param>
        /// <returns>Value of the key if exists</returns>
        public static T GetProp<T>(this Dictionary<String, object> dictionary, string propName)
        {
            object result;
            dictionary.TryGetValue(propName, out result);
            return (T)result;
        }

        /// <summary>
        /// Provide an extension that do a TryGetValue and return the result
        /// </summary>
        /// <param name="dictionary">Dictionary hosting the KeyValuePair</param>
        /// <param name="propName">Key to be looked for</param>
        /// <returns>Value of the key if exists</returns>
        public static T GetPropAs<T>(this Dictionary<String, object> dictionary, string propName) 
            where T : class 
        {
            object result;
            dictionary.TryGetValue(propName, out result);
            return result as T;
        }

        /// <summary>
        /// Extension getting a collection of a specified type T from an object created by the JavascriptSerializer 
        /// </summary>
        public static IEnumerable<T> GetPropCollection<T>(this Dictionary<string, object> dictionary, string propName)
        {
            var dicAsObjectArray = dictionary.GetPropAs<object[]>(propName);
            var dicAsArrayList = dictionary.GetPropAs<ArrayList>(propName);

            var d1 = dicAsObjectArray == null ? null : dicAsObjectArray.Cast<T>();
            var d2 = dicAsArrayList == null ? null : dicAsArrayList.Cast<T>();

            return d1 ?? d2 ?? new List<T>();
        }

        /// <summary>
        /// Extension getting a collection of object from an object created by the JavascriptSerializer 
        /// </summary>
        public static IEnumerable<Dictionary<string, object>> GetPropCollection(this Dictionary<string, object> dictionary, string propName)
        {
            var dicAsObjectArray = dictionary.GetPropAs<object[]>(propName);
            var dicAsArrayList = dictionary.GetPropAs<ArrayList>(propName);

            var d1 = dicAsObjectArray == null ? null : dicAsObjectArray.Cast<Dictionary<string, object>>();
            var d2 = dicAsArrayList == null ? null : dicAsArrayList.Cast<Dictionary<string, object>>();

            return d1 ?? d2 ?? new List<Dictionary<string, object>>();
        }
    }
}