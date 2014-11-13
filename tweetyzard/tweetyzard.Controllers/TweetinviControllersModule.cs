using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity;
using TweetinviControllers.Account;
using TweetinviControllers.Friendship;
using TweetinviControllers.Geo;
using TweetinviControllers.Help;
using TweetinviControllers.Lists;
using TweetinviControllers.Messages;
using TweetinviControllers.Saved_Search;
using TweetinviControllers.Search;
using TweetinviControllers.Timeline;
using TweetinviControllers.Trends;
using TweetinviControllers.Tweet;
using TweetinviControllers.User;
using TweetinviCore;
using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.Models;
using TweetinviCore.Interfaces.QueryGenerators;
using TweetinviCore.Interfaces.QueryValidators;

namespace TweetinviControllers
{
    [ExcludeFromCodeCoverage]
    public class TweetinviControllersModule : IModule
    {
        private readonly IUnityContainer _container;

        public TweetinviControllersModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            InitializeControllers();
            InitializeJsonControllers();
            InitializeQueryExecutors();
            InitializeQueryGenerators();
            InitializeQueryValidators();
            InitializeParameters();
        }

        private void InitializeControllers()
        {
            _container.RegisterType<IAccountController, AccountController>(new PerThreadLifetimeManager());
            _container.RegisterType<IFriendshipController, FriendshipController>(new PerThreadLifetimeManager());
            _container.RegisterType<IGeoController, GeoController>(new PerThreadLifetimeManager());
            _container.RegisterType<IHelpController, HelpController>(new PerThreadLifetimeManager());
            _container.RegisterType<IMessageController, MessageController>(new PerThreadLifetimeManager());
            _container.RegisterType<ISavedSearchController, SavedSearchController>(new PerThreadLifetimeManager());
            _container.RegisterType<ITimelineController, TimelineController>(new PerThreadLifetimeManager());
            _container.RegisterType<ITrendsController, TrendsController>(new PerThreadLifetimeManager());
            _container.RegisterType<ITweetController, TweetController>(new PerThreadLifetimeManager());
            _container.RegisterType<IUserController, UserController>(new PerThreadLifetimeManager());
            _container.RegisterType<ITweetListController, TweetListController>(new PerThreadLifetimeManager());

            _container.RegisterType<ISearchController, SearchController>(new PerThreadLifetimeManager());
        }

        private void InitializeJsonControllers()
        {
            _container.RegisterType<IAccountJsonController, AccountJsonController>(new PerThreadLifetimeManager());
            _container.RegisterType<IFriendshipJsonController, FriendshipJsonController>(new PerThreadLifetimeManager());
            _container.RegisterType<IGeoJsonController, GeoJsonController>(new PerThreadLifetimeManager());
            _container.RegisterType<IHelpJsonController, HelpJsonController>(new PerThreadLifetimeManager());
            _container.RegisterType<IMessageJsonController, MessageJsonController>(new PerThreadLifetimeManager());
            _container.RegisterType<ISavedSearchJsonController, SavedSearchJsonController>(new PerThreadLifetimeManager());
            _container.RegisterType<ITimelineJsonController, TimelineJsonController>(new PerThreadLifetimeManager());
            _container.RegisterType<ITrendsJsonController, TrendsJsonController>(new PerThreadLifetimeManager());
            _container.RegisterType<ITweetJsonController, TweetJsonController>(new PerThreadLifetimeManager());
            _container.RegisterType<IUserJsonController, UserJsonController>(new PerThreadLifetimeManager());
            _container.RegisterType<ITweetListJsonController, TweetListJsonController>(new PerThreadLifetimeManager());

            _container.RegisterType<ISearchJsonController, SearchJsonController>(new PerThreadLifetimeManager());
        }

        private void InitializeQueryExecutors()
        {
            _container.RegisterType<IAccountQueryExecutor, AccountQueryExecutor>(new PerThreadLifetimeManager());
            _container.RegisterType<IFriendshipQueryExecutor, FriendshipQueryExecutor>(new PerThreadLifetimeManager());
            _container.RegisterType<IGeoQueryExecutor, GeoQueryExecutor>(new PerThreadLifetimeManager());
            _container.RegisterType<IHelpQueryExecutor, HelpQueryExecutor>(new PerThreadLifetimeManager());
            _container.RegisterType<IMessageQueryExecutor, MessageQueryExecutor>(new PerThreadLifetimeManager());
            _container.RegisterType<ISavedSearchQueryExecutor, SavedSearchQueryExecutor>(new PerThreadLifetimeManager());
            _container.RegisterType<ITimelineQueryExecutor, TimelineQueryExecutor>(new PerThreadLifetimeManager());
            _container.RegisterType<ITrendsQueryExecutor, TrendsQueryExecutor>(new PerThreadLifetimeManager());
            _container.RegisterType<ITweetQueryExecutor, TweetQueryExecutor>(new PerThreadLifetimeManager());
            _container.RegisterType<IUserQueryExecutor, UserQueryExecutor>(new PerThreadLifetimeManager());
            _container.RegisterType<ITweetListQueryExecutor, TweetListQueryExecutor>(new PerThreadLifetimeManager());

            _container.RegisterType<ISearchQueryExecutor, SearchQueryExecutor>(new PerThreadLifetimeManager());
        }

        private void InitializeQueryGenerators()
        {
            _container.RegisterType<IAccountQueryGenerator, AccountQueryGenerator>(new PerThreadLifetimeManager());
            _container.RegisterType<IFriendshipQueryGenerator, FriendshipQueryGenerator>(new PerThreadLifetimeManager());
            _container.RegisterType<IGeoQueryGenerator, GeoQueryGenerator>(new PerThreadLifetimeManager());
            _container.RegisterType<IHelpQueryGenerator, HelpQueryGenerator>(new PerThreadLifetimeManager());
            _container.RegisterType<IMessageQueryGenerator, MessageQueryGenerator>(new PerThreadLifetimeManager());
            _container.RegisterType<ISavedSearchQueryGenerator, SavedSearchQueryGenerator>(new PerThreadLifetimeManager());
            _container.RegisterType<ITimelineQueryGenerator, TimelineQueryGenerator>(new PerThreadLifetimeManager());
            _container.RegisterType<ITrendsQueryGenerator, TrendsQueryGenerator>(new PerThreadLifetimeManager());
            _container.RegisterType<ITweetQueryGenerator, TweetQueryGenerator>(new PerThreadLifetimeManager());
            _container.RegisterType<IUserQueryGenerator, UserQueryGenerator>(new PerThreadLifetimeManager());
            _container.RegisterType<ISearchQueryGenerator, SearchQueryGenerator>(new PerThreadLifetimeManager());
            _container.RegisterType<ITweetListQueryGenerator, TweetListQueryGenerator>(new PerThreadLifetimeManager());
            _container.RegisterType<ITweetListQueryParameterGenerator, TweetListQueryParameterGenerator>(new PerResolveLifetimeManager());
            _container.RegisterType<IUserQueryParameterGenerator, UserQueryParameterGenerator>(new PerThreadLifetimeManager());
        }

        private void InitializeQueryValidators()
        {
            _container.RegisterType<IMessageQueryValidator, MessageQueryValidator>(new PerThreadLifetimeManager());
            _container.RegisterType<ITweetQueryValidator, TweetQueryValidator>(new PerThreadLifetimeManager());
            _container.RegisterType<IUserQueryValidator, UserQueryValidator>(new PerThreadLifetimeManager());
            _container.RegisterType<ISearchQueryValidator, SearchQueryValidator>(new PerThreadLifetimeManager());
            _container.RegisterType<ITweetListQueryValidator, TweetListQueryValidator>(new PerThreadLifetimeManager());
        }

        private void InitializeParameters()
        {
            _container.RegisterType<IFriendshipAuthorizations, FriendshipAuthorizations>();
        }
    }
}