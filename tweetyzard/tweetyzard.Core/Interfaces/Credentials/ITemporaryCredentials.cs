using TweetinviCore.Interfaces.oAuth;

namespace TweetinviCore.Interfaces.Credentials
{
    public interface ITemporaryCredentials : IConsumerCredentials
    {
        string AuthorizationKey { get; set; }
        string AuthorizationSecret { get; set; }

        string VerifierCode { get; set; }
    }
}