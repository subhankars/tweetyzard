using System;
using System.Text;
using TweetinviControllers.Geo;
using TweetinviControllers.Properties;
using TweetinviCore.Helpers;
using TweetinviCore.Interfaces.DTO;

namespace TweetinviControllers.Tweet
{
    public interface ITweetQueryGenerator
    {
        // Publish Tweet
        string GetPublishTweetQuery(ITweetDTO tweetDTO);

        // Publish Tweet in reply to
        string GetPublishTweetInReplyToQuery(ITweetDTO tweetToPublish, ITweetDTO tweetToReplyTo);
        string GetPublishTweetInReplyToQuery(ITweetDTO tweet, long tweetIdToRespondTo);

        // Publish Retweet
        string GetPublishRetweetQuery(ITweetDTO tweetDTO);
        string GetPublishRetweetQuery(long tweetId);

        // Get Retweets
        string GetRetweetsQuery(ITweetDTO tweetDTO);
        string GetRetweetsQuery(long tweetId);

        // Destroy Tweet
        string GetDestroyTweetQuery(ITweetDTO tweetDTO);
        string GetDestroyTweetQuery(long tweetId);

        // Generate OembedTweet
        string GetGenerateOEmbedTweetQuery(ITweetDTO tweetDTO);
        string GetGenerateOEmbedTweetQuery(long tweetId);

        // Favorite Tweet
        string GetFavouriteTweetQuery(ITweetDTO tweetDTO);
        string GetFavouriteTweetQuery(long tweetId);

        string GetUnFavouriteTweetQuery(ITweetDTO tweetDTO);
        string GetUnFavouriteTweetQuery(long tweetId);
    }

    public class TweetQueryGenerator : ITweetQueryGenerator
    {
        private readonly IGeoQueryGenerator _geoQueryGenerator;
        private readonly ITweetQueryValidator _tweetQueryValidator;
        private readonly ITwitterStringFormatter _twitterStringFormatter;

        public TweetQueryGenerator(
            IGeoQueryGenerator geoQueryGenerator,
            ITweetQueryValidator tweetQueryValidator,
            ITwitterStringFormatter twitterStringFormatter)
        {
            _geoQueryGenerator = geoQueryGenerator;
            _tweetQueryValidator = tweetQueryValidator;
            _twitterStringFormatter = twitterStringFormatter;
        }

        private string CleanupString(string source)
        {
            return _twitterStringFormatter.TwitterEncode(source);
        }

        // Publish Tweet
        public string GetPublishTweetQuery(ITweetDTO tweetDTO)
        {
            if (!_tweetQueryValidator.CanTweetDTOBePublished(tweetDTO))
            {
                return null;
            }

            string baseQuery = String.Format(Resources.Tweet_Publish, CleanupString(tweetDTO.Text));
            return AddAdditionalParameters(tweetDTO, baseQuery);
        }

        // Publish Tweet in reply to
        public string GetPublishTweetInReplyToQuery(ITweetDTO tweetToPublish, ITweetDTO tweetToReplyTo)
        {
            if (!_tweetQueryValidator.CanTweetDTOBePublished(tweetToPublish) ||
                !_tweetQueryValidator.IsTweetPublished(tweetToReplyTo))
            {
                return null;
            }

            string baseQuery = String.Format(Resources.Tweet_PublishInReplyTo, CleanupString(tweetToPublish.Text), tweetToReplyTo.Id);
            return AddAdditionalParameters(tweetToPublish, baseQuery);
        }

        public string GetPublishTweetInReplyToQuery(ITweetDTO tweetToPublish, long tweetIdToRespondTo)
        {
            if (!_tweetQueryValidator.CanTweetDTOBePublished(tweetToPublish))
            {
                return null;
            }

            string baseQuery = String.Format(Resources.Tweet_PublishInReplyTo, CleanupString(tweetToPublish.Text), tweetIdToRespondTo);
            return AddAdditionalParameters(tweetToPublish, baseQuery);
        }

        // Publish Retweet
        public string GetPublishRetweetQuery(ITweetDTO tweetDTO)
        {
            if (!_tweetQueryValidator.IsTweetPublished(tweetDTO))
            {
                return null;
            }

            return GetPublishRetweetQuery(tweetDTO.Id);
        }

        public string GetPublishRetweetQuery(long tweetId)
        {
            return String.Format(Resources.Tweet_Retweet_Publish, tweetId);
        }

        // Get Retweets
        public string GetRetweetsQuery(ITweetDTO tweetDTO)
        {
            if (!_tweetQueryValidator.IsTweetPublished(tweetDTO))
            {
                return null;
            }

            return GetRetweetsQuery(tweetDTO.Id);
        }

        public string GetRetweetsQuery(long tweetId)
        {
            return String.Format(Resources.Tweet_Retweet_GetRetweets, tweetId);
        }

        // Destroy Tweet
        public string GetDestroyTweetQuery(ITweetDTO tweetDTO)
        {
            if (!_tweetQueryValidator.CanTweetDTOBeDestroyed(tweetDTO))
            {
                return null;
            }

            return GetDestroyTweetQuery(tweetDTO.Id);
        }

        public string GetDestroyTweetQuery(long tweetId)
        {
            return String.Format(Resources.Tweet_Destroy, tweetId);
        }

        // Favorite Tweet
        public string GetFavouriteTweetQuery(ITweetDTO tweetDTO)
        {
            if (!_tweetQueryValidator.IsTweetPublished(tweetDTO))
            {
                return null;
            }

            return GetFavouriteTweetQuery(tweetDTO.Id);
        }

        public string GetFavouriteTweetQuery(long tweetId)
        {
            return String.Format(Resources.Tweet_Favorite_Create, tweetId);
        }

        // Unfavourite Tweet
        public string GetUnFavouriteTweetQuery(ITweetDTO tweetDTO)
        {
            if (!_tweetQueryValidator.IsTweetPublished(tweetDTO))
            {
                return null;
            }

            return GetUnFavouriteTweetQuery(tweetDTO.Id);
        }

        public string GetUnFavouriteTweetQuery(long tweetId)
        {
            return String.Format(Resources.Tweet_Favorite_Destroy, tweetId);
        }

        // OEmbed Tweet
        public string GetGenerateOEmbedTweetQuery(ITweetDTO tweetDTO)
        {
            if (!_tweetQueryValidator.IsTweetPublished(tweetDTO))
            {
                return null;
            }

            return GetGenerateOEmbedTweetQuery(tweetDTO.Id);
        }

        public string GetGenerateOEmbedTweetQuery(long tweetId)
        {
            return String.Format(Resources.Tweet_GenerateOEmbed, tweetId);
        }

        // Helper
        private string AddAdditionalParameters(ITweetDTO tweet, string baseQuery)
        {
            StringBuilder query = new StringBuilder(baseQuery);

            string placeIdParameter = _geoQueryGenerator.GeneratePlaceIdParameter(tweet.PlaceId);
            if (!String.IsNullOrEmpty(placeIdParameter))
            {
                query.Append(String.Format("&{0}", placeIdParameter));
            }

            string coordinatesParameter = _geoQueryGenerator.GenerateGeoParameter(tweet.Coordinates);
            if (!String.IsNullOrEmpty(coordinatesParameter))
            {
                query.Append(String.Format("&{0}", coordinatesParameter));
            }

            return query.ToString();
        }
    }
}