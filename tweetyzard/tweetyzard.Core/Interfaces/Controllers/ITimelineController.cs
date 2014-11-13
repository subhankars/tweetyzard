using System.Collections.Generic;
using TweetinviCore.Interfaces.DTO;

namespace TweetinviCore.Interfaces.Controllers
{
    public interface ITimelineController
    {
        // Home Timeline
        IEnumerable<ITweet> GetHomeTimeline(int maximumTweets = 40, bool excludeReplies = false);

        // User Timeline
        IEnumerable<ITweet> GetUserTimeline(IUser user, int maximumTweets = 40, bool excludeReplies = false);
        IEnumerable<ITweet> GetUserTimeline(IUserIdDTO userDTO, int maximumTweets = 40, bool excludeReplies = false);
        IEnumerable<ITweet> GetUserTimeline(long userId, int maximumTweets = 40, bool excludeReplies = false);
        IEnumerable<ITweet> GetUserTimeline(string userScreenName, int maximumTweets = 40, bool excludeReplies = false);

        // Mention Timeline
        IEnumerable<IMention> GetMentionsTimeline(int maximumTweets = 40, bool excludeReplies = false);
    }
}