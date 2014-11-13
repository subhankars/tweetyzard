using System.Text.RegularExpressions;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.oAuth;
using TweetinviCredentials.Properties;

namespace TweetinviCredentials
{
    public interface ICredentialsCreator
    {
        ITemporaryCredentials GenerateApplicationCredentials(string consumerKey, string consumerSecret);
        IOAuthCredentials GetCredentialsFromVerifierCode(string verifierCode, ITemporaryCredentials temporaryCredentials);
    }

    public class CredentialsCreator : ICredentialsCreator
    {
        private readonly IOAuthToken _oAuthToken;
        private readonly ICredentialsFactory _credentialsFactory;
        private readonly IOAuthWebRequestGenerator _oAuthWebRequestGenerator;
        private readonly IUnityFactory<ITemporaryCredentials> _applicationCredentialsUnityFactory;

        public CredentialsCreator(
            IOAuthToken oAuthToken,
            ICredentialsFactory credentialsFactory,
            IOAuthWebRequestGenerator oAuthWebRequestGenerator,
            IUnityFactory<ITemporaryCredentials> applicationCredentialsUnityFactory)
        {
            _oAuthToken = oAuthToken;
            _credentialsFactory = credentialsFactory;
            _oAuthWebRequestGenerator = oAuthWebRequestGenerator;
            _applicationCredentialsUnityFactory = applicationCredentialsUnityFactory;
        }

        // Step 0 - Generate Temporary Credentials
        public ITemporaryCredentials GenerateApplicationCredentials(string consumerKey, string consumerSecret)
        {
            var consumerKeyParameterOverride = _applicationCredentialsUnityFactory.GenerateParameterOverrideWrapper("consumerKey", consumerKey);
            var consumerSecretParameterOverride = _applicationCredentialsUnityFactory.GenerateParameterOverrideWrapper("consumerSecret", consumerSecret);

            return _applicationCredentialsUnityFactory.Create(consumerKeyParameterOverride, consumerSecretParameterOverride);
        }

        // Step 2 - Generate User Credentials
        public IOAuthCredentials GetCredentialsFromVerifierCode(string verifierCode, ITemporaryCredentials temporaryCredentials)
        {
            var callbackParameter = _oAuthWebRequestGenerator.GenerateParameter("oauth_verifier", verifierCode, true, true, false);
            var response = _oAuthToken.ExecuteQueryWithSpecificParametersAndTemporaryCredentials(Resources.OAuthRequestAccessToken, HttpMethod.POST, new[] { callbackParameter }, temporaryCredentials);

            if (response == null)
            {
                return null;
            }

            Match responseInformation = Regex.Match(response, Resources.OAuthTokenAccessRegex);
            if (responseInformation.Groups["oauth_token"] == null || responseInformation.Groups["oauth_token_secret"] == null)
            {
                return null;
            }

            var credentials = _credentialsFactory.CreateOAuthCredentials(
                    responseInformation.Groups["oauth_token"].Value,
                    responseInformation.Groups["oauth_token_secret"].Value,
                    temporaryCredentials.ConsumerKey,
                    temporaryCredentials.ConsumerSecret);

            return credentials;
        }
    }
}