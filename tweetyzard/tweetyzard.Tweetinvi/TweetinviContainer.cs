using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Streaminvi;
using TweetinviControllers;
using TweetinviCore;
using TweetinviCredentials;
using TweetinviFactories;
using TweetinviLogic;
using TweetinviWebLogic;

namespace Tweetinvi
{
    public class TweetinviContainer
    {
        private static List<IModule> _moduleCatalog;

        private static IUnityContainer _container;
        public static IUnityContainer Container
        {
            get { return _container; }
        }

        static TweetinviContainer()
        {
            Initialize();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public static T Resolve<T>(ResolverOverride[] resolverOverrides)
        {
            return _container.Resolve<T>(resolverOverrides);
        }

        private static void Initialize()
        {
            _container = new UnityContainer();
            _moduleCatalog = new List<IModule>();

            RegisterModules();
            InitialiseModules();
        }

        private static void RegisterModules()
        {
            _moduleCatalog.Add(new OAuthConnectionModule(_container));
            _moduleCatalog.Add(new TwitterTokenModule(_container));
            _moduleCatalog.Add(new TweetinviFactoriesModule(_container));
            _moduleCatalog.Add(new TweetinviLogicModule(_container));
            _moduleCatalog.Add(new TweetinviControllersModule(_container));
            _moduleCatalog.Add(new StreaminviModule(_container));
        }

        private static void InitialiseModules()
        {
            foreach (var module in _moduleCatalog)
            {
                module.Initialize();
            }
        }
    }
}
