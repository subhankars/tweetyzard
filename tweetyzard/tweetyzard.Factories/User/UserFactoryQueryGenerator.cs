using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TweetinviCore;
using TweetinviCore.Interfaces.DTO;

namespace TweetinviFactories.User
{
    public interface IUserFactoryQueryGenerator
    {
        string GenerateListOfIdsParameter(IEnumerable<long> ids);
        string GenerateListOfUserDTOParameter(IEnumerable<IUserIdDTO> usersDTO);
        string GenerateListOfScreenNameParameter(IEnumerable<string> userNames);
    }

    public class UserFactoryQueryGenerator : IUserFactoryQueryGenerator
    {
        public string GenerateListOfUserDTOParameter(IEnumerable<IUserIdDTO> usersDTO)
        {
            var userDTOList = usersDTO.ToList();
            if (usersDTO.Any(user => user.Id == TweetinviConstants.DEFAULT_ID && String.IsNullOrEmpty(user.ScreenName)))
            {
                throw new ArgumentException("Cannot generate a list with any empty screename and id");
            }

            const string initialUserId = "user_id=";
            const string initialScreenName = "&screen_name=";

            StringBuilder idsBuilder = new StringBuilder(initialUserId);
            StringBuilder screeNameBuilder = new StringBuilder(initialScreenName);

            for (int i = 0; i < userDTOList.Count - 1; ++i)
            {
                var userDTO = userDTOList[0];

                if (userDTO.Id != TweetinviConstants.DEFAULT_ID)
                {
                    idsBuilder.Append(String.Format("{0}%2C", userDTO.Id));
                }
                else
                {
                    screeNameBuilder.Append(String.Format("{0}%2C", userDTO.ScreenName));
                }
            }

            // Last element does not have a comma
            if (userDTOList[userDTOList.Count - 1].Id != -1)
            {
                idsBuilder.Append(userDTOList[userDTOList.Count - 1].Id);
            }
            else
            {
                screeNameBuilder.Append(userDTOList[userDTOList.Count - 1].ScreenName);
            }

            // Only ids
            if (idsBuilder.ToString() == initialUserId)
            {
                return screeNameBuilder.ToString();
            }

            // Only screenames
            if (screeNameBuilder.ToString() == initialScreenName)
            {
                return idsBuilder.ToString();
            }

            // Both
            return idsBuilder.Append(screeNameBuilder).ToString();
        }

        public string GenerateListOfIdsParameter(IEnumerable<long> ids)
        {
            var idsList = ids.ToList();
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < idsList.Count - 1; ++i)
            {
                builder.Append(String.Format("{0}%2C", ids.ElementAt(i)));
            }

            builder.Append(idsList.ElementAt(idsList.Count - 1));

            return builder.ToString();
        }

        public string GenerateListOfScreenNameParameter(IEnumerable<string> screenNames)
        {
            var screenNamesList = screenNames.ToList();
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < screenNamesList.Count - 1; ++i)
            {
                builder.Append(String.Format("{0}%2C", screenNamesList.ElementAt(i)));
            }

            builder.Append(screenNamesList.ElementAt(screenNamesList.Count - 1));

            return builder.ToString();
        }
    }
}