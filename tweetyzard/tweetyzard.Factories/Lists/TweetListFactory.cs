using System.Collections.Generic;
using System.Linq;
using TweetinviCore.Enum;
using TweetinviCore.Helpers;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.Parameters;

namespace TweetinviFactories.Lists
{
    public class TweetListFactory : ITweetListFactory
    {
        private readonly ITweetListFactoryQueryExecutor _tweetListFactoryQueryExecutor;
        private readonly IUnityFactory<ITweetList> _tweetListUnityFactory;
        private readonly IJsonObjectConverter _jsonObjectConverter;
        private readonly IListIdentifierFactory _listIdentifierFactory;

        public TweetListFactory(
            ITweetListFactoryQueryExecutor tweetListFactoryQueryExecutor,
            IUnityFactory<ITweetList> tweetListUnityFactory,
            IJsonObjectConverter jsonObjectConverter,
            IListIdentifierFactory listIdentifierFactory)
        {
            _tweetListFactoryQueryExecutor = tweetListFactoryQueryExecutor;
            _tweetListUnityFactory = tweetListUnityFactory;
            _jsonObjectConverter = jsonObjectConverter;
            _listIdentifierFactory = listIdentifierFactory;
        }

        // Create List
        public ITweetList CreateList(string name, PrivacyMode privacyMode, string description)
        {
            var listDTO = _tweetListFactoryQueryExecutor.CreateList(name, privacyMode, description);
            return GenerateTweetListFromDTO(listDTO);
        }

        // Get Existing
        public ITweetList GetExistingTweetList(ITweetList list)
        {
            if (list == null)
            {
                return null;
            }

            return GetExistingTweetList(list.TweetListDTO);
        }

        public ITweetList GetExistingTweetList(ITweetListDTO listDTO)
        {
            var identifier = _listIdentifierFactory.Create(listDTO);
            return GetExistingTweetList(identifier);
        }

        public ITweetList GetExistingTweetList(long listId)
        {
            var identifier = _listIdentifierFactory.Create(listId);
            return GetExistingTweetList(identifier);
        }

        public ITweetList GetExistingTweetList(string slug, IUser user)
        {
            if (user == null)
            {
                return null;
            }

            return GetExistingTweetList(slug, user.UserDTO);
        }

        public ITweetList GetExistingTweetList(string slug, IUserIdDTO userDTO)
        {
            var identifier = _listIdentifierFactory.Create(slug, userDTO);
            return GetExistingTweetList(identifier);
        }

        public ITweetList GetExistingTweetList(string slug, long userId)
        {
            var identifier = _listIdentifierFactory.Create(slug, userId);
            return GetExistingTweetList(identifier);
        }

        public ITweetList GetExistingTweetList(string slug, string userScreenName)
        {
            var identifier = _listIdentifierFactory.Create(slug, userScreenName);
            return GetExistingTweetList(identifier);
        }

        private ITweetList GetExistingTweetList(IListIdentifier identifier)
        {
            if (identifier == null)
            {
                return null;
            }

            var tweetListDTO = _tweetListFactoryQueryExecutor.GetExistingTweetList(identifier);
            return GenerateTweetListFromDTO(tweetListDTO);
        }

        // Generate TweetList from DTO
        public ITweetList GenerateTweetListFromDTO(ITweetListDTO tweetListDTO)
        {
            var parameterOverride = _tweetListUnityFactory.GenerateParameterOverrideWrapper("tweetListDTO", tweetListDTO);
            return _tweetListUnityFactory.Create(parameterOverride);
        }

        public IEnumerable<ITweetList> GenerateTweetListsFromDTO(IEnumerable<ITweetListDTO> tweetListsDTO)
        {
            if (tweetListsDTO == null)
            {
                return null;
            }

            return tweetListsDTO.Select(GenerateTweetListFromDTO).ToArray();
        }

        public ITweetList GenerateTweetListFromJson(string jsonList)
        {
            var tweetListDTO = _jsonObjectConverter.DeserializeObject<ITweetListDTO>(jsonList);
            return GenerateTweetListFromDTO(tweetListDTO);
        }
    }
}