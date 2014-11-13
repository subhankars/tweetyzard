using System.Collections.Generic;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.oAuth;

namespace TweetinviCore.Interfaces.Factories
{
    public interface IUserFactory
    {
        ILoggedUser GetLoggedUser();
        ILoggedUser GetLoggedUser(IOAuthCredentials credentials);

        IUser GetUserFromId(long userId);
        IUser GetUserFromScreenName(string userName);

        // Generate User from Json
        IUser GenerateUserFromJson(string jsonUser);

        // Get Multiple users
        IEnumerable<IUser> GetUsersFromIds(IEnumerable<long> userIds);
        IEnumerable<IUser> GetUsersFromNames(IEnumerable<string> userNames);

        // Generate user from DTO
        IUser GenerateUserFromDTO(IUserDTO userDTO);
        ILoggedUser GenerateLoggedUserFromDTO(IUserDTO userDTO);
        IEnumerable<IUser> GenerateUsersFromDTO(IEnumerable<IUserDTO> usersDTO);

        // Generate userIdDTO from
        IUserIdDTO GenerateUserIdDTOFromId(long userId);
        IUserIdDTO GenerateUserIdDTOFromScreenName(string userScreenName);
    }
}