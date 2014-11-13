namespace TweetinviCore.Interfaces.Credentials.QueryDTO
{
    public interface IBaseCursorQueryDTO
    {
        long PreviousCursor { get; set; }
        long NextCursor { get; set; }

        string PreviousCursorStr { get; set; }
        string NextCursorStr { get; set; }

        string RawJson { get; set; }

        int GetNumberOfObjectRetrieved();
    }
}