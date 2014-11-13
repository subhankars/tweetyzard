using TweetinviControllers.Properties;

namespace TweetinviControllers.Account
{
    public interface IAccountQueryGenerator
    {
        string GetLoggedUserAccountSettingsQuery();
    }

    public class AccountQueryGenerator : IAccountQueryGenerator
    {
        public string GetLoggedUserAccountSettingsQuery()
        {
            return Resources.Account_GetSettings;
        }
    }
}