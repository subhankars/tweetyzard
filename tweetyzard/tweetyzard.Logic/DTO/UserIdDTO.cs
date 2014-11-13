using System;
using Newtonsoft.Json;
using TweetinviCore;
using TweetinviCore.Interfaces.DTO;

namespace TweetinviLogic.DTO
{
    public class UserIdDTO : IUserIdDTO
    {
        private long? _id;
        public long Id
        {
            get
            {
                if (_id == null)
                {
                    _id = IdStr == null ? TweetinviConstants.DEFAULT_ID : Int64.Parse(IdStr);
                }

                return _id.Value;
            }
            set
            {
                _id = value;
                IdStr = _id.ToString();
            }
        }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }
    }
}