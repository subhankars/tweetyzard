using System;
using TweetinviControllers.Properties;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.QueryGenerators;
using TweetinviCore.Interfaces.QueryValidators;

namespace TweetinviControllers.Timeline
{
    public interface ITimelineQueryGenerator
    {
        // Home Timeline
        string GetHomeTimelineQuery(int maximumTweets, bool excludeReplies);

        // User Timeline
        string GetUserTimelineQuery(IUserIdDTO userDTO, int maximumTweets, bool excludeReplies);
        string GetUserTimelineQuery(long userId, int maximumTweets, bool excludeReplies);
        string GetUserTimelineQuery(string screenName, int maximumTweets, bool excludeReplies);

        // Mention Timeline
        string GetMentionsTimelineQuery(int maximumTweets, bool excludeReplies);
    }

    public class TimelineQueryGenerator : ITimelineQueryGenerator
    {
        private readonly IUserQueryParameterGenerator _userQueryParameterGenerator;
        private readonly IUserQueryValidator _userQueryValidator;

        public TimelineQueryGenerator(
            IUserQueryParameterGenerator userQueryGenerator,
            IUserQueryValidator userQueryValidator)
        {
            _userQueryParameterGenerator = userQueryGenerator;
            _userQueryValidator = userQueryValidator;
        }

        public string GetHomeTimelineQuery(int maximumTweets, bool excludeReplies)
        {
            return String.Format(Resources.Timeline_GetHomeTimeline, maximumTweets, excludeReplies);
        }

        public string GetUserTimelineQuery(IUserIdDTO userDTO, int maximumTweets, bool excludeReplies)
        {
            if (!_userQueryValidator.CanUserBeIdentified(userDTO))
            {
                return null;
            }

            var userParameter = _userQueryParameterGenerator.GenerateIdOrScreenNameParameter(userDTO);
            return GetUserTimelineBaseQuery(userParameter, maximumTweets, excludeReplies);
        }

        public string GetUserTimelineQuery(long userId, int maximumTweets, bool excludeReplies)
        {
            string userIdParameter = _userQueryParameterGenerator.GenerateUserIdParameter(userId);
            return GetUserTimelineBaseQuery(userIdParameter, maximumTweets, excludeReplies);
        }

        public string GetUserTimelineQuery(string screenName, int maximumTweets, bool excludeReplies)
        {
            if (!_userQueryValidator.IsScreenNameValid(screenName))
            {
                return null;
            }

            string userScreenNameParameter = _userQueryParameterGenerator.GenerateScreenNameParameter(screenName);
            return GetUserTimelineBaseQuery(userScreenNameParameter, maximumTweets, excludeReplies);
        }

        private string GetUserTimelineBaseQuery(string queryParameter, int maximumTweets, bool excludeReplies)
        {
            string baseQuery = String.Format(Resources.Timeline_GetUserTimeline, maximumTweets, excludeReplies);
            return String.Format("{0}&{1}", baseQuery, queryParameter);
        }

        public string GetMentionsTimelineQuery(int maximumTweets, bool excludeReplies)
        {
            return String.Format(Resources.Timeline_GetMentionsTimeline, maximumTweets, excludeReplies);
        }
    }
}