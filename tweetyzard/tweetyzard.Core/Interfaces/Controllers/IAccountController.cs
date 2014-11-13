using TweetinviCore.Interfaces.Models;

namespace TweetinviCore.Interfaces.Controllers
{
    public interface IAccountController
    {
        IAccountSettings GetLoggedUserSettings();
    }
}