using System;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.QueryGenerators;
using TweetinviCore.Interfaces.QueryValidators;

namespace TweetinviControllers.User
{
    public class UserQueryParameterGenerator : IUserQueryParameterGenerator
    {
        private readonly IUserQueryValidator _userQueryValidator;

        public UserQueryParameterGenerator(IUserQueryValidator userQueryValidator)
        {
            _userQueryValidator = userQueryValidator;
        }

        public string GenerateUserIdParameter(long userId, string parameterName = "user_id")
        {
            if (!_userQueryValidator.IsUserIdValid(userId))
            {
                return null;
            }

            return String.Format("{0}={1}", parameterName, userId);
        }

        public string GenerateScreenNameParameter(string screenName, string parameterName = "screen_name")
        {
            if (!_userQueryValidator.IsScreenNameValid(screenName))
            {
                return null;
            }

            return String.Format("{0}={1}", parameterName, screenName);
        }

        public string GenerateIdOrScreenNameParameter(
            IUserIdDTO userDTO,
            string idParameterName = "user_id",
            string screenNameParameterName = "screen_name")
        {
            if (userDTO == null)
            {
                throw new ArgumentException("Cannot extract id or name parameter from a null userDTO.");
            }

            if (!_userQueryValidator.CanUserBeIdentified(userDTO))
            {
                throw new ArgumentException("Cannot extract either id or name parameter from the given userDTO.");
            }

            if (_userQueryValidator.IsUserIdValid(userDTO.Id))
            {
                return GenerateUserIdParameter(userDTO.Id, idParameterName);
            }

            return GenerateScreenNameParameter(userDTO.ScreenName, screenNameParameterName);
        }
    }
}