using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using TweetinviCore.Wrappers;

namespace TweetinviLogic.Wrapper
{
    // Wrapper classes "cannot" be tested
    [ExcludeFromCodeCoverage] 
    public class JsonConvertWrapper : IJsonConvertWrapper
    {
        public T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public T DeserializeObject<T>(string json, JsonConverter[] converters)
        {
            return JsonConvert.DeserializeObject<T>(json, converters);
        }
    }
}