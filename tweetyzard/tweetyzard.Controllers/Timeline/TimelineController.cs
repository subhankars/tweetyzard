using System;
using System.Collections.Generic;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;

namespace TweetinviControllers.Timeline
{
    public class TimelineController : ITimelineController
    {
        private readonly ITweetFactory _tweetFactory;
        private readonly ITimelineQueryExecutor _timelineQueryExecutor;

        public TimelineController(
            ITweetFactory tweetFactory,
            ITimelineQueryExecutor timelineQueryExecutor)
        {
            _tweetFactory = tweetFactory;
            _timelineQueryExecutor = timelineQueryExecutor;
        }

        // Home Timeline
        public IEnumerable<ITweet> GetHomeTimeline(int maximumTweets = 40, bool excludeReplies = false)
        {
            var timelineDTO = _timelineQueryExecutor.GetHomeTimeline(maximumTweets, excludeReplies);
            return _tweetFactory.GenerateTweetsFromDTO(timelineDTO);
        }

        // User Timeline
        public IEnumerable<ITweet> GetUserTimeline(IUser user, int maximumTweets = 40, bool excludeReplies = false)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }

            return GetUserTimeline(user.UserDTO, maximumTweets, excludeReplies);
        }

        public IEnumerable<ITweet> GetUserTimeline(IUserIdDTO userDTO, int maximumTweets = 40, bool excludeReplies = false)
        {
            var tweetsDTO = _timelineQueryExecutor.GetUserTimeline(userDTO, maximumTweets, excludeReplies);
            return _tweetFactory.GenerateTweetsFromDTO(tweetsDTO);
        }

        public IEnumerable<ITweet> GetUserTimeline(long userId, int maximumTweets = 40, bool excludeReplies = false)
        {
            var tweetsDTO = _timelineQueryExecutor.GetUserTimeline(userId, maximumTweets, excludeReplies);
            return _tweetFactory.GenerateTweetsFromDTO(tweetsDTO);
        }

        public IEnumerable<ITweet> GetUserTimeline(string userScreenName, int maximumTweets = 40, bool excludeReplies = false)
        {
            var tweetsDTO = _timelineQueryExecutor.GetUserTimeline(userScreenName, maximumTweets, excludeReplies);
            return _tweetFactory.GenerateTweetsFromDTO(tweetsDTO);
        }

        // Mention Timeline
        public IEnumerable<IMention> GetMentionsTimeline(int maximumTweets = 40, bool excludeReplies = false)
        {
            var timelineDTO = _timelineQueryExecutor.GetMentionsTimeline(maximumTweets, excludeReplies);
            return _tweetFactory.GenerateMentionsFromDTO(timelineDTO);
        }
    }
}