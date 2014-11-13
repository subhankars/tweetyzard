using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.DTO;

namespace TweetinviLogic.TwitterEntities
{
    public class Relationship : IRelationship
    {
        public IRelationshipDTO RelationshipDTO { get; set; }

        public Relationship(IRelationshipDTO relationshipDTO)
        {
            RelationshipDTO = relationshipDTO;
        }

        public long SourceId
        {
            get { return RelationshipDTO.SourceId; }
        }

        public string SourceIdStr
        {
            get { return RelationshipDTO.SourceIdStr; }
        }

        public string SourceScreenName
        {
            get { return RelationshipDTO.SourceScreenName; }
        }

        public long TargetId
        {
            get { return RelationshipDTO.TargetId; }
        }

        public string TargetIdStr
        {
            get { return RelationshipDTO.TargetIdStr; }
        }

        public string TargetScreenName
        {
            get { return RelationshipDTO.TargetScreenName; }
        }

        public bool Following
        {
            get { return RelationshipDTO.Following; }
        }

        public bool FollowedBy
        {
            get { return RelationshipDTO.FollowedBy; }
        }

        public bool NotificationsEnabled
        {
            get { return RelationshipDTO.NotificationsEnabled; }
        }

        public bool Blocking
        {
            get { return RelationshipDTO.Blocking; }
        }

        public bool WantRetweets
        {
            get { return RelationshipDTO.WantRetweets; }
        }

        public bool AllReplies
        {
            get { return RelationshipDTO.AllReplies; }
        }

        public bool MarkedSpam
        {
            get { return RelationshipDTO.MarkedSpam; }
        }

        public bool CanSendDirectMessage
        {
            get { return RelationshipDTO.CanSendDirectMessage; }
        }
    }
}