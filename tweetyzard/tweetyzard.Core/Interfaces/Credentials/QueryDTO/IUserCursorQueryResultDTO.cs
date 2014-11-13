using TweetinviCore.Interfaces.DTO;

namespace TweetinviCore.Interfaces.Credentials.QueryDTO
{
    public interface IUserCursorQueryResultDTO : IBaseCursorQueryDTO
    {
        IUserDTO[] Users { get; set; }
    }
}