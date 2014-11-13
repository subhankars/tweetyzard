using System;
using System.Collections.Generic;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.Parameters;

namespace Tweetinvi
{
    public static class TweetList
    {
        [ThreadStatic]
        private static ITweetListFactory _tweetListFactory;
        public static ITweetListFactory TweetListFactory
        {
            get
            {
                if (_tweetListFactory == null)
                {
                    Initialize();
                }
                
                return _tweetListFactory;
            }
        }

        [ThreadStatic]
        private static ITweetListController _tweetlistController;
        public static ITweetListController TweetListController
        {
            get
            {
                if (_tweetlistController == null)
                {
                    Initialize();
                }

                return _tweetlistController;
            }
        }

        private static readonly IUnityFactory<IListUpdateParameters> _listUpdateParametersUnityFactory;

        static TweetList()
        {
            Initialize();

            _listUpdateParametersUnityFactory = TweetinviContainer.Resolve<IUnityFactory<IListUpdateParameters>>();
        }

        private static void Initialize()
        {
            _tweetListFactory = TweetinviContainer.Resolve<ITweetListFactory>();
            _tweetlistController = TweetinviContainer.Resolve<ITweetListController>();
        }
       
        // Get Existing List
        public static ITweetList GetExistingList(ITweetList list)
        {
            return TweetListFactory.GetExistingTweetList(list);
        }

        public static ITweetList GetExistingList(ITweetListDTO listDTO)
        {
            return TweetListFactory.GetExistingTweetList(listDTO);
        }
        
        public static ITweetList GetExistingList(long listId)
        {
            return TweetListFactory.GetExistingTweetList(listId);
        }

        public static ITweetList GetExistingList(string slug, IUser user)
        {
            return TweetListFactory.GetExistingTweetList(slug, user);
        }

        public static ITweetList GetExistingList(string slug, IUserIdDTO userDTO)
        {
            return TweetListFactory.GetExistingTweetList(slug, userDTO);
        }

        public static ITweetList GetExistingList(string slug, long userId)
        {
            return TweetListFactory.GetExistingTweetList(slug, userId);
        }

        public static ITweetList GetExistingList(string slug, string userScreenName)
        {
            return TweetListFactory.GetExistingTweetList(slug, userScreenName);
        }

        // Get UserLists
        public static IEnumerable<ITweetList> GetUserLists(IUser user, bool getOwnedListsFirst)
        {
            return TweetListController.GetUserLists(user, getOwnedListsFirst);
        }

        public static IEnumerable<ITweetList> GetUserLists(IUserIdDTO userDTO, bool getOwnedListsFirst)
        {
            return TweetListController.GetUserLists(userDTO, getOwnedListsFirst);
        }

        public static IEnumerable<ITweetList> GetUserLists(long userId, bool getOwnedListsFirst)
        {
            return TweetListController.GetUserLists(userId, getOwnedListsFirst);
        }

        public static IEnumerable<ITweetList> GetUserLists(string userScreenName, bool getOwnedListsFirst)
        {
            return TweetListController.GetUserLists(userScreenName, getOwnedListsFirst);
        }

        // Create List
        public static ITweetList CreateList(string name, PrivacyMode privacyMode, string description = null)
        {
            return TweetListFactory.CreateList(name, privacyMode, description);
        }

        // Update List
        public static ITweetList UpdateList(ITweetList tweetList, IListUpdateParameters parameters)
        {
            return TweetListController.UpdateList(tweetList, parameters);
        }

        public static ITweetList UpdateList(ITweetListDTO tweetListDTO, IListUpdateParameters parameters)
        {
            return TweetListController.UpdateList(tweetListDTO, parameters);
        }

        public static ITweetList UpdateList(long listId, IListUpdateParameters parameters)
        {
            return TweetListController.UpdateList(listId, parameters);
        }

        public static ITweetList UpdateList(string slug, IUser owner, IListUpdateParameters parameters)
        {
            return TweetListController.UpdateList(slug, owner, parameters);
        }

        public static ITweetList UpdateList(string slug, IUserIdDTO ownerDTO, IListUpdateParameters parameters)
        {
            return TweetListController.UpdateList(slug, ownerDTO, parameters);
        }

        public static ITweetList UpdateList(string slug, long ownerId, IListUpdateParameters parameters)
        {
            return TweetListController.UpdateList(slug, ownerId, parameters);
        }

        public static ITweetList UpdateList(string slug, string ownerScreenName, IListUpdateParameters parameters)
        {
            return TweetListController.UpdateList(slug, ownerScreenName, parameters);
        }

        // Destroy List
        public static bool DestroyList(ITweetList tweetList)
        {
            return TweetListController.DestroyList(tweetList);
        }

        public static bool DestroyList(ITweetListDTO tweetListDTO)
        {
            return TweetListController.DestroyList(tweetListDTO);
        }

        public static bool DestroyList(long listId)
        {
            return TweetListController.DestroyList(listId);
        }

        public static bool DestroyList(string slug, IUser owner)
        {
            return TweetListController.DestroyList(slug, owner);
        }

        public static bool DestroyList(string slug, IUserDTO ownerDTO)
        {
            return TweetListController.DestroyList(slug, ownerDTO);
        }

        public static bool DestroyList(string slug, long ownerId)
        {
            return TweetListController.DestroyList(slug, ownerId);
        }

        public static bool DestroyList(string slug, string ownerScreenName)
        {
            return TweetListController.DestroyList(slug, ownerScreenName);
        }

        // Get Tweets from List
        public static IEnumerable<ITweet> GetTweetsFromList(ITweetList tweetList)
        {
            return _tweetlistController.GetTweetsFromList(tweetList);
        }

        public static IEnumerable<ITweet> GetTweetsFromList(ITweetListDTO tweetListDTO)
        {
            return _tweetlistController.GetTweetsFromList(tweetListDTO);
        }

        public static IEnumerable<ITweet> GetTweetsFromList(long listId)
        {
            return _tweetlistController.GetTweetsFromList(listId);
        }

        public static IEnumerable<ITweet> GetTweetsFromList(string slug, IUser owner)
        {
            return _tweetlistController.GetTweetsFromList(slug, owner);
        }

        public static IEnumerable<ITweet> GetTweetsFromList(string slug, IUserIdDTO ownerDTO)
        {
            return _tweetlistController.GetTweetsFromList(slug, ownerDTO);
        }

        public static IEnumerable<ITweet> GetTweetsFromList(string slug, string ownerScreenName)
        {
            return _tweetlistController.GetTweetsFromList(slug, ownerScreenName);
        }

        public static IEnumerable<ITweet> GetTweetsFromList(string slug, long ownerId)
        {
            return _tweetlistController.GetTweetsFromList(slug, ownerId);
        }

        // Get Members of List
        public static IEnumerable<IUser> GetMembersOfList(ITweetList tweetList, int maxNumberOfUsersToRetrieve = 100)
        {
            return _tweetlistController.GetMembersOfList(tweetList, maxNumberOfUsersToRetrieve);
        }

        public static IEnumerable<IUser> GetMembersOfList(ITweetListDTO tweetListDTO, int maxNumberOfUsersToRetrieve = 100)
        {
            return _tweetlistController.GetMembersOfList(tweetListDTO, maxNumberOfUsersToRetrieve);
        }

        public static IEnumerable<IUser> GetMembersOfList(long listId, int maxNumberOfUsersToRetrieve = 100)
        {
            return _tweetlistController.GetMembersOfList(listId, maxNumberOfUsersToRetrieve);
        }

        public static IEnumerable<IUser> GetMembersOfList(string slug, IUser owner, int maxNumberOfUsersToRetrieve = 100)
        {
            return _tweetlistController.GetMembersOfList(slug, owner, maxNumberOfUsersToRetrieve);
        }

        public static IEnumerable<IUser> GetMembersOfList(string slug, IUserIdDTO ownerDTO, int maxNumberOfUsersToRetrieve = 100)
        {
            return _tweetlistController.GetMembersOfList(slug, ownerDTO, maxNumberOfUsersToRetrieve);
        }

        public static IEnumerable<IUser> GetMembersOfList(string slug, string ownerScreenName, int maxNumberOfUsersToRetrieve = 100)
        {
            return _tweetlistController.GetMembersOfList(slug, ownerScreenName, maxNumberOfUsersToRetrieve);
        }

        public static IEnumerable<IUser> GetMembersOfList(string slug, long ownerId, int maxNumberOfUsersToRetrieve = 100)
        {
            return _tweetlistController.GetMembersOfList(slug, ownerId, maxNumberOfUsersToRetrieve);
        }

        // Generate ListUpdateParameter
        public static IListUpdateParameters GenerateUpdateParameters()
        {
            return _listUpdateParametersUnityFactory.Create();
        }
    }
}