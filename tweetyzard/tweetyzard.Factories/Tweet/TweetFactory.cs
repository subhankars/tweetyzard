using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TweetinviCore;
using TweetinviCore.Helpers;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.Models;
using TweetinviLogic.JsonConverters;

namespace TweetinviFactories.Tweet
{
    public class TweetFactory : ITweetFactory
    {
        private readonly ITweetFactoryQueryExecutor _tweetDTOFactory;
        private readonly IUnityFactory<ITweet> _tweetUnityFactory;
        private readonly IUnityFactory<IMention> _mentionUnityFactory;
        private readonly IUnityFactory<IOEmbedTweet> _oembedTweetUnityFactory;
        private readonly IJsonObjectConverter _jsonObjectConverter;

        public TweetFactory(
            ITweetFactoryQueryExecutor tweetDTOFactory,
            IUnityFactory<ITweet> tweetUnityFactory,
            IUnityFactory<IMention> mentionFactory,
            IUnityFactory<IOEmbedTweet> oembedTweetUnityFactory,
            IJsonObjectConverter jsonObjectConverter)
        {
            _tweetDTOFactory = tweetDTOFactory;
            _tweetUnityFactory = tweetUnityFactory;
            _mentionUnityFactory = mentionFactory;
            _oembedTweetUnityFactory = oembedTweetUnityFactory;
            _jsonObjectConverter = jsonObjectConverter;
        }

        // Get Tweet
        public ITweet GetTweet(long tweetId)
        {
            var tweetDTO = _tweetDTOFactory.GetTweetDTO(tweetId);
            return GenerateTweetFromDTO(tweetDTO);
        }

        // Create Tweet
        public ITweet CreateTweet(string text)
        {
            var tweetDTO = _tweetDTOFactory.CreateTweetDTO(text);
            return GenerateTweetFromDTO(tweetDTO);
        }

        // Generate Tweet from Json
        public ITweet GenerateTweetFromJson(string jsonTweet)
        {
            var tweetDTO = _jsonObjectConverter.DeserializeObject<ITweetDTO>(jsonTweet);
            if (tweetDTO.Id == TweetinviConstants.DEFAULT_ID)
            {
                return null;
            }

            return GenerateTweetFromDTO(tweetDTO);
        }

        // Generate Tweet From DTO
        public ITweet GenerateTweetFromDTO(ITweetDTO tweetDTO)
        {
            if (tweetDTO == null)
            {
                return null;
            }

            var parameterOverride = _mentionUnityFactory.GenerateParameterOverrideWrapper("tweetDTO", tweetDTO);
            var tweet = _tweetUnityFactory.Create(parameterOverride);

            return tweet;
        }

        public IEnumerable<ITweet> GenerateTweetsFromDTO(IEnumerable<ITweetDTO> tweetsDTO)
        {
            if (tweetsDTO == null)
            {
                return null;
            }

            List<ITweet> tweets = new List<ITweet>();
            var tweetsDTOArray = tweetsDTO.ToArray();

            for (int i = 0; i < tweetsDTOArray.Length; ++i)
            {
                var tweet = GenerateTweetFromDTO(tweetsDTOArray[i]);
                if (tweet != null)
                {
                    tweets.Add(tweet);
                }
            }

            return tweets;
        }

        // Generate OEmbedTweet from DTO
        public IOEmbedTweet GenerateOEmbedTweetFromDTO(IOEmbedTweetDTO oEmbedTweetDTO)
        {
            if (oEmbedTweetDTO == null)
            {
                return null;
            }

            var parameterOverride = _mentionUnityFactory.GenerateParameterOverrideWrapper("oEmbedTweetDTO", oEmbedTweetDTO);
            var oEmbedTweet = _oembedTweetUnityFactory.Create(parameterOverride);

            return oEmbedTweet;
        }

        // Generate Mention from DTO
        public IMention GenerateMentionFromDTO(ITweetDTO tweetDTO)
        {
            if (tweetDTO == null)
            {
                return null;
            }

            var parameterOverride = _mentionUnityFactory.GenerateParameterOverrideWrapper("tweetDTO", tweetDTO);
            var mention = _mentionUnityFactory.Create(parameterOverride);

            return mention;
        }

        public IEnumerable<IMention> GenerateMentionsFromDTO(IEnumerable<ITweetDTO> tweetsDTO)
        {
            if (tweetsDTO == null)
            {
                return null;
            }

            return tweetsDTO.Select(GenerateMentionFromDTO).ToList();
        }
    }
}