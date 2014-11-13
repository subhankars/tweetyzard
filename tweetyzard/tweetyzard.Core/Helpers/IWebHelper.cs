using System.IO;

namespace TweetinviCore.Helpers
{
    public interface IWebHelper
    {
        Stream GetResponseStream(string url);
    }
}