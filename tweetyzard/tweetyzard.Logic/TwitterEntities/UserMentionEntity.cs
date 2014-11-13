using System.Collections.Generic;
using Newtonsoft.Json;
using TweetinviCore.Interfaces.Models;

namespace TweetinviLogic.TwitterEntities
{
    /// <summary>
    /// Object storing information related with an user mention on Twitter
    /// </summary>
    public class UserMentionEntity : IUserMentionEntity
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr{ get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName{ get; set; }

        [JsonProperty("name")]
        public string Name{ get; set; }

        [JsonProperty("indices")]
        public IList<int> Indices{ get; set; }
    }
}