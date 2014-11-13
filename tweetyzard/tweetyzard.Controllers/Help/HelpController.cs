using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.oAuth;

namespace TweetinviControllers.Help
{
    public class HelpController : IHelpController
    {
        private readonly IHelpQueryExecutor _helpQueryExecutor;

        public HelpController(IHelpQueryExecutor helpQueryExecutor)
        {
            _helpQueryExecutor = helpQueryExecutor;
        }

        public ITokenRateLimits GetCurrentCredentialsRateLimits()
        {
            return _helpQueryExecutor.GetCurrentCredentialsRateLimits();
        }

        public ITokenRateLimits GetCredentialsRateLimits(IOAuthCredentials credentials)
        {
            return _helpQueryExecutor.GetCredentialsRateLimits(credentials);
        }

        public string GetTwitterPrivacyPolicy()
        {
            return _helpQueryExecutor.GetTwitterPrivacyPolicy();
        }
    }
}