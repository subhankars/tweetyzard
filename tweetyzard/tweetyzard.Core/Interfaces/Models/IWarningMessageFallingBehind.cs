namespace TweetinviCore.Interfaces.Models
{
    public interface IWarningMessageFallingBehind : IWarningMessage
    {
        int PercentFull { get; }
    }
}