using TweetinviCore.Enum;

namespace TweetinviCore.Interfaces.Parameters
{
    public interface IListUpdateParameters
    {
        string Name { get; set; }
        string Description { get; set; }
        PrivacyMode PrivacyMode { get; set; }
    }
}