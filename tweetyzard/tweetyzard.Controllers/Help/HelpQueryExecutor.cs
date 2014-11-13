using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.oAuth;

namespace TweetinviControllers.Help
{
    public interface IHelpQueryExecutor
    {
        ITokenRateLimits GetCurrentCredentialsRateLimits();
        ITokenRateLimits GetCredentialsRateLimits(IOAuthCredentials credentials);
        string GetTwitterPrivacyPolicy();
    }

    public class HelpQueryExecutor : IHelpQueryExecutor
    {
        private readonly IHelpQueryGenerator _helpQueryGenerator;
        private readonly ITwitterAccessor _twitterAccessor;
        private readonly ICredentialsAccessor _credentialsAccessor;

        public HelpQueryExecutor(
            IHelpQueryGenerator helpQueryGenerator,
            ITwitterAccessor twitterAccessor,
            ICredentialsAccessor credentialsAccessor)
        {
            _helpQueryGenerator = helpQueryGenerator;
            _twitterAccessor = twitterAccessor;
            _credentialsAccessor = credentialsAccessor;
        }

        public ITokenRateLimits GetCurrentCredentialsRateLimits()
        {
            string query = _helpQueryGenerator.GetCredentialsLimitsQuery();
            return _twitterAccessor.ExecuteGETQuery<ITokenRateLimits>(query);
        }

        public ITokenRateLimits GetCredentialsRateLimits(IOAuthCredentials credentials)
        {
            var savedCredentials = _credentialsAccessor.CurrentThreadCredentials;
            _credentialsAccessor.CurrentThreadCredentials = credentials;
            var rateLimits = GetCurrentCredentialsRateLimits();
            _credentialsAccessor.CurrentThreadCredentials = savedCredentials;
            return rateLimits;
        }

        public string GetTwitterPrivacyPolicy()
        {
            string query = _helpQueryGenerator.GetTwitterPrivacyPolicyQuery();
            var privacyJson = _twitterAccessor.ExecuteGETQuery(query);
            return privacyJson["privacy"].ToObject<string>();
        }
    }
}