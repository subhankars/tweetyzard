using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.Streaminvi;

namespace Tweetinvi
{
    public static class Stream
    {
        private static readonly IUnityFactory<IUserStream> _userStreamFactory;
        private static readonly IUnityFactory<ITweetStream> _tweetStreamUnityFactory;
        private static readonly IUnityFactory<ISampleStream> _sampleStreamUnityFactory;
        private static readonly IUnityFactory<ITrackedStream> _trackedStreamUnityFactory;
        private static readonly IUnityFactory<IFilteredStream> _filteredStreamUnityFactory;

        static Stream()
        {
            _userStreamFactory = TweetinviContainer.Resolve<IUnityFactory<IUserStream>>();
            _tweetStreamUnityFactory = TweetinviContainer.Resolve<IUnityFactory<ITweetStream>>();
            _sampleStreamUnityFactory = TweetinviContainer.Resolve<IUnityFactory<ISampleStream>>();
            _trackedStreamUnityFactory = TweetinviContainer.Resolve<IUnityFactory<ITrackedStream>>();
            _filteredStreamUnityFactory = TweetinviContainer.Resolve<IUnityFactory<IFilteredStream>>();
        }

        public static ITweetStream CreateTweetStream()
        {
            return _tweetStreamUnityFactory.Create();
        }

        public static ITrackedStream CreateTrackedStream()
        {
            return _trackedStreamUnityFactory.Create();
        }

        public static ISampleStream CreateSampleStream()
        {
            return _sampleStreamUnityFactory.Create();
        }

        public static IFilteredStream CreateFilteredStream()
        {
            return _filteredStreamUnityFactory.Create();
        }

        public static IUserStream CreateUserStream()
        {
            return _userStreamFactory.Create();
        }
    }
}