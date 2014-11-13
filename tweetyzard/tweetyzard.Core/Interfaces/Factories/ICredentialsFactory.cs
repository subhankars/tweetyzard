using TweetinviCore.Interfaces.oAuth;

namespace TweetinviCore.Interfaces.Factories
{
    public interface ICredentialsFactory
    {
        IOAuthCredentials CreateOAuthCredentials(string userAccessToken, string userAccessSecret, string consumerKey, string consumerSecret);
    }
}