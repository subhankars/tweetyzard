using System;
using TweetinviCore;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.Parameters;
using TweetinviCore.Interfaces.QueryValidators;

namespace TweetinviLogic.Model
{
    public class ListIdentifierFactory : IListIdentifierFactory
    {
        private readonly IUnityFactory<IListIdentifier> _listIdentifierUnityFactory;
        private readonly IUserQueryValidator _userQueryValidator;

        public ListIdentifierFactory(
            IUnityFactory<IListIdentifier> listIdentifierUnityFactory,
            IUserQueryValidator userQueryValidator)
        {
            _listIdentifierUnityFactory = listIdentifierUnityFactory;
            _userQueryValidator = userQueryValidator;
        }

        public IListIdentifier Create(ITweetListDTO tweetListDTO)
        {
            if (tweetListDTO == null)
            {
                return null;
            }

            if (tweetListDTO.Id != TweetinviConstants.DEFAULT_ID)
            {
                return Create(tweetListDTO.Id);
            }

            if (!String.IsNullOrEmpty(tweetListDTO.Slug) && _userQueryValidator.CanUserBeIdentified(tweetListDTO.Creator))
            {
                if (_userQueryValidator.IsUserIdValid(tweetListDTO.Creator.Id))
                {
                    return Create(tweetListDTO.Slug, tweetListDTO.Creator.Id);
                }

                return Create(tweetListDTO.Slug, tweetListDTO.Creator.ScreenName);
            }

            return null;
        }

        public IListIdentifier Create(long listId)
        {
            var listIdentifier = _listIdentifierUnityFactory.Create();
            listIdentifier.ListId = listId;
            return listIdentifier;
        }

        public IListIdentifier Create(string slug, IUserIdDTO userDTO)
        {
            if (userDTO == null)
            {
                return null;
            }

            if (userDTO.Id != TweetinviConstants.DEFAULT_ID)
            {
                return Create(slug, userDTO.Id);
            }

            if (!String.IsNullOrEmpty(userDTO.ScreenName))
            {
                return Create(slug, userDTO.ScreenName);
            }

            return null;
        }

        public IListIdentifier Create(string slug, long ownerId)
        {
            var listIdentifier = _listIdentifierUnityFactory.Create();
            listIdentifier.Slug = slug;
            listIdentifier.OwnerId = ownerId;
            return listIdentifier;
        }

        public IListIdentifier Create(string slug, string ownerScreenName)
        {
            var listIdentifier = _listIdentifierUnityFactory.Create();
            listIdentifier.Slug = slug;
            listIdentifier.OwnerScreenName = ownerScreenName;
            return listIdentifier;
        }
    }

    public class ListIdentifier : IListIdentifier
    {
        public ListIdentifier()
        {
            ListId = TweetinviConstants.DEFAULT_ID;
            OwnerId = TweetinviConstants.DEFAULT_ID;
        }

        public long ListId { get; set; }
        public string Slug { get; set; }
        public long OwnerId { get; set; }
        public string OwnerScreenName { get; set; }
    }
}