namespace TweetinviCore.Interfaces.DTO
{
    public interface ITweetLocationRemovedInfo
    {
        long UserId { get; }
        string UserIdStr { get; }
        long UpToStatusId { get; }
        string UpToStatusIdStr { get; }
    }
}