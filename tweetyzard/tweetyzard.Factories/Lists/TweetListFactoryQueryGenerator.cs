using System;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces.Parameters;
using TweetinviCore.Interfaces.QueryGenerators;
using TweetinviCore.Interfaces.QueryValidators;
using TweetinviFactories.Properties;

namespace TweetinviFactories.Lists
{
    public interface ITweetListFactoryQueryGenerator
    {
        string GetCreateListQuery(string name, PrivacyMode privacyMode, string description);
        string GetListByIdQuery(IListIdentifier listIdentifier);
    }

    public class TweetListFactoryQueryGenerator : ITweetListFactoryQueryGenerator
    {
        private readonly ITweetListQueryValidator _listsQueryValidator;
        private readonly ITweetListQueryParameterGenerator _listQueryParameterGenerator;

        public TweetListFactoryQueryGenerator(
            ITweetListQueryValidator listsQueryValidator,
            ITweetListQueryParameterGenerator listQueryParameterGenerator)
        {
            _listsQueryValidator = listsQueryValidator;
            _listQueryParameterGenerator = listQueryParameterGenerator;
        }

        public string GetCreateListQuery(string name, PrivacyMode privacyMode, string description)
        {
            var baseQuery = String.Format(Resources.List_Create, name, privacyMode.ToString().ToLower());

            if (_listsQueryValidator.IsDescriptionParameterValid(description))
            {
                baseQuery += String.Format(Resources.List_Create_DescriptionParameter, description);
            }

            return baseQuery;
        }

        public string GetListByIdQuery(IListIdentifier listIdentifier)
        {
            if (!_listsQueryValidator.IsListIdentifierValid(listIdentifier))
            {
                return null;
            }

            var identifierParameter = _listQueryParameterGenerator.GenerateIdentifierParameter(listIdentifier);
            return String.Format(Resources.List_GetExistingList, identifierParameter);
        }
    }
}