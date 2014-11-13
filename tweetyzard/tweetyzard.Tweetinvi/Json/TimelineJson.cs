using System;
using TweetinviControllers.Timeline;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.DTO;

namespace Tweetinvi.Json
{
    public static class TimelineJson
    {
        [ThreadStatic]
        private static ITimelineJsonController _timelineJsonController;
        public static ITimelineJsonController TimelineJsonController
        {
            get
            {
                if (_timelineJsonController == null)
                {
                    Initialize();
                }

                return _timelineJsonController;
            }
        }

        static TimelineJson()
        {
            Initialize();
        }

        private static void Initialize()
        {
            _timelineJsonController = TweetinviContainer.Resolve<ITimelineJsonController>();
        }

        public static string GetHomeTimeline(int maximumTweets = 40, bool excludeReplies = false)
        {
            return TimelineJsonController.GetHomeTimeline(maximumTweets, excludeReplies);
        }

        public static string GetUserTimeline(IUser user, int maximumTweets = 40, bool excludeReplies = false)
        {
            return TimelineJsonController.GetUserTimeline(user, maximumTweets, excludeReplies);
        }

        public static string GetUserTimeline(IUserIdDTO userDTO, int maximumTweets = 40, bool excludeReplies = false)
        {
            return TimelineJsonController.GetUserTimeline(userDTO, maximumTweets, excludeReplies);
        }

        public static string GetUserTimeline(long userId, int maximumTweets = 40, bool excludeReplies = false)
        {
            return TimelineJsonController.GetUserTimeline(userId, maximumTweets, excludeReplies);
        }

        public static string GetUserTimeline(string userScreenName, int maximumTweets = 40, bool excludeReplies = false)
        {
            return TimelineJsonController.GetUserTimeline(userScreenName, maximumTweets, excludeReplies);
        }

        public static string GetMentionsTimeline(int maximumTweets = 40)
        {
            return TimelineJsonController.GetMentionsTimeline(maximumTweets);
        }
    }
}
