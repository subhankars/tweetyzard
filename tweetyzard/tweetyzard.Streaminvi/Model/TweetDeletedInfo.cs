﻿using Newtonsoft.Json;
using TweetinviCore.Interfaces.DTO;

namespace Streaminvi.Model
{
    public class TweetDeletedInfo : ITweetDeletedInfo
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("user_id_str")]
        public string UserIdStr { get; set; }
    }
}