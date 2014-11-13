using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.oAuth;

namespace TweetinviCore.Interfaces.Controllers
{
    public interface IHelpController
    {
        ITokenRateLimits GetCurrentCredentialsRateLimits();
        ITokenRateLimits GetCredentialsRateLimits(IOAuthCredentials credentials);
        string GetTwitterPrivacyPolicy();
    }
}