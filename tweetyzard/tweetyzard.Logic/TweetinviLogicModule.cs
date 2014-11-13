using System.Collections;
using Microsoft.Practices.Unity;
using TweetinviCore;
using TweetinviCore.Exceptions;
using TweetinviCore.Helpers;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.Exceptions;
using TweetinviCore.Interfaces.Models;
using TweetinviCore.Interfaces.Parameters;
using TweetinviCore.Wrappers;
using TweetinviLogic.Exceptions;
using TweetinviLogic.Helpers;
using TweetinviLogic.JsonConverters;
using TweetinviLogic.Model;
using TweetinviLogic.TwitterEntities;
using TweetinviLogic.Wrapper;

namespace TweetinviLogic
{
    public class TweetinviLogicModule : IModule
    {
        private readonly IUnityContainer _container;

        public TweetinviLogicModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<ITweet, Tweet>();
            _container.RegisterType<IOEmbedTweet, OEmbedTweet>();
            _container.RegisterType<ITweetList, TweetList>();
            _container.RegisterType<IUser, User>();
            _container.RegisterType<ILoggedUser, LoggedUser>();
            _container.RegisterType<IAccountSettings, AccountSettings>();
            _container.RegisterType<IMessage, Message>();
            _container.RegisterType<IMention, Mention>();
            _container.RegisterType<IRelationship, Relationship>();
            _container.RegisterType<IRelationshipState, RelationshipState>();

            _container.RegisterType<ICoordinates, Coordinates>();
            _container.RegisterType<ILocation, Location>();

            _container.RegisterType<IJsonPropertyConverterRepository, JsonPropertyConverterRepository>();
            _container.RegisterType<IJsonObjectConverter, JsonObjectConverter>(new ContainerControlledLifetimeManager());

            RegisterHelpers();
            InitializeWrappers();
            InitializeParameters();
            InitializeExceptionHandler();
        }

        private void RegisterHelpers()
        {
            _container.RegisterType<ITwitterStringFormatter, TwitterStringFormatter>();
        }

        private void InitializeParameters()
        {
            _container.RegisterType<IGeoCode, GeoCode>();
            _container.RegisterType<IListIdentifier, ListIdentifier>();
            _container.RegisterType<IListIdentifierFactory, ListIdentifierFactory>();
            _container.RegisterType<IListUpdateParameters, ListUpdateParameters>();
            _container.RegisterType<ITweetSearchParameters, TweetSearchParameters>();
        }

        private void InitializeWrappers()
        {
            _container.RegisterType<IJObjectStaticWrapper, JObjectStaticWrapper>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IJsonConvertWrapper, JsonConvertWrapper>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IParameterOverrideWrapper, ParameterOverrideWrapper>();
        }

        private void InitializeExceptionHandler()
        {
            _container.RegisterType<IExceptionHandler, ExceptionHandler>(new PerThreadLifetimeManager());
            _container.RegisterType<IWebExceptionInfoExtractor, WebExceptionInfoExtractor>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ITwitterException, TwitterException>();
            _container.RegisterType<ITwitterExceptionInfo, TwitterExceptionInfo>();
        }
    }
}