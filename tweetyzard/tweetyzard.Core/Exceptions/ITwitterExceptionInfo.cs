namespace TweetinviCore.Exceptions
{
    public interface ITwitterExceptionInfo
    {
        string Message { get; set; }
        int Code { get; set; }
    }
}