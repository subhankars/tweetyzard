using Streaminvi.Properties;
using TweetinviCore.Helpers;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.oAuth;
using TweetinviCore.Interfaces.Streaminvi;
using TweetinviCore.Wrappers;

namespace Streaminvi
{
    public class SampleStream : TweetStream, ISampleStream
    {
        public SampleStream(
            IStreamResultGenerator streamResultGenerator, 
            IJsonObjectConverter jsonObjectConverter, 
            IJObjectStaticWrapper jObjectStaticWrapper, 
            ITweetFactory tweetFactory, 
            IOAuthToken oAuthToken) 
            : base(streamResultGenerator, jsonObjectConverter, jObjectStaticWrapper, tweetFactory, oAuthToken)
        {
        }

        public void StartStream()
        {
            StartStream(Resources.Stream_Sample);
        }
    }
}