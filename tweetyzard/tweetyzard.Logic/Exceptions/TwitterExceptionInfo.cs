using Newtonsoft.Json;
using TweetinviCore.Exceptions;

namespace TweetinviLogic.Exceptions
{
    public class TwitterExceptionInfo : ITwitterExceptionInfo
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }
    }
}