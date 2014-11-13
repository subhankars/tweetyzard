using System;
using TweetinviCore;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.QueryValidators;

namespace TweetinviControllers.User
{
    public class UserQueryValidator : IUserQueryValidator
    {
        public bool CanUserBeIdentified(IUserIdDTO userIdDTO)
        {
            return userIdDTO != null && (IsUserIdValid(userIdDTO.Id) || IsScreenNameValid(userIdDTO.ScreenName));
        }

        public bool IsScreenNameValid(string screenName)
        {
            return !String.IsNullOrEmpty(screenName);
        }

        public bool IsUserIdValid(long userId)
        {
            return userId != TweetinviConstants.DEFAULT_ID;
        }
    }
}