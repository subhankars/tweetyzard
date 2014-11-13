using System;
using TweetinviCore.Helpers;

namespace TweetinviLogic.Helpers
{
    public class TwitterStringFormatter : ITwitterStringFormatter
    {
        public string TwitterEncode(string source)
        {
            if (source == null)
            {
                return String.Empty;
            }

            return Uri.EscapeDataString(source);
        }
    }
}