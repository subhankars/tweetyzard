using System;
using System.Text.RegularExpressions;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.oAuth;
using TweetinviCredentials.Properties;
using TweetinviWebLogic;

namespace TweetinviCredentials
{
    public class WebTokenCreator : IWebTokenCreator
    {
        private readonly IUnityFactory<ITemporaryCredentials> _applicationCredentialsUnityFactory;
        private readonly IOAuthWebRequestGenerator _oAuthWebRequestGenerator;
        private readonly IOAuthToken _oAuthToken;

        public WebTokenCreator(
            IUnityFactory<ITemporaryCredentials> applicationCredentialsUnityFactory,
            IOAuthWebRequestGenerator oAuthWebRequestGenerator,
            IOAuthToken oAuthToken)
        {
            _applicationCredentialsUnityFactory = applicationCredentialsUnityFactory;
            _oAuthWebRequestGenerator = oAuthWebRequestGenerator;
            _oAuthToken = oAuthToken;
        }

        // Step 0 - Generate Application Credentials
        public ITemporaryCredentials GenerateApplicationCredentials(string consumerKey, string consumerSecret)
        {
            var consumerKeyParameterOverride = _applicationCredentialsUnityFactory.GenerateParameterOverrideWrapper("consumerKey", consumerKey);
            var consumerSecretParameterOverride = _applicationCredentialsUnityFactory.GenerateParameterOverrideWrapper("consumerSecret", consumerSecret);

            return _applicationCredentialsUnityFactory.Create(consumerKeyParameterOverride, consumerSecretParameterOverride);
        }

        // Step 1 - Generate Authorization URL
        public string GetPinCodeAuthorizationURL(ITemporaryCredentials temporaryCredentials)
        {
            return GetAuthorizationURL(temporaryCredentials, Resources.OAuth_PINCode_CallbackURL);
        }

        public string GetAuthorizationURL(ITemporaryCredentials temporaryCredentials, string callbackURL)
        {
            var callbackParameter = _oAuthWebRequestGenerator.GenerateParameter("oauth_callback", callbackURL, true, true, false);

            var requestTokenResponse = _oAuthToken.ExecuteQueryWithSpecificParametersAndTemporaryCredentials(Resources.OAuthRequestToken, HttpMethod.POST, new[] { callbackParameter }, temporaryCredentials);

            if (!string.IsNullOrEmpty(requestTokenResponse) && requestTokenResponse != Resources.OAuthRequestToken)
            {
                Match tokenInformation = Regex.Match(requestTokenResponse, Resources.OAuthTokenRequestRegex);

                bool callbackConfirmed = Boolean.Parse(tokenInformation.Groups["oauth_callback_confirmed"].Value);
                if (!callbackConfirmed)
                {
                    return null;
                }

                temporaryCredentials.AuthorizationKey = tokenInformation.Groups["oauth_token"].Value;
                temporaryCredentials.AuthorizationSecret = tokenInformation.Groups["oauth_token_secret"].Value;

                return String.Format("{0}?oauth_token={1}", Resources.OAuthRequestAuthorize, temporaryCredentials.AuthorizationKey);
            }

            return null;
        }

        // Step 2 - Generate Credentials
        public string GetVerifierCodeFromCallbackURL(string callbackURL)
        {
            if (callbackURL == null)
            {
                return null;
            }

            Match urlInformation = Regex.Match(callbackURL, Resources.OAuthToken_GetVerifierCode_Regex);
            return urlInformation.Groups["oauth_verifier"].Value;
        }

        public IOAuthCredentials GetCredentialsFromCallbackURL(string callbackURL, ITemporaryCredentials temporaryCredentials)
        {
            Match urlInformation = Regex.Match(callbackURL, Resources.OAuthToken_GetVerifierCode_Regex);

            String responseOAuthToken = urlInformation.Groups["oauth_token"].Value;
            String verifierCode = urlInformation.Groups["oauth_verifier"].Value;

            // Check that the callback URL response passed in is for our current credentials....
            if (String.Equals(responseOAuthToken, temporaryCredentials.AuthorizationKey))
            {
                GetCredentialsFromVerifierCode(verifierCode, temporaryCredentials);
            }

            return null;
        }

        public IOAuthCredentials GetCredentialsFromVerifierCode(string verifierCode, ITemporaryCredentials temporaryCredentials)
        {
            temporaryCredentials.VerifierCode = verifierCode;
            return GenerateToken(temporaryCredentials);
        }

        public IOAuthCredentials GenerateToken(ITemporaryCredentials temporaryCredentials)
        {
            var callbackParameter = _oAuthWebRequestGenerator.GenerateParameter("oauth_verifier", temporaryCredentials.VerifierCode, true, true, false);

            var response = _oAuthToken.ExecuteQueryWithSpecificParametersAndTemporaryCredentials(Resources.OAuthRequestAccessToken, HttpMethod.POST, new[] { callbackParameter }, temporaryCredentials);
            if (response == null)
            {
                return null;
            }

            Match responseInformation = Regex.Match(response, "oauth_token=(?<oauth_token>(?:\\w|\\-)*)&oauth_token_secret=(?<oauth_token_secret>(?:\\w)*)&user_id=(?<user_id>(?:\\d)*)&screen_name=(?<screen_name>(?:\\w)*)");

            var credentials = new OAuthCredentials();
            credentials.AccessToken = responseInformation.Groups["oauth_token"].Value;
            credentials.AccessTokenSecret = responseInformation.Groups["oauth_token_secret"].Value;
            credentials.ConsumerKey = temporaryCredentials.ConsumerKey;
            credentials.ConsumerSecret = temporaryCredentials.ConsumerSecret;

            return credentials;
        }
    }
}