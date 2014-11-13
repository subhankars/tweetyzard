using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.oAuth;

namespace TweetinviFactories.Credentials
{
    public class CredentialsFactory : ICredentialsFactory
    {
        private readonly IUnityFactory<IOAuthCredentials> _oauthCredentialsUnityFactory;

        public CredentialsFactory(IUnityFactory<IOAuthCredentials> oauthCredentialsUnityFactory)
        {
            _oauthCredentialsUnityFactory = oauthCredentialsUnityFactory;
        }

        public IOAuthCredentials CreateOAuthCredentials(string userAccessToken, string userAccessSecret, string consumerKey, string consumerSecret)
        {
            var credentials = _oauthCredentialsUnityFactory.Create();
            credentials.AccessToken = userAccessToken;
            credentials.AccessTokenSecret = userAccessSecret;
            credentials.ConsumerKey = consumerKey;
            credentials.ConsumerSecret = consumerSecret;
            return credentials;
        }
    }
}