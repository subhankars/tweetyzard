namespace TweetinviCore.Interfaces.Credentials.QueryDTO
{
    public interface IIdsCursorQueryResultDTO : IBaseCursorQueryDTO
    {
        long[] Ids { get; set; }
    }
}