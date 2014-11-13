using TweetinviCore.Interfaces.DTO;

namespace TweetinviControllers.Tweet
{
    public interface ITweetQueryValidator
    {
        bool CanTweetDTOBePublished(ITweetDTO tweetDTO);
        bool CanTweetDTOBeDestroyed(ITweetDTO tweetDTO);
        bool IsTweetPublished(ITweetDTO tweetDTO);
    }

    public class TweetQueryValidator : ITweetQueryValidator
    {
        public bool CanTweetDTOBePublished(ITweetDTO tweet)
        {
            return tweet != null && !tweet.IsTweetPublished && !tweet.IsTweetDestroyed;
        }

        public bool CanTweetDTOBeDestroyed(ITweetDTO tweet)
        {
            return tweet != null && tweet.IsTweetPublished && !tweet.IsTweetDestroyed;
        }

        public bool IsTweetPublished(ITweetDTO tweet)
        {
            return tweet != null && tweet.IsTweetPublished && !tweet.IsTweetDestroyed;
        }
    }
}