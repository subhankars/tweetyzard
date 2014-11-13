using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.oAuth;

namespace Tweetinvi
{
    public static class RateLimit
    {
        private static readonly IHelpController _helpController;

        static RateLimit()
        {
            _helpController = TweetinviContainer.Resolve<IHelpController>();
        }

        public static ITokenRateLimits GetCurrentCredentialsRateLimits()
        {
            return _helpController.GetCurrentCredentialsRateLimits();
        }

        public static ITokenRateLimits GetCredentialsRateLimits(IOAuthCredentials credentials)
        {
            return _helpController.GetCredentialsRateLimits(credentials);
        }
    }
}