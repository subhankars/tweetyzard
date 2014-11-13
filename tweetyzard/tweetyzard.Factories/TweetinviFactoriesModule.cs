using Microsoft.Practices.Unity;
using TweetinviCore;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.Models;
using TweetinviFactories.Credentials;
using TweetinviFactories.Direct_Messages;
using TweetinviFactories.Friendship;
using TweetinviFactories.Geo;
using TweetinviFactories.Lists;
using TweetinviFactories.SavedSearch;
using TweetinviFactories.Tweet;
using TweetinviFactories.User;
using TweetinviLogic.DTO;
using TweetinviLogic.TwitterEntities;

namespace TweetinviFactories
{
    public class TweetinviFactoriesModule : IModule
    {
        private readonly IUnityContainer _container;

        public TweetinviFactoriesModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            RegisterDTO();
            RegisterFactories();

            _container.RegisterType(typeof(IUnityFactory<>), typeof(UnityFactory<>));
        }

        private void RegisterDTO()
        {
            _container.RegisterType<ITweetDTO, TweetDTO>();
            _container.RegisterType<ITweetListDTO, TweetListDTO>();
            _container.RegisterType<IUserDTO, UserDTO>();
            _container.RegisterType<IMessageDTO, MessageDTO>();
            _container.RegisterType<IRelationshipDTO, RelationshipDTO>();

            _container.RegisterType<ITweetEntities, TweetEntities>();
            _container.RegisterType<IUrlEntity, UrlEntity>();
            _container.RegisterType<IHashtagEntity, HashtagEntity>();
        }

        private void RegisterFactories()
        {
            _container.RegisterType<ICredentialsFactory, CredentialsFactory>(new ContainerControlledLifetimeManager());

            _container.RegisterType<ITweetFactory, TweetFactory>(new PerThreadLifetimeManager());
            _container.RegisterType<ITweetFactoryQueryExecutor, TweetFactoryQueryExecutor>(new PerThreadLifetimeManager());

            _container.RegisterType<IUserFactory, UserFactory>(new PerThreadLifetimeManager());
            _container.RegisterType<IUserFactoryQueryExecutor, UserFactoryQueryExecutor>(new PerThreadLifetimeManager());
            _container.RegisterType<IUserFactoryQueryGenerator, UserFactoryQueryGenerator>(new PerThreadLifetimeManager());

            _container.RegisterType<IFriendshipFactory, FriendshipFactory>(new PerThreadLifetimeManager());
            _container.RegisterType<IFriendshipFactoryQueryExecutor, FriendshipFactoryQueryExecutor>(new PerThreadLifetimeManager());

            _container.RegisterType<IMessageFactory, MessageFactory>(new PerThreadLifetimeManager());
            _container.RegisterType<IMessageFactoryQueryExecutor, MessageFactoryQueryExecutor>(new PerThreadLifetimeManager());

            _container.RegisterType<ITweetListFactory, TweetListFactory>(new PerThreadLifetimeManager());
            _container.RegisterType<ITweetListFactoryQueryExecutor, TweetListFactoryQueryExecutor>(new PerThreadLifetimeManager());
            _container.RegisterType<ITweetListFactoryQueryGenerator, TweetListFactoryQueryGenerator>(new PerThreadLifetimeManager());

            _container.RegisterType<IGeoFactory, GeoFactory>(new PerThreadLifetimeManager());

            _container.RegisterType<ISavedSearchFactory, SavedSearchFactory>(new PerThreadLifetimeManager());
            _container.RegisterType<ISavedSearchJsonFactory, SavedSearchJsonFactory>(new PerThreadLifetimeManager());
            _container.RegisterType<ISavedSearchQueryExecutor, SavedSearchFactoryQueryExecutor>(new PerThreadLifetimeManager());
            _container.RegisterType<ISavedSearchQueryGenerator, SavedSearchFactoryQueryGenerator>(new PerThreadLifetimeManager());
        }
    }
}