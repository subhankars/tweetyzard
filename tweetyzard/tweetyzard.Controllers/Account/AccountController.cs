using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.Models;

namespace TweetinviControllers.Account
{
    public class AccountController : IAccountController
    {
        private readonly IAccountQueryExecutor _accountQueryExecutor;
        private readonly IUnityFactory<IAccountSettings> _accountSettingsUnityFactory;

        public AccountController(
            IAccountQueryExecutor accountQueryExecutor,
            IUnityFactory<IAccountSettings> accountSettingsUnityFactory)
        {
            _accountQueryExecutor = accountQueryExecutor;
            _accountSettingsUnityFactory = accountSettingsUnityFactory;
        }

        public IAccountSettings GetLoggedUserSettings()
        {
            var accountSettingsDTO = _accountQueryExecutor.GetLoggedUserAccountSettings();
            return GenerateAccountSettingsFromDTO(accountSettingsDTO);
        }

        private IAccountSettings GenerateAccountSettingsFromDTO(IAccountSettingsDTO accountSettingsDTO)
        {
            var parameterOverride = _accountSettingsUnityFactory.GenerateParameterOverrideWrapper("accountSettingsDTO", accountSettingsDTO);
            return _accountSettingsUnityFactory.Create(parameterOverride);
        }
    }
}