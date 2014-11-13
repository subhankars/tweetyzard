using System.Collections.Generic;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Models;

namespace TweetinviCore.Interfaces.Factories
{
    public interface ITweetFactory
    {
        ITweet GetTweet(long tweetId);
        ITweet CreateTweet(string text);

        // Generate Tweet From Json
        ITweet GenerateTweetFromJson(string jsonTweet);

        // Generate Tweet from DTO
        ITweet GenerateTweetFromDTO(ITweetDTO tweetDTO);
        IEnumerable<ITweet> GenerateTweetsFromDTO(IEnumerable<ITweetDTO> tweetsDTO);

        // Generate OEmbedTweet from DTO
        IOEmbedTweet GenerateOEmbedTweetFromDTO(IOEmbedTweetDTO oEmbedTweetDTO);

        // Generate Mention from DTO
        IMention GenerateMentionFromDTO(ITweetDTO tweetDTO);
        IEnumerable<IMention> GenerateMentionsFromDTO(IEnumerable<ITweetDTO> tweetsDTO);
    }
}