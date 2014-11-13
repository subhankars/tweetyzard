using TweetinviCore.Interfaces.DTO;

namespace TweetinviCore.Interfaces.QueryGenerators
{
    public interface IUserQueryParameterGenerator
    {
        string GenerateUserIdParameter(long userId, string parameterName = "user_id");
        string GenerateScreenNameParameter(string screenName, string parameterName = "screen_name");

        string GenerateIdOrScreenNameParameter(
            IUserIdDTO userDTO,
            string idParameterName = "user_id",
            string screenNameParameterName = "screen_name");
    }
}