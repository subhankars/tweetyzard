using Newtonsoft.Json.Linq;

namespace TweetinviCore.Wrappers
{
    public interface IJObjectStaticWrapper
    {
        JObject GetJobjectFromJson(string json);
        T ToObject<T>(JToken jObject);
        string GetNodeRootName(JToken jToken);
    }
}