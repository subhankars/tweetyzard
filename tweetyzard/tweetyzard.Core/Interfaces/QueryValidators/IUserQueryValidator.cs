using TweetinviCore.Interfaces.DTO;

namespace TweetinviCore.Interfaces.QueryValidators
{
    public interface IUserQueryValidator
    {
        bool CanUserBeIdentified(IUserIdDTO userIdDTO);
        bool IsScreenNameValid(string screenName);
        bool IsUserIdValid(long userId);
    }
}