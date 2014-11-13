using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using TweetinviCore;
using TweetinviCore.Extensions;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.Models;
using TweetinviLogic.Model;

namespace TweetinviLogic
{
    /// <summary>
    /// Class representing a Tweet
    /// https://dev.twitter.com/docs/api/1/get/statuses/show/%3Aid
    /// </summary>
    [Serializable]
    public class Tweet : ICloneable, ITweet
    {
        public const Int16 MAX_TWEET_SIZE = 140;

        private ITweetDTO _tweetDTO;

        private readonly ITweetController _tweetController;
        private readonly ITweetFactory _tweetFactory;
        private readonly IUserFactory _userFactory;
        
        #region Public Attributes

        private IUser _creator;
        private void UpdateCreator()
        {
            _creator = _tweetDTO == null ? null : _userFactory.GenerateUserFromDTO(_tweetDTO.Creator);
        }

        public ITweetDTO TweetDTO
        {
            get { return _tweetDTO; }
            set
            {
                _tweetDTO = value;
                UpdateCreator();
            }
        }

        #region Twitter API Attributes

        public long Id
        {
            get { return _tweetDTO.Id; }
            set { _tweetDTO.Id = value; }
        }

        public string IdStr
        {
            get { return _tweetDTO.IdStr; }
            set { _tweetDTO.IdStr = value; }
        }

        public string Text
        {
            get { return _tweetDTO.Text; }
            set { _tweetDTO.Text = value; }
        }

        public bool Favourited
        {
            get { return _tweetDTO.Favorited; }
        }

        public ICoordinates Coordinates
        {
            get { return _tweetDTO.Coordinates; }
            set { _tweetDTO.Coordinates = value; }
        }

        public ITweetEntities Entities
        {
            get { return _tweetDTO.Entities; }
            set { _tweetDTO.Entities = value; }
        }

        public IUser Creator
        {
            get { return _creator; }
            set
            {
                _tweetDTO.Creator = value.UserDTO;
                UpdateCreator();
            }
        }

        public DateTime CreatedAt
        {
            get { return _tweetDTO.CreatedAt; }
        }

        public string Source
        {
            get { return _tweetDTO.Source; }
            set { _tweetDTO.Source = value; }
        }

        public bool Truncated
        {
            get { return _tweetDTO.Truncated; }
        }

        public long? InReplyToStatusId
        {
            get { return _tweetDTO.InReplyToStatusId; }
            set { _tweetDTO.InReplyToStatusId = value; }
        }

        public string InReplyToStatusIdStr
        {
            get { return _tweetDTO.InReplyToStatusIdStr; }
            set { _tweetDTO.InReplyToStatusIdStr = value; }
        }

        public long? InReplyToUserId
        {
            get { return _tweetDTO.InReplyToUserId; }
            set { _tweetDTO.InReplyToUserId = value; }
        }

        public string InReplyToUserIdStr
        {
            get { return _tweetDTO.InReplyToUserIdStr; }
            set { _tweetDTO.InReplyToUserIdStr = value; }
        }

        public string InReplyToScreenName
        {
            get { return _tweetDTO.InReplyToScreenName; }
            set { _tweetDTO.InReplyToScreenName = value; }
        }

        public int[] ContributorsIds
        {
            get { return _tweetDTO.ContributorsIds; }
            set { _tweetDTO.ContributorsIds = value; }
        }

        public int RetweetCount
        {
            get { return _tweetDTO.RetweetCount; }
        }

        public bool Retweeted
        {
            get { return _tweetDTO.Retweeted; }
        }

        public bool IsRetweet
        {
            get { return _tweetDTO.RetweetedTweetDTO != null; }
        }

        private ITweet _retweetedTweet;
        public ITweet RetweetedTweet
        {
            get
            {
                if (_retweetedTweet == null)
                {
                    _retweetedTweet = _tweetFactory.GenerateTweetFromDTO(_tweetDTO.RetweetedTweetDTO);
                }

                return _retweetedTweet;
            }
        }

        public bool PossiblySensitive
        {
            get { return _tweetDTO.PossiblySensitive; }
        }

        #endregion

        #region Tweetinvi API Accessors

        public List<IHashtagEntity> Hashtags
        {
            get
            {
                if (Entities != null)
                {
                    return Entities.Hashtags;
                }

                return null;
            }

            set
            {
                if (Entities != null)
                {
                    Entities.Hashtags = value;
                }
            }
        }

        public List<IUrlEntity> Urls
        {
            get
            {
                if (Entities != null)
                {
                    return Entities.Urls;
                }

                return null;
            }

            set
            {
                if (Entities != null)
                {
                    Entities.Urls = value;
                }
            }
        }

        public List<IMediaEntity> Media
        {
            get
            {
                if (Entities != null)
                {
                    return Entities.Medias;
                }

                return null;
            }

            set
            {
                if (Entities != null)
                {
                    Entities.Medias = value;
                }
            }
        }

        public List<IUserMentionEntity> UserMentions
        {
            get
            {
                if (Entities != null)
                {
                    return Entities.UserMentions;
                }

                return null;
            }

            set
            {
                if (Entities != null)
                {
                    Entities.UserMentions = value;
                }
            }
        }

        #endregion

        #region Tweetinvi API Attributes

        public int Length
        {
            get { return _tweetDTO.Text == null ? 0 : StringExtension.TweetLenght(_tweetDTO.Text); }
        }

        public bool IsTweetPublished
        {
            get { return _tweetDTO.IsTweetPublished; }
        }

        public bool IsTweetDestroyed
        {
            get { return _tweetDTO.IsTweetDestroyed; }
        }

        public DateTime TweetCreationDate = DateTime.Now;

        public List<ITweet> Retweets { get; set; }

        #endregion

        #endregion

        [InjectionConstructor]
        public Tweet(
            ITweetDTO tweetDTO,
            ITweetController tweetController,
            ITweetFactory tweetFactory,
            IUserFactory userFactory)
        {
            _tweetController = tweetController;
            _tweetFactory = tweetFactory;
            _userFactory = userFactory;

            TweetDTO = tweetDTO;
        }

        public int TweetRemainingCharacters()
        {
            return MAX_TWEET_SIZE - StringExtension.TweetLenght(Text);
        }

        public bool Publish()
        {
            return _tweetController.PublishTweet(this);
        }

        public ITweet PublishReply(string text)
        {
            var tweetToPublish = _tweetFactory.CreateTweet(text);
            if (!_tweetController.PublishTweetInReplyTo(tweetToPublish, this))
            {
                return null;
            }

            return tweetToPublish;
        }

        public bool PublishReply(ITweet replyTweet)
        {
            return _tweetController.PublishTweetInReplyTo(replyTweet, this);
        }

        public bool PublishInReplyTo(ITweet replyToTweet)
        {
            if (replyToTweet == null || replyToTweet.Id == TweetinviConstants.DEFAULT_ID)
            {
                return false;
            }

            return PublishInReplyTo(replyToTweet.Id);
        }

        public bool PublishInReplyTo(long replyToTweetId)
        {
            return _tweetController.PublishTweetInReplyTo(this, replyToTweetId);
        }

        public bool PublishWithGeo(ICoordinates coordinates)
        {
            _tweetDTO.Coordinates = coordinates;
            return _tweetController.PublishTweet(this);
        }

        public bool PublishWithGeo(double longitude, double latitude)
        {
            var coordinates = new Coordinates(longitude, latitude);
            return PublishWithGeo(coordinates);
        }

        public bool PublishWithGeoInReplyTo(double longitude, double latitude, ITweet replyToTweet)
        {
            if (replyToTweet == null || replyToTweet.Id == TweetinviConstants.DEFAULT_ID)
            {
                return false;
            }

            return PublishWithGeoInReplyTo(latitude, longitude, replyToTweet.Id);
        }

        public bool PublishWithGeoInReplyTo(double longitude, double latitude, long replyToTweetId)
        {
            var coordinates = new Coordinates(longitude, latitude);
            return PublishWithGeoInReplyTo(coordinates, replyToTweetId);
        }

        public bool PublishWithGeoInReplyTo(ICoordinates coordinates, ITweet replyToTweet)
        {
            if (replyToTweet == null || replyToTweet.Id == TweetinviConstants.DEFAULT_ID)
            {
                return false;
            }

            return PublishWithGeoInReplyTo(coordinates, replyToTweet.Id);
        }

        public bool PublishWithGeoInReplyTo(ICoordinates coordinates, long replyToTweetId)
        {
            return _tweetController.PublishTweetWithGeoInReplyTo(this, coordinates, replyToTweetId);
        }

        private bool CanTweetBeRetweeted()
        {
            return _tweetDTO != null && _tweetDTO.Id != TweetinviConstants.DEFAULT_ID && IsTweetPublished && !IsTweetDestroyed;
        }

        public ITweet PublishRetweet()
        {
            if (!CanTweetBeRetweeted())
            {
                return null;
            }

            return _tweetController.PublishRetweet(this);
        }

        public List<ITweet> GetRetweets()
        {
            return _tweetController.GetRetweets(_tweetDTO).ToList();
        }

        public IOEmbedTweet GenerateOEmbedTweet()
        {
            return _tweetController.GenerateOEmbedTweet(_tweetDTO);
        }

        public bool Destroy()
        {
            return _tweetController.DestroyTweet(_tweetDTO);
        }

        public void Favourite()
        {
            if (_tweetController.FavoriteTweet(_tweetDTO))
            {
                _tweetDTO.Favorited = true;
            }
        }

        public void UnFavourite()
        {
            if (_tweetController.UnFavoriteTweet(_tweetDTO))
            {
                _tweetDTO.Favorited = false;
            }
        }

        public override string ToString()
        {
            return String.Format("'{0}', {1}, '{2}', '{3}', '{4}', '{5}'",
                Id, CreatedAt, Creator.Name, Text, Creator.Language, TweetCreationDate);
        }

        public bool Equals(ITweet other)
        {
            // Equals is currently used to compare only if 2 tweets are the same
            // We do not look for the tweet version (DateTime)

            bool result = _tweetDTO.Equals(other.TweetDTO) &&
                          IsTweetPublished == other.IsTweetPublished &&
                          IsTweetDestroyed == other.IsTweetDestroyed;

            return result;
        }

        #region ICloneable
        
        /// <summary>
        /// Copy a Tweet into a new one
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return _tweetFactory.GenerateTweetFromDTO(_tweetDTO);
        }
        
        #endregion
    }
}