using System;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;
using TweetinviFactories.Properties;
using TweetinviLogic.DTO;

namespace TweetinviFactories.Tweet
{
    public interface ITweetFactoryQueryExecutor
    {
        ITweetDTO GetTweetDTO(long tweetId);
        ITweetDTO CreateTweetDTO(string text);
    }

    public class TweetFactoryQueryExecutor : ITweetFactoryQueryExecutor
    {
        private readonly ITwitterAccessor _twitterAccessor;
        private readonly IUnityFactory<ITweetDTO> _tweetDTOUnityFactory;

        public TweetFactoryQueryExecutor(
            ITwitterAccessor twitterAccessor,
            IUnityFactory<ITweetDTO> tweetDTOUnityFactory)
        {
            _twitterAccessor = twitterAccessor;
            _tweetDTOUnityFactory = tweetDTOUnityFactory;
        }

        public ITweetDTO GetTweetDTO(long tweetId)
        {
            string query = String.Format(Resources.Tweet_Get, tweetId);
            return _twitterAccessor.ExecuteGETQuery<TweetDTO>(query);
        }

        public ITweetDTO CreateTweetDTO(string text)
        {
            var tweetDTO = _tweetDTOUnityFactory.Create();
            tweetDTO.Text = text;

            return tweetDTO;
        }
    }
}