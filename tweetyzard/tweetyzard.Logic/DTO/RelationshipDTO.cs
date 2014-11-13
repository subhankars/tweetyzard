using Newtonsoft.Json;
using TweetinviCore.Interfaces.DTO;

namespace TweetinviLogic.DTO
{
    public class RelationshipDTO : IRelationshipDTO
    {
        [JsonProperty("relationship")]
        private TwitterRelationshipDTO _relationship { get; set; }

        private SourceRelationshipDTO _sourceAccessor
        {
            get { return _relationship.Source; }
        }

        private TargetRelationshipDTO _targetAccessor
        {
            get { return _relationship.Target; }
        }

        public long SourceId
        {
            get { return _sourceAccessor.SourceId; }
        }

        public string SourceIdStr
        {
            get { return _sourceAccessor.SourceIdStr; }
        }

        public string SourceScreenName
        {
            get { return _sourceAccessor.SourceScreenName; }
        }

        public long TargetId
        {
            get { return _targetAccessor.TargetId; }
        }

        public string TargetIdStr
        {
            get { return _targetAccessor.TargetIdStr; }
        }

        public string TargetScreenName
        {
            get { return _targetAccessor.TargetScreenName; }
        }

        [JsonIgnore]
        public bool Following
        {
            get { return _sourceAccessor.Following; }
        }

        [JsonIgnore]
        public bool FollowedBy
        {
            get { return _sourceAccessor.FollowedBy; }
        }

        [JsonIgnore]
        public bool NotificationsEnabled
        {
            get { return _sourceAccessor.NotificationsEnabled; }
        }

        [JsonIgnore]
        public bool Blocking
        {
            get { return _sourceAccessor.Blocking; }
        }

        [JsonIgnore]
        public bool WantRetweets
        {
            get { return _sourceAccessor.WantRetweets; }
        }

        [JsonIgnore]
        public bool AllReplies
        {
            get { return _sourceAccessor.AllReplies; }
        }

        [JsonIgnore]
        public bool MarkedSpam
        {
            get { return _sourceAccessor.MarkedSpam; }
        }

        [JsonIgnore]
        public bool CanSendDirectMessage
        {
            get { return _sourceAccessor.CanSendDirectMessage; }
        }

        // ReSharper disable UnusedAutoPropertyAccessor.Local
        // ReSharper disable ClassNeverInstantiated.Local
        private class TwitterRelationshipDTO
        {
            [JsonProperty("source")]
            public SourceRelationshipDTO Source { get; set; }

            [JsonProperty("target")]
            public TargetRelationshipDTO Target { get; set; }
        }

        private class SourceRelationshipDTO
        {
            [JsonProperty("id")]
            public long SourceId { get; private set; }


            [JsonProperty("id_str")]
            public string SourceIdStr { get; private set; }

            [JsonProperty("screen_name")]
            public string SourceScreenName { get; private set; }

            [JsonProperty("following")]
            public bool Following { get; set; }

            [JsonProperty("followed_by")]
            public bool FollowedBy { get; set; }

            [JsonProperty("notifications_enabled")]
            public bool NotificationsEnabled { get; set; }

            [JsonProperty("blocking")]
            public bool Blocking { get; set; }

            [JsonProperty("want_retweets")]
            public bool WantRetweets { get; set; }

            [JsonProperty("all_replies")]
            public bool AllReplies { get; set; }

            [JsonProperty("marked_spam")]
            public bool MarkedSpam { get; set; }

            [JsonProperty("can_dm")]
            public bool CanSendDirectMessage { get; set; }
        }

        private class TargetRelationshipDTO
        {
            [JsonProperty("id")]
            public long TargetId { get; set; }

            [JsonProperty("id_str")]
            public string TargetIdStr { get; set; }

            [JsonProperty("screen_name")]
            public string TargetScreenName { get; set; }
        }
        // ReSharper restore ClassNeverInstantiated.Local
        // ReSharper restore UnusedAutoPropertyAccessor.Local
    }
}