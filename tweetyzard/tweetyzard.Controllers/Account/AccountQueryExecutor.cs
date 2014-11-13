using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.DTO;

namespace TweetinviControllers.Account
{
    public interface IAccountQueryExecutor
    {
        IAccountSettingsDTO GetLoggedUserAccountSettings();
    }

    public class AccountQueryExecutor : IAccountQueryExecutor
    {
        private readonly ITwitterAccessor _twitterAccessor;
        private readonly IAccountQueryGenerator _accountQueryGenerator;

        public AccountQueryExecutor(
            ITwitterAccessor twitterAccessor,
            IAccountQueryGenerator accountQueryGenerator)
        {
            _twitterAccessor = twitterAccessor;
            _accountQueryGenerator = accountQueryGenerator;
        }

        public IAccountSettingsDTO GetLoggedUserAccountSettings()
        {
            string query = _accountQueryGenerator.GetLoggedUserAccountSettingsQuery();
            return _twitterAccessor.ExecuteGETQuery<IAccountSettingsDTO>(query);
        }
    }
}