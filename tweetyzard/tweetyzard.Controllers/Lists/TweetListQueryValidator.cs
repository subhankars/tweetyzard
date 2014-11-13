using System;
using TweetinviCore;
using TweetinviCore.Interfaces.Parameters;
using TweetinviCore.Interfaces.QueryValidators;

namespace TweetinviControllers.Lists
{
    public class TweetListQueryValidator : ITweetListQueryValidator
    {
        public bool IsListUpdateParametersValid(IListUpdateParameters parameters)
        {
            return parameters != null;
        }

        public bool IsDescriptionParameterValid(string description)
        {
            return !String.IsNullOrEmpty(description);
        }

        public bool IsNameParameterValid(string name)
        {
            return !String.IsNullOrEmpty(name);
        }

        public bool IsListIdentifierValid(IListIdentifier listIdentifier)
        {
            if (listIdentifier == null)
            {
                return false;
            }

            if (listIdentifier.ListId != TweetinviConstants.DEFAULT_ID)
            {
                return true;
            }

            bool isOwnerIdentifierValid = IsOwnerIdValid(listIdentifier.OwnerId) || IsOwnerScreenNameValid(listIdentifier.OwnerScreenName);
            return IsSlugValid(listIdentifier.Slug) && isOwnerIdentifierValid;
        }

        public bool IsOwnerScreenNameValid(string ownerScreenName)
        {
            return !String.IsNullOrEmpty(ownerScreenName);
        }

        public bool IsOwnerIdValid(long ownderId)
        {
            return ownderId != 0;
        }

        public bool IsSlugValid(string slug)
        {
            return !String.IsNullOrEmpty(slug);
        }
    }
}