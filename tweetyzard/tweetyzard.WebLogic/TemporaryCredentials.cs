using TweetinviCore.Interfaces.Credentials;

namespace TweetinviWebLogic
{
    public class TemporaryCredentials : ConsumerCredentials, ITemporaryCredentials
    {
        public TemporaryCredentials(string consumerKey, string consumerSecret) 
            : base(consumerKey, consumerSecret)
        {
        }

        public string AuthorizationKey { get; set; }
        public string AuthorizationSecret { get; set; }
        public string VerifierCode { get; set; }
    }
}