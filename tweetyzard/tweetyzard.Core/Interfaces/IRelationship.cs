namespace TweetinviCore.Interfaces
{
    public interface IRelationship
    {
        long SourceId { get; }
        string SourceIdStr { get; }
        string SourceScreenName { get; }

        long TargetId { get; }
        string TargetIdStr { get; }
        string TargetScreenName { get; }

        bool Following { get; }
        bool FollowedBy { get; }

        bool NotificationsEnabled { get; }
        bool Blocking { get; }
        bool WantRetweets { get; }
        bool AllReplies { get; }
        bool MarkedSpam { get; }
        bool CanSendDirectMessage { get; }
    }
}