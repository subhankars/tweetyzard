using TweetinviControllers.Properties;

namespace TweetinviControllers.Help
{
    public interface IHelpQueryGenerator
    {
        string GetCredentialsLimitsQuery();
        string GetTwitterPrivacyPolicyQuery();
    }

    public class HelpQueryGenerator : IHelpQueryGenerator
    {
        public string GetCredentialsLimitsQuery()
        {
            return Resources.Help_GetRateLimit;
        }

        public string GetTwitterPrivacyPolicyQuery()
        {
            return Resources.Help_GetTwitterPrivacyPolicy;
        }
    }
}