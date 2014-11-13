using TweetinviCore.Enum;
using TweetinviCore.Interfaces.Parameters;

namespace TweetinviLogic.Model
{
    public class ListUpdateParameters : IListUpdateParameters
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PrivacyMode PrivacyMode { get; set; }
    }
}