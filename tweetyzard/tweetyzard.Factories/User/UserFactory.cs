using System.Collections.Generic;
using System.Linq;
using TweetinviCore.Helpers;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.oAuth;

namespace TweetinviFactories.User
{
    public class UserFactory : IUserFactory
    {
        private readonly IUserFactoryQueryExecutor _userFactoryQueryExecutor;
        private readonly IUnityFactory<ILoggedUser> _loggedUserUnityFactory;
        private readonly IUnityFactory<IUser> _userUnityFactory;
        private readonly IUnityFactory<IUserIdDTO> _userIdDTOUnityFactory;
        private readonly IJsonObjectConverter _jsonObjectConverter;
        private readonly ICredentialsAccessor _credentialsAccessor;

        public UserFactory(
            IUserFactoryQueryExecutor userFactoryQueryExecutor,
            IUnityFactory<ILoggedUser> loggedUserUnityFactory,
            IUnityFactory<IUser> userUnityFactory,
            IUnityFactory<IUserIdDTO> userIdDTOUnityFactory,
            IJsonObjectConverter jsonObjectConverter,
            ICredentialsAccessor credentialsAccessor)
        {
            _userFactoryQueryExecutor = userFactoryQueryExecutor;
            _loggedUserUnityFactory = loggedUserUnityFactory;
            _userUnityFactory = userUnityFactory;
            _userIdDTOUnityFactory = userIdDTOUnityFactory;
            _jsonObjectConverter = jsonObjectConverter;
            _credentialsAccessor = credentialsAccessor;
        }

        public ILoggedUser GetLoggedUser()
        {
            var userDTO = _userFactoryQueryExecutor.GetLoggedUser();
            return GenerateLoggedUserFromDTO(userDTO);
        }

        public ILoggedUser GetLoggedUser(IOAuthCredentials credentials)
        {
            var userDTO = _credentialsAccessor.ExecuteOperationWithCredentials(credentials, () =>
            {
                return _userFactoryQueryExecutor.GetLoggedUser();
            });

            return GenerateLoggedUserFromDTO(userDTO);
        }

        public IUser GetUserFromId(long userId)
        {
            var userDTO = _userFactoryQueryExecutor.GetUserDTOFromId(userId);
            return GenerateUserFromDTO(userDTO);
        }

        public IUser GetUserFromScreenName(string userName)
        {
            var userDTO = _userFactoryQueryExecutor.GetUserDTOFromScreenName(userName);
            return GenerateUserFromDTO(userDTO);
        }

        // Generate User from Json
        public IUser GenerateUserFromJson(string jsonUser)
        {
            var userDTO = _jsonObjectConverter.DeserializeObject<IUserDTO>(jsonUser);
            return GenerateUserFromDTO(userDTO);
        }

        public IEnumerable<IUser> GetUsersFromIds(IEnumerable<long> userIds)
        {
            var usersDTO = _userFactoryQueryExecutor.GetUsersDTOFromIds(userIds);
            return GenerateUsersFromDTO(usersDTO);
        }

        public IEnumerable<IUser> GetUsersFromNames(IEnumerable<string> userNames)
        {
            var usersDTO = _userFactoryQueryExecutor.GetUsersDTOFromScreenNames(userNames);
            return GenerateUsersFromDTO(usersDTO);
        }

        // Generate DTO from id
        public IUserIdDTO GenerateUserIdDTOFromId(long userId)
        {
            var userIdDTO = _userIdDTOUnityFactory.Create();
            userIdDTO.Id = userId;

            return userIdDTO;
        }

        public IUserIdDTO GenerateUserIdDTOFromScreenName(string userScreenName)
        {
            var userIdDTO = _userIdDTOUnityFactory.Create();
            userIdDTO.ScreenName = userScreenName;

            return userIdDTO;
        }

        // Generate from DTO
        public ILoggedUser GenerateLoggedUserFromDTO(IUserDTO userDTO)
        {
            if (userDTO == null)
            {
                return null;
            }

            var userDTOParameterOverride = _loggedUserUnityFactory.GenerateParameterOverrideWrapper("userDTO", userDTO);
            var user = _loggedUserUnityFactory.Create(userDTOParameterOverride);

            return user;
        }

        public IUser GenerateUserFromDTO(IUserDTO userDTO)
        {
            if (userDTO == null)
            {
                return null;
            }

            var parameterOverride = _userUnityFactory.GenerateParameterOverrideWrapper("userDTO", userDTO);
            var user = _userUnityFactory.Create(parameterOverride);

            return user;
        }

        public IEnumerable<IUser> GenerateUsersFromDTO(IEnumerable<IUserDTO> usersDTO)
        {
            if (usersDTO == null)
            {
                return null;
            }

            return usersDTO.Select(GenerateUserFromDTO).ToList();
        }
    }
}