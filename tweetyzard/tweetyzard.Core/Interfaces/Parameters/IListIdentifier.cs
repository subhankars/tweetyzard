namespace TweetinviCore.Interfaces.Parameters
{
    public interface IListIdentifier
    {
        long ListId { get; set; }
        string Slug { get; set; }

        long OwnerId { get; set; }
        string OwnerScreenName { get; set; }
    }
}