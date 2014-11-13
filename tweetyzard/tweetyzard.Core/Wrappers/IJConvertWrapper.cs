using Newtonsoft.Json;

namespace TweetinviCore.Wrappers
{
    public interface IJsonConvertWrapper
    {
        T DeserializeObject<T>(string json);
        T DeserializeObject<T>(string json, JsonConverter[] converters);
    }
}