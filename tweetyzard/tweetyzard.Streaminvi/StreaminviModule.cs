using Microsoft.Practices.Unity;
using Streaminvi.Helpers;
using Streaminvi.Model;
using TweetinviCore;
using TweetinviCore.Interfaces.Models;
using TweetinviCore.Interfaces.Streaminvi;
using TweetinviLogic.Model;

namespace Streaminvi
{
    public class StreaminviModule : IModule
    {
        private readonly IUnityContainer _container;

        public StreaminviModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<IUserStream, UserStream>();
            _container.RegisterType<ITweetStream, TweetStream>();
            _container.RegisterType<ISampleStream, SampleStream>();
            _container.RegisterType<ITrackedStream, TrackedStream>();
            _container.RegisterType<IFilteredStream, FilteredStream>();

            _container.RegisterType<IWarningMessage, WarningMessage>();
            _container.RegisterType<IWarningMessageTooManyFollowers, WarningMessageTooManyFollowers>();
            _container.RegisterType<IWarningMessageFallingBehind, WarningMessageFallingBehind>();

            _container.RegisterType<IStreamResultGenerator, StreamResultGenerator>();
            _container.RegisterType(typeof (IStreamTrackManager<>), typeof (StreamTrackManager<>));
        }
    }
}