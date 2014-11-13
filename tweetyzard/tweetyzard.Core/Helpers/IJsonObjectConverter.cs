using Newtonsoft.Json;

namespace TweetinviCore.Helpers
{
    public interface IJsonObjectConverter
    {
        T DeserializeObject<T>(string json, JsonConverter[] converters = null) where T : class;
    }
}