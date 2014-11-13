using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TweetinviCore.Wrappers;
using TweetinviLogic.JsonConverters;

namespace TweetinviLogic.Wrapper
{
    // Wrapper classes "cannot" be tested
    [ExcludeFromCodeCoverage] 
    public class JObjectStaticWrapper : IJObjectStaticWrapper
    {
        private readonly JsonSerializer _serializer;

        public JObjectStaticWrapper()
        {
            _serializer = new JsonSerializer();
            
            foreach (var converter in JsonPropertiesConverterRepository.Converters)
            {
                _serializer.Converters.Add(converter);
            }
        }

        public JObject GetJobjectFromJson(string json)
        {
            return JObject.Parse(json);
        }

        public T ToObject<T>(JToken jToken)
        {
            return jToken.ToObject<T>(_serializer);
        }

        public string GetNodeRootName(JToken jToken)
        {
            var jProperty = jToken as JProperty;
            return jProperty != null ? jProperty.Name : null;
        }
    }
}