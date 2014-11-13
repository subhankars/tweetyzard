using System.Collections.Generic;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.DTO;

namespace TweetinviControllers.Tweet
{
    public interface ITweetQueryExecutor
    {
        // Publish Tweet
        ITweetDTO PublishTweet(ITweetDTO tweetToPublish);
        ITweetDTO PublishTweetInReplyTo(ITweetDTO tweetToPublish, ITweetDTO tweetToReplyTo);
        ITweetDTO PublishTweetInReplyTo(ITweetDTO tweetToPublish, long tweetIdToReplyTo);

        // Publish Retweet
        ITweetDTO PublishRetweet(ITweetDTO tweetToRetweet);
        ITweetDTO PublishRetweet(long tweetId);

        // Get Retweets
        IEnumerable<ITweetDTO> GetRetweets(ITweetDTO tweet);
        IEnumerable<ITweetDTO> GetRetweets(long tweetId);

        // Destroy Tweet
        bool DestroyTweet(ITweetDTO tweet);
        bool DestroyTweet(long tweetId);

        // Favorite Tweet
        bool FavouriteTweet(ITweetDTO tweet);
        bool FavouriteTweet(long tweetId);

        bool UnFavouriteTweet(ITweetDTO tweet);
        bool UnFavouriteTweet(long tweetId);

        // Generate OEmbedTweet
        IOEmbedTweetDTO GenerateOEmbedTweet(ITweetDTO tweetDTO);
        IOEmbedTweetDTO GenerateOEmbedTweet(long tweetId);
    }

    public class TweetQueryExecutor : ITweetQueryExecutor
    {
        private readonly ITweetQueryGenerator _tweetQueryGenerator;
        private readonly ITwitterAccessor _twitterAccessor;

        public TweetQueryExecutor(
            ITweetQueryGenerator tweetQueryGenerator,
            ITwitterAccessor twitterAccessor)
        {
            _tweetQueryGenerator = tweetQueryGenerator;
            _twitterAccessor = twitterAccessor;
        }

        // Publish Tweet
        public ITweetDTO PublishTweet(ITweetDTO tweetToPublish)
        {
            string query = _tweetQueryGenerator.GetPublishTweetQuery(tweetToPublish);
            return _twitterAccessor.ExecutePOSTQuery<ITweetDTO>(query);
        }

        public ITweetDTO PublishTweetInReplyTo(ITweetDTO tweetToPublish, ITweetDTO tweetToReplyTo)
        {
            string query = _tweetQueryGenerator.GetPublishTweetInReplyToQuery(tweetToPublish, tweetToReplyTo);
            return _twitterAccessor.ExecutePOSTQuery<ITweetDTO>(query);
        }

        public ITweetDTO PublishTweetInReplyTo(ITweetDTO tweetToPublish, long tweetIdToReplyTo)
        {
            string query = _tweetQueryGenerator.GetPublishTweetInReplyToQuery(tweetToPublish, tweetIdToReplyTo);
            return _twitterAccessor.ExecutePOSTQuery<ITweetDTO>(query);
        }

        // Publish Retweet
        public ITweetDTO PublishRetweet(ITweetDTO tweetToRetweet)
        {
            string query = _tweetQueryGenerator.GetPublishRetweetQuery(tweetToRetweet);
            return _twitterAccessor.ExecutePOSTQuery<ITweetDTO>(query);
        }

        public ITweetDTO PublishRetweet(long tweetId)
        {
            string query = _tweetQueryGenerator.GetPublishRetweetQuery(tweetId);
            return _twitterAccessor.ExecutePOSTQuery<ITweetDTO>(query);
        }

        // Get Retweets
        public IEnumerable<ITweetDTO> GetRetweets(ITweetDTO tweet)
        {
            string query = _tweetQueryGenerator.GetRetweetsQuery(tweet);
            return _twitterAccessor.ExecuteGETQuery<IEnumerable<ITweetDTO>>(query);
        }

        public IEnumerable<ITweetDTO> GetRetweets(long tweetId)
        {
            string query = _tweetQueryGenerator.GetRetweetsQuery(tweetId);
            return _twitterAccessor.ExecuteGETQuery<IEnumerable<ITweetDTO>>(query);
        }

        // Destroy Tweet
        public bool DestroyTweet(ITweetDTO tweet)
        {
            string query = _tweetQueryGenerator.GetDestroyTweetQuery(tweet);
            return _twitterAccessor.TryExecutePOSTQuery(query);
        }

        public bool DestroyTweet(long tweetId)
        {
            string query = _tweetQueryGenerator.GetDestroyTweetQuery(tweetId);
            return _twitterAccessor.TryExecutePOSTQuery(query);
        }

        // Favourite Tweet
        public bool FavouriteTweet(ITweetDTO tweet)
        {
            string query = _tweetQueryGenerator.GetFavouriteTweetQuery(tweet);
            return _twitterAccessor.TryExecutePOSTQuery(query);
        }

        public bool FavouriteTweet(long tweetId)
        {
            string query = _tweetQueryGenerator.GetFavouriteTweetQuery(tweetId);
            return _twitterAccessor.TryExecutePOSTQuery(query);
        }

        public bool UnFavouriteTweet(ITweetDTO tweet)
        {
            string query = _tweetQueryGenerator.GetUnFavouriteTweetQuery(tweet);
            return _twitterAccessor.TryExecutePOSTQuery(query);
        }

        public bool UnFavouriteTweet(long tweetId)
        {
            string query = _tweetQueryGenerator.GetUnFavouriteTweetQuery(tweetId);
            return _twitterAccessor.TryExecutePOSTQuery(query);
        }

        // Generate OEmbed Tweet
        public IOEmbedTweetDTO GenerateOEmbedTweet(ITweetDTO tweet)
        {
            string query = _tweetQueryGenerator.GetGenerateOEmbedTweetQuery(tweet);
            return _twitterAccessor.ExecuteGETQuery<IOEmbedTweetDTO>(query);
        }

        public IOEmbedTweetDTO GenerateOEmbedTweet(long tweetId)
        {
            string query = _tweetQueryGenerator.GetGenerateOEmbedTweetQuery(tweetId);
            return _twitterAccessor.ExecuteGETQuery<IOEmbedTweetDTO>(query);
        }
    }
}