using System.Collections.Generic;
using System.Linq;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.Credentials.QueryDTO;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Parameters;

namespace TweetinviControllers.Lists
{
    public interface ITweetListQueryExecutor
    {
        IEnumerable<ITweetListDTO> GetUserLists(IUserIdDTO userDTO, bool getOwnedListsFirst);
        IEnumerable<ITweetListDTO> GetUserLists(long userId, bool getOwnedListsFirst);
        IEnumerable<ITweetListDTO> GetUserLists(string userScreenName, bool getOwnedListsFirst);

        ITweetListDTO UpdateList(IListIdentifier identifier, IListUpdateParameters parameters);
        bool DestroyList(IListIdentifier identifier);
        IEnumerable<ITweetDTO> GetTweetsFromList(IListIdentifier identifier);
        IEnumerable<IUserDTO> GetMembersOfList(IListIdentifier identifier, int maxNumberOfUsersToRetrieve);
    }

    public class TweetListQueryExecutor : ITweetListQueryExecutor
    {
        private readonly ITweetListQueryGenerator _listsQueryGenerator;
        private readonly ITwitterAccessor _twitterAccessor;

        public TweetListQueryExecutor(ITweetListQueryGenerator listsQueryGenerator, ITwitterAccessor twitterAccessor)
        {
            _listsQueryGenerator = listsQueryGenerator;
            _twitterAccessor = twitterAccessor;
        }

        // Get User Lists
        public IEnumerable<ITweetListDTO> GetUserLists(IUserIdDTO userDTO, bool getOwnedListsFirst)
        {
            var query = _listsQueryGenerator.GetUserListsQuery(userDTO, getOwnedListsFirst);
            return _twitterAccessor.ExecuteGETQuery<IEnumerable<ITweetListDTO>>(query);
        }

        public IEnumerable<ITweetListDTO> GetUserLists(long userId, bool getOwnedListsFirst)
        {
            var query = _listsQueryGenerator.GetUserListsQuery(userId, getOwnedListsFirst);
            return _twitterAccessor.ExecuteGETQuery<IEnumerable<ITweetListDTO>>(query);
        }

        public IEnumerable<ITweetListDTO> GetUserLists(string userScreenName, bool getOwnedListsFirst)
        {
            var query = _listsQueryGenerator.GetUserListsQuery(userScreenName, getOwnedListsFirst);
            return _twitterAccessor.ExecuteGETQuery<IEnumerable<ITweetListDTO>>(query);
        }

        // Update List
        public ITweetListDTO UpdateList(IListIdentifier identifier, IListUpdateParameters parameters)
        {
            string query = _listsQueryGenerator.GetUpdateListQuery(identifier, parameters);
            return _twitterAccessor.ExecutePOSTQuery<ITweetListDTO>(query);
        }

        // Destroy List
        public bool DestroyList(IListIdentifier identifier)
        {
            string query = _listsQueryGenerator.GetDestroyListQuery(identifier);
            return _twitterAccessor.TryExecutePOSTQuery(query);
        }

        // Get Tweets from list
        public IEnumerable<ITweetDTO> GetTweetsFromList(IListIdentifier identifier)
        {
            string query = _listsQueryGenerator.GetTweetsFromListQuery(identifier);
            return _twitterAccessor.ExecuteGETQuery<IEnumerable<ITweetDTO>>(query);
        }

        public IEnumerable<IUserDTO> GetMembersOfList(IListIdentifier identifier, int maxNumberOfUsersToRetrieve)
        {
            string query = _listsQueryGenerator.GetMembersFromListQuery(identifier);
            var usersCursorQueryResults = _twitterAccessor.ExecuteCursorGETQuery<IUserCursorQueryResultDTO>(query, maxNumberOfUsersToRetrieve);
            if (usersCursorQueryResults == null)
            {
                return null;
            }

            return usersCursorQueryResults.SelectMany(x => x.Users);
        }
    }
}