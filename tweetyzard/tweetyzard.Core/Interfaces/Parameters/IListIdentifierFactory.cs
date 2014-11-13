using TweetinviCore.Interfaces.DTO;

namespace TweetinviCore.Interfaces.Parameters
{
    public interface IListIdentifierFactory
    {
        IListIdentifier Create(ITweetListDTO tweetListDTO);
        IListIdentifier Create(long listId);
        IListIdentifier Create(string slug, IUserIdDTO userDTO);
        IListIdentifier Create(string slug, long ownerId);
        IListIdentifier Create(string slug, string ownerScreenName);
    }
}