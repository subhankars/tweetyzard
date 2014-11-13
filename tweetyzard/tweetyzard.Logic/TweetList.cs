using System;
using System.Collections.Generic;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.Parameters;

namespace TweetinviLogic
{
    public class TweetList : ITweetList
    {
        private readonly IUserFactory _userFactory;
        private readonly ITweetListController _tweetListController;
        private ITweetListDTO _tweetListDTO;
        private IUser _creator;

        public TweetList(
            IUserFactory userFactory,
            ITweetListController tweetListController,
            ITweetListDTO tweetListDTO)
        {
            _userFactory = userFactory;
            _tweetListController = tweetListController;
            TweetListDTO = tweetListDTO;
        }

        public ITweetListDTO TweetListDTO
        {
            get { return _tweetListDTO; }
            set
            {
                _tweetListDTO = value;
                UpdateCreator();
            }
        }

        public long Id
        {
            get { return _tweetListDTO.Id; }
        }

        public string IdStr
        {
            get { return _tweetListDTO.IdStr; }
        }

        public string Slug
        {
            get { return _tweetListDTO.Slug; }
        }

        public string Name
        {
            get { return _tweetListDTO.Name; }
        }

        public string FullName
        {
            get { return _tweetListDTO.FullName; }
        }

        public IUser Creator
        {
            get { return _creator; }
        }

        public DateTime CreatedAt
        {
            get { return _tweetListDTO.CreatedAt; }
        }

        public string Uri
        {
            get { return _tweetListDTO.Uri; }

        }

        public string Description
        {
            get { return _tweetListDTO.Description; }
        }

        public bool Following
        {
            get { return _tweetListDTO.Following; }
        }

        public PrivacyMode PrivacyMode
        {
            get { return _tweetListDTO.PrivacyMode; }
        }

        public int MemberCount
        {
            get { return _tweetListDTO.MemberCount; }
        }

        public int SubscriberCount
        {
            get { return _tweetListDTO.SubscriberCount; }
        }

        public bool Update(IListUpdateParameters parameters)
        {
            var updateList = _tweetListController.UpdateList(_tweetListDTO, parameters);

            if (updateList != null)
            {
                _tweetListDTO = updateList.TweetListDTO;
                return true;
            }

            return false;
        }

        public bool Destroy()
        {
            return _tweetListController.DestroyList(_tweetListDTO);
        }

        public IEnumerable<ITweet> GetTweets()
        {
            return _tweetListController.GetTweetsFromList(_tweetListDTO);
        }

        public IEnumerable<IUser> GetMembers(int maxNumberOfUsersToRetrieve = 100)
        {
            return _tweetListController.GetMembersOfList(_tweetListDTO, maxNumberOfUsersToRetrieve);
        }

        private void UpdateCreator()
        {
            if (_tweetListDTO != null)
            {
                _creator = _userFactory.GenerateUserFromDTO(_tweetListDTO.Creator);
            }
        }
    }
}