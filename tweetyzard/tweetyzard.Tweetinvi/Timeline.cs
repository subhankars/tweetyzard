using System;
using System.Collections.Generic;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.DTO;

namespace Tweetinvi
{
    public static class Timeline
    {
        [ThreadStatic]
        private static ITimelineController _timelineController;
        public static ITimelineController TimelineController
        {
            get
            {
                if (_timelineController == null)
                {
                    Initialize();
                }

                return _timelineController;
            }
        }

        static Timeline()
        {
            Initialize();
        }

        private static void Initialize()
        {
            _timelineController = TweetinviContainer.Resolve<ITimelineController>();
        }

        // Home Timeline
        public static IEnumerable<ITweet> GetHomeTimeline(int maximumTweets = 40, bool excludeReplies = false)
        {
            return TimelineController.GetHomeTimeline(maximumTweets, excludeReplies);
        }

        // User Timeline
        public static IEnumerable<ITweet> GetUserTimeline(IUser user, int maximumTweets = 40, bool excludeReplies = false)
        {
            return TimelineController.GetUserTimeline(user, maximumTweets, excludeReplies);
        }

        public static IEnumerable<ITweet> GetUserTimeline(IUserIdDTO userDTO, int maximumTweets = 40, bool excludeReplies = false)
        {
            return TimelineController.GetUserTimeline(userDTO, maximumTweets, excludeReplies);
        }

        public static IEnumerable<ITweet> GetUserTimeline(long userId, int maximumTweets = 40, bool excludeReplies = false)
        {
            return TimelineController.GetUserTimeline(userId, maximumTweets, excludeReplies);
        }

        public static IEnumerable<ITweet> GetUserTimeline(string userScreenName, int maximumTweets = 40, bool excludeReplies = false)
        {
            return TimelineController.GetUserTimeline(userScreenName, maximumTweets, excludeReplies);
        }

        // Mention Timeline
        public static IEnumerable<IMention> GetMentionsTimeline(int maximumTweets = 40)
        {
            return TimelineController.GetMentionsTimeline(maximumTweets);
        }
    }
}