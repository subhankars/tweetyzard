using Microsoft.Practices.Unity;
using TweetinviCore;
using TweetinviCore.Interfaces.Credentials;

namespace TweetinviCredentials
{
    public class TwitterTokenModule : IModule
    {
        private readonly IUnityContainer _container;

        public TwitterTokenModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<ITwitterAccessor, TwitterAccessor>(new PerThreadLifetimeManager());
            _container.RegisterType<ICredentialsAccessor, CredentialsAccessor>(new PerThreadLifetimeManager());

            _container.RegisterType<ICredentialsCreator, CredentialsCreator>();
            _container.RegisterType<IWebTokenCreator, WebTokenCreator>();
        }
    }
}