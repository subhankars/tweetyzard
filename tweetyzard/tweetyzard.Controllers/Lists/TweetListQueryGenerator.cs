using System;
using System.Text;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Parameters;
using TweetinviControllers.Properties;
using TweetinviCore.Interfaces.QueryGenerators;
using TweetinviCore.Interfaces.QueryValidators;

namespace TweetinviControllers.Lists
{
    public interface ITweetListQueryGenerator
    {
        string GetUserListsQuery(IUserIdDTO userIdDTO, bool getOwnedListsFirst);
        string GetUserListsQuery(long userId, bool getOwnedListsFirst);
        string GetUserListsQuery(string userScreenName, bool getOwnedListsFirst);

        string GetUpdateListQuery(IListIdentifier identifier, IListUpdateParameters parameters);
        string GetDestroyListQuery(IListIdentifier identifier);
        string GetTweetsFromListQuery(IListIdentifier identifier);
        string GetMembersFromListQuery(IListIdentifier identifier);
    }

    public class TweetListQueryGenerator : ITweetListQueryGenerator
    {
        private readonly ITweetListQueryValidator _listsQueryValidator;
        private readonly IUserQueryParameterGenerator _userQueryParameterGenerator;
        private readonly IUserQueryValidator _userQueryValidator;
        private readonly ITweetListQueryParameterGenerator _tweetListQueryParameterGenerator;

        public TweetListQueryGenerator(
            ITweetListQueryValidator listsQueryValidator,
            IUserQueryParameterGenerator userQueryParameterGenerator,
            IUserQueryValidator userQueryValidator,
            ITweetListQueryParameterGenerator tweetListQueryParameterGenerator)
        {
            _listsQueryValidator = listsQueryValidator;
            _userQueryParameterGenerator = userQueryParameterGenerator;
            _userQueryValidator = userQueryValidator;
            _tweetListQueryParameterGenerator = tweetListQueryParameterGenerator;
        }

        public string GetUserListsQuery(IUserIdDTO userIdDTO, bool getOwnedListsFirst)
        {
            if (!_userQueryValidator.CanUserBeIdentified(userIdDTO))
            {
                return null;
            }

            var userIdentifier = _userQueryParameterGenerator.GenerateIdOrScreenNameParameter(userIdDTO);
            return GenerateUserListsQuery(userIdentifier, getOwnedListsFirst);
        }

        public string GetUserListsQuery(long userId, bool getOwnedListsFirst)
        {
            if (!_userQueryValidator.IsUserIdValid(userId))
            {
                return null;
            }

            var userIdentifier = _userQueryParameterGenerator.GenerateUserIdParameter(userId);
            return GenerateUserListsQuery(userIdentifier, getOwnedListsFirst);
        }

        public string GetUserListsQuery(string userScreenName, bool getOwnedListsFirst)
        {
            if (!_userQueryValidator.IsScreenNameValid(userScreenName))
            {
                return null;
            }

            var userIdentifier = _userQueryParameterGenerator.GenerateScreenNameParameter(userScreenName);
            return GenerateUserListsQuery(userIdentifier, getOwnedListsFirst);
        }

        private string GenerateUserListsQuery(string userIdentifier, bool getOwnedListsFirst)
        {
            return String.Format(Resources.List_GetUserLists, userIdentifier, getOwnedListsFirst);
        }

        public string GetUpdateListQuery(IListIdentifier identifier, IListUpdateParameters parameters)
        {
            if (!_listsQueryValidator.IsListIdentifierValid(identifier) || 
                !_listsQueryValidator.IsListUpdateParametersValid(parameters))
            {
                return null;
            }

            var listIdentifierParameter = _tweetListQueryParameterGenerator.GenerateIdentifierParameter(identifier);
            var updateQueryParameters = GenerateUpdateAdditionalParameters(parameters);

            var queryParameters = String.Format("{0}{1}", listIdentifierParameter, updateQueryParameters);
            return String.Format(Resources.List_Update, queryParameters);
        }

        private string GenerateUpdateAdditionalParameters(IListUpdateParameters parameters)
        {
            string privacyModeParameter = String.Format(Resources.List_PrivacyModeParameter, parameters.PrivacyMode.ToString().ToLower());

            StringBuilder queryParameterBuilder = new StringBuilder(privacyModeParameter);

            if (_listsQueryValidator.IsDescriptionParameterValid(parameters.Description))
            {
                string descriptionParameter = String.Format(Resources.List_DescriptionParameter, parameters.Description);
                queryParameterBuilder.Append(descriptionParameter);
            }

            if (_listsQueryValidator.IsNameParameterValid(parameters.Name))
            {
                string nameParameter = String.Format(Resources.List_NameParameter, parameters.Name);
                queryParameterBuilder.Append(nameParameter);
            }

            return queryParameterBuilder.ToString();
        }

        public string GetDestroyListQuery(IListIdentifier identifier)
        {
            if (!_listsQueryValidator.IsListIdentifierValid(identifier))
            {
                return null;
            }

            var identifierParameter = _tweetListQueryParameterGenerator.GenerateIdentifierParameter(identifier);
            return String.Format(Resources.List_Destroy, identifierParameter);
        }

        public string GetTweetsFromListQuery(IListIdentifier identifier)
        {
            if (!_listsQueryValidator.IsListIdentifierValid(identifier))
            {
                return null;
            }

            var identifierParameter = _tweetListQueryParameterGenerator.GenerateIdentifierParameter(identifier);
            return String.Format(Resources.List_GetTweetsFromList, identifierParameter);
        }

        public string GetMembersFromListQuery(IListIdentifier identifier)
        {
            if (!_listsQueryValidator.IsListIdentifierValid(identifier))
            {
                return null;
            }

            var identifierParameter = _tweetListQueryParameterGenerator.GenerateIdentifierParameter(identifier);
            return String.Format(Resources.List_Members, identifierParameter);
        }
    }
}