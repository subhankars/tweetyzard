using System;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.DTO;

namespace TweetinviControllers.Timeline
{
    public interface ITimelineJsonController
    {
        // Home Timeline
        string GetHomeTimeline(int maximumTweets = 40, bool excludeReplies = false);

        // User Timeline
        string GetUserTimeline(IUser user, int maximumTweets = 40, bool excludeReplies = false);
        string GetUserTimeline(IUserIdDTO userDTO, int maximumTweets = 40, bool excludeReplies = false);
        string GetUserTimeline(long userId, int maximumTweets = 40, bool excludeReplies = false);
        string GetUserTimeline(string userScreenName, int maximumTweets = 40, bool excludeReplies = false);

        // Mention Timeline
        string GetMentionsTimeline(int maximumTweets = 40, bool excludeReplies = false);
    }

    public class TimelineJsonController : ITimelineJsonController
    {
        private readonly ITimelineQueryGenerator _timelineQueryGenerator;
        private readonly ITwitterAccessor _twitterAccessor;

        public TimelineJsonController(
            ITimelineQueryGenerator timelineQueryGenerator,
            ITwitterAccessor twitterAccessor)
        {
            _timelineQueryGenerator = timelineQueryGenerator;
            _twitterAccessor = twitterAccessor;
        }

        public string GetHomeTimeline(int maximumTweets = 40, bool excludeReplies = false)
        {
            string query = _timelineQueryGenerator.GetHomeTimelineQuery(maximumTweets, excludeReplies);
            return _twitterAccessor.ExecuteJsonGETQuery(query);
        }

        public string GetUserTimeline(IUser user, int maximumTweets = 40, bool excludeReplies = false)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }

            return GetUserTimeline(user.UserDTO, maximumTweets, excludeReplies);
        }

        public string GetUserTimeline(IUserIdDTO userDTO, int maximumTweets = 40, bool excludeReplies = false)
        {
            string query = _timelineQueryGenerator.GetUserTimelineQuery(userDTO, maximumTweets, excludeReplies);
            return _twitterAccessor.ExecuteJsonGETQuery(query);
        }

        public string GetUserTimeline(long userId, int maximumTweets = 40, bool excludeReplies = false)
        {
            string query = _timelineQueryGenerator.GetUserTimelineQuery(userId, maximumTweets, excludeReplies);
            return _twitterAccessor.ExecuteJsonGETQuery(query);
        }

        public string GetUserTimeline(string userScreenName, int maximumTweets = 40, bool excludeReplies = false)
        {
            string query = _timelineQueryGenerator.GetUserTimelineQuery(userScreenName, maximumTweets, excludeReplies);
            return _twitterAccessor.ExecuteJsonGETQuery(query);
        }

        public string GetMentionsTimeline(int maximumTweets = 40, bool excludeReplies = false)
        {
            string query = _timelineQueryGenerator.GetMentionsTimelineQuery(maximumTweets, excludeReplies);
            return _twitterAccessor.ExecuteJsonGETQuery(query);
        }
    }
}