using System;
using System.Collections.Generic;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.Parameters;

namespace TweetinviControllers.Lists
{
    public class TweetListController : ITweetListController
    {
        private readonly ITweetFactory _tweetFactory;
        private readonly IUserFactory _userFactory;
        private readonly ITweetListQueryExecutor _listsQueryExecutor;
        private readonly ITweetListFactory _listsFactory;
        private readonly IListIdentifierFactory _listIdentifierFactory;

        public TweetListController(
            ITweetFactory tweetFactory,
            IUserFactory userFactory,
            ITweetListQueryExecutor listsQueryExecutor,
            ITweetListFactory listsFactory,
            IListIdentifierFactory listIdentifierFactory)
        {
            _tweetFactory = tweetFactory;
            _userFactory = userFactory;
            _listsQueryExecutor = listsQueryExecutor;
            _listsFactory = listsFactory;
            _listIdentifierFactory = listIdentifierFactory;
        }

        // Get User Lists
        public IEnumerable<ITweetList> GetUserLists(IUser user, bool getOwnedListsFirst)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }

            return GetUserLists(user.UserDTO, getOwnedListsFirst);
        }

        public IEnumerable<ITweetList> GetUserLists(IUserIdDTO userDTO, bool getOwnedListsFirst)
        {
            var tweetListsDTO = _listsQueryExecutor.GetUserLists(userDTO, getOwnedListsFirst);
            return _listsFactory.GenerateTweetListsFromDTO(tweetListsDTO);
        }

        public IEnumerable<ITweetList> GetUserLists(long userId, bool getOwnedListsFirst)
        {
            var tweetListsDTO = _listsQueryExecutor.GetUserLists(userId, getOwnedListsFirst);
            return _listsFactory.GenerateTweetListsFromDTO(tweetListsDTO);
        }

        public IEnumerable<ITweetList> GetUserLists(string userScreenName, bool getOwnedListsFirst)
        {
            var tweetListsDTO = _listsQueryExecutor.GetUserLists(userScreenName, getOwnedListsFirst);
            return _listsFactory.GenerateTweetListsFromDTO(tweetListsDTO);
        }

        // Update List
        public ITweetList UpdateList(ITweetList tweetList, IListUpdateParameters parameters)
        {
            return UpdateList(tweetList.TweetListDTO, parameters);
        }

        public ITweetList UpdateList(ITweetListDTO tweetListDTO, IListUpdateParameters parameters)
        {
            var identifier = _listIdentifierFactory.Create(tweetListDTO);
            return UpdateList(identifier, parameters);
        }

        public ITweetList UpdateList(long listId, IListUpdateParameters parameters)
        {
            var identifier = _listIdentifierFactory.Create(listId);
            return UpdateList(identifier, parameters);
        }

        public ITweetList UpdateList(string slug, IUser owner, IListUpdateParameters parameters)
        {
            if (owner == null)
            {
                throw new ArgumentException("Owner cannot be null");
            }

            return UpdateList(slug, owner.UserDTO, parameters);
        }

        public ITweetList UpdateList(string slug, IUserIdDTO ownerDTO, IListUpdateParameters parameters)
        {
            var identifier = _listIdentifierFactory.Create(slug, ownerDTO);
            return UpdateList(identifier, parameters);
        }

        public ITweetList UpdateList(string slug, long ownerId, IListUpdateParameters parameters)
        {
            var identifier = _listIdentifierFactory.Create(slug, ownerId);
            return UpdateList(identifier, parameters);
        }

        public ITweetList UpdateList(string slug, string ownerScreenName, IListUpdateParameters parameters)
        {
            var identifier = _listIdentifierFactory.Create(slug, ownerScreenName);
            return UpdateList(identifier, parameters);
        }

        public ITweetList UpdateList(IListIdentifier identifier, IListUpdateParameters parameters)
        {
            var tweetListDTO = _listsQueryExecutor.UpdateList(identifier, parameters);
            return _listsFactory.GenerateTweetListFromDTO(tweetListDTO);
        }

        // Destroy List
        public bool DestroyList(ITweetList tweetList)
        {
            if (tweetList == null)
            {
                throw new ArgumentException("TweetList cannot be null");
            }

            return DestroyList(tweetList.TweetListDTO);
        }

        public bool DestroyList(ITweetListDTO tweetListDTO)
        {
            var identifier = _listIdentifierFactory.Create(tweetListDTO);
            return DestroyList(identifier);
        }

        public bool DestroyList(long listId)
        {
            var listIdentifier = _listIdentifierFactory.Create(listId);
            return DestroyList(listIdentifier);
        }

        public bool DestroyList(string slug, IUser owner)
        {
            if (owner == null)
            {
                throw new ArgumentException("Owner cannot be null");
            }

            return DestroyList(slug, owner.UserDTO);
        }

        public bool DestroyList(string slug, IUserIdDTO ownerDTO)
        {
            var identifier = _listIdentifierFactory.Create(slug, ownerDTO);
            return DestroyList(identifier);
        }

        public bool DestroyList(string slug, string ownerScreenName)
        {
            var identifier = _listIdentifierFactory.Create(slug, ownerScreenName);
            return DestroyList(identifier);
        }

        public bool DestroyList(string slug, long ownerId)
        {
            var identifier = _listIdentifierFactory.Create(slug, ownerId);
            return DestroyList(identifier);
        }

        public bool DestroyList(IListIdentifier identifier)
        {
            return _listsQueryExecutor.DestroyList(identifier);
        }

        // Get Tweets from List
        public IEnumerable<ITweet> GetTweetsFromList(ITweetList tweetList)
        {
            if (tweetList == null)
            {
                return null;
            }

            return GetTweetsFromList(tweetList.TweetListDTO);
        }

        public IEnumerable<ITweet> GetTweetsFromList(ITweetListDTO tweetListDTO)
        {
            var identifier = _listIdentifierFactory.Create(tweetListDTO);
            return GetTweetsFromList(identifier);
        }

        public IEnumerable<ITweet> GetTweetsFromList(long listId)
        {
            var identifier = _listIdentifierFactory.Create(listId);
            return GetTweetsFromList(identifier);
        }

        public IEnumerable<ITweet> GetTweetsFromList(string slug, IUser owner)
        {
            if (owner == null)
            {
                throw new ArgumentException("Owner cannot be null");
            }

            return GetTweetsFromList(slug, owner.UserDTO);
        }

        public IEnumerable<ITweet> GetTweetsFromList(string slug, IUserIdDTO ownerDTO)
        {
            var identifier = _listIdentifierFactory.Create(slug, ownerDTO);
            return GetTweetsFromList(identifier);
        }

        public IEnumerable<ITweet> GetTweetsFromList(string slug, string ownerScreenName)
        {
            var identifier = _listIdentifierFactory.Create(slug, ownerScreenName);
            return GetTweetsFromList(identifier);
        }

        public IEnumerable<ITweet> GetTweetsFromList(string slug, long ownerId)
        {
            var identifier = _listIdentifierFactory.Create(slug, ownerId);
            return GetTweetsFromList(identifier);
        }

        public IEnumerable<ITweet> GetTweetsFromList(IListIdentifier identifier)
        {
            var tweetsDTO = _listsQueryExecutor.GetTweetsFromList(identifier);
            return _tweetFactory.GenerateTweetsFromDTO(tweetsDTO);
        }

        // Get Members of List
        public IEnumerable<IUser> GetMembersOfList(ITweetList tweetList, int maxNumberOfUsersToRetrieve = 100)
        {
            if (tweetList == null)
            {
                throw new ArgumentException("TweetList cannot be null");
            }

            return GetMembersOfList(tweetList.TweetListDTO, maxNumberOfUsersToRetrieve);
        }

        public IEnumerable<IUser> GetMembersOfList(ITweetListDTO tweetListDTO, int maxNumberOfUsersToRetrieve = 100)
        {
            var identifier = _listIdentifierFactory.Create(tweetListDTO);
            return GetMembersOfList(identifier, maxNumberOfUsersToRetrieve);
        }

        public IEnumerable<IUser> GetMembersOfList(long listId, int maxNumberOfUsersToRetrieve = 100)
        {
            var identifier = _listIdentifierFactory.Create(listId);
            return GetMembersOfList(identifier, maxNumberOfUsersToRetrieve);
        }

        public IEnumerable<IUser> GetMembersOfList(string slug, IUser owner, int maxNumberOfUsersToRetrieve = 100)
        {
            if (owner == null)
            {
                throw new ArgumentException("Owner cannot be null");
            }

            return GetMembersOfList(slug, owner.UserDTO, maxNumberOfUsersToRetrieve);
        }

        public IEnumerable<IUser> GetMembersOfList(string slug, IUserIdDTO ownerDTO, int maxNumberOfUsersToRetrieve = 100)
        {
            var identifier = _listIdentifierFactory.Create(slug, ownerDTO);
            return GetMembersOfList(identifier, maxNumberOfUsersToRetrieve);
        }

        public IEnumerable<IUser> GetMembersOfList(string slug, string ownerScreenName, int maxNumberOfUsersToRetrieve = 100)
        {
            var identifier = _listIdentifierFactory.Create(slug, ownerScreenName);
            return GetMembersOfList(identifier, maxNumberOfUsersToRetrieve);
        }

        public IEnumerable<IUser> GetMembersOfList(string slug, long ownerId, int maxNumberOfUsersToRetrieve = 100)
        {
            var identifier = _listIdentifierFactory.Create(slug, ownerId);
            return GetMembersOfList(identifier, maxNumberOfUsersToRetrieve);
        }

        public IEnumerable<IUser> GetMembersOfList(IListIdentifier identifier, int maxNumberOfUsersToRetrieve = 100)
        {
            var usersDTO = _listsQueryExecutor.GetMembersOfList(identifier, maxNumberOfUsersToRetrieve);
            return _userFactory.GenerateUsersFromDTO(usersDTO);
        }
    }
}