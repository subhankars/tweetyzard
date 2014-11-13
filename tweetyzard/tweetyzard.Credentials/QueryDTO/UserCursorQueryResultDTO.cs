using TweetinviCore.Interfaces.Credentials.QueryDTO;
using TweetinviCore.Interfaces.DTO;

namespace TweetinviCredentials.QueryDTO
{
    public class UserCursorQueryResultDTO : BaseCursorQueryDTO, IUserCursorQueryResultDTO
    {
        public IUserDTO[] Users { get; set; }

        public override int GetNumberOfObjectRetrieved()
        {
            return Users.Length;
        }
    }
}