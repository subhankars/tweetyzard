using TweetinviCore.Interfaces.Models;

namespace TweetinviControllers.Friendship
{
    public class FriendshipAuthorizations : IFriendshipAuthorizations
    {
        public bool RetweetsEnabled { get; set; }
        public bool DeviceNotificationEnabled { get; set; }
    }
}