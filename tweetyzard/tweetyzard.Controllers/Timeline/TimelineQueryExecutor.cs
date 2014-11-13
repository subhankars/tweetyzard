using System;
using System.Collections.Generic;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.DTO;

namespace TweetinviControllers.Timeline
{
    public interface ITimelineQueryExecutor
    {
        // Home Timeline
        IEnumerable<ITweetDTO> GetHomeTimeline(int maximumTweets = 40, bool excludeReplies = false);

        // User Timeline
        IEnumerable<ITweetDTO> GetUserTimeline(IUser user, int maximumTweets = 40, bool excludeReplies = false);
        IEnumerable<ITweetDTO> GetUserTimeline(IUserIdDTO userDTO, int maximumTweets = 40, bool excludeReplies = false);
        IEnumerable<ITweetDTO> GetUserTimeline(long userId, int maximumTweets = 40, bool excludeReplies = false);
        IEnumerable<ITweetDTO> GetUserTimeline(string userScreenName, int maximumTweets = 40, bool excludeReplies = false);

        // Mention Timeline
        IEnumerable<ITweetDTO> GetMentionsTimeline(int maximumTweets = 40, bool excludeReplies = false);
    }

    public class TimelineQueryExecutor : ITimelineQueryExecutor
    {
        private readonly ITwitterAccessor _twitterAccessor;
        private readonly ITimelineQueryGenerator _timelineQueryGenerator;

        public TimelineQueryExecutor(
            ITwitterAccessor twitterAccessor,
            ITimelineQueryGenerator timelineQueryGenerator)
        {
            _twitterAccessor = twitterAccessor;
            _timelineQueryGenerator = timelineQueryGenerator;
        }

        // Home Timeline
        public IEnumerable<ITweetDTO> GetHomeTimeline(int maximumTweets = 40, bool excludeReplies = false)
        {
            string query = _timelineQueryGenerator.GetHomeTimelineQuery(maximumTweets, excludeReplies);
            return _twitterAccessor.ExecuteGETQuery<IEnumerable<ITweetDTO>>(query);
        }

        // User Timeline
        public IEnumerable<ITweetDTO> GetUserTimeline(IUser user, int maximumTweets = 40, bool excludeReplies = false)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }

            return GetUserTimeline(user.UserDTO, maximumTweets, excludeReplies);
        }

        public IEnumerable<ITweetDTO> GetUserTimeline(IUserIdDTO userDTO, int maximumTweets = 40, bool excludeReplies = false)
        {
            string query = _timelineQueryGenerator.GetUserTimelineQuery(userDTO, maximumTweets, excludeReplies);
            return _twitterAccessor.ExecuteGETQuery<IEnumerable<ITweetDTO>>(query);
        }

        public IEnumerable<ITweetDTO> GetUserTimeline(long userId, int maximumTweets = 40, bool excludeReplies = false)
        {
            string query = _timelineQueryGenerator.GetUserTimelineQuery(userId, maximumTweets, excludeReplies);
            return _twitterAccessor.ExecuteGETQuery<IEnumerable<ITweetDTO>>(query);
        }

        public IEnumerable<ITweetDTO> GetUserTimeline(string userScreenName, int maximumTweets = 40, bool excludeReplies = false)
        {
            string query = _timelineQueryGenerator.GetUserTimelineQuery(userScreenName, maximumTweets, excludeReplies);
            return _twitterAccessor.ExecuteGETQuery<IEnumerable<ITweetDTO>>(query);
        }

        // Mention Timeline
        public IEnumerable<ITweetDTO> GetMentionsTimeline(int maximumTweets = 40, bool excludeReplies = false)
        {
            string query = _timelineQueryGenerator.GetMentionsTimelineQuery(maximumTweets, excludeReplies);
            return _twitterAccessor.ExecuteGETQuery<IEnumerable<ITweetDTO>>(query);
        }
    }
}