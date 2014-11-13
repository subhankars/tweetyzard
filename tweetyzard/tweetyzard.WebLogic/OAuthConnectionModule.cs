using Microsoft.Practices.Unity;
using TweetinviCore;
using TweetinviCore.Helpers;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.oAuth;

namespace TweetinviWebLogic
{
    public class OAuthConnectionModule : IModule
    {
        private readonly IUnityContainer _container;

        public OAuthConnectionModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<IOAuthToken, OAuthToken>(new PerThreadLifetimeManager());
            _container.RegisterType<IConsumerCredentials, ConsumerCredentials>();
            _container.RegisterType<ITemporaryCredentials, TemporaryCredentials>();
            _container.RegisterType<IOAuthCredentials, OAuthCredentials>();

            _container.RegisterType<IOAuthQueryParameter, OAuthQueryParameter>();
            _container.RegisterType<IOAuthWebRequestGenerator, OAuthWebRequestGenerator>();
            
            _container.RegisterType<IWebDownloader, WebDownloader>();
            _container.RegisterType<IWebHelper, WebHelper>();
        }
    }
}