namespace TweetinviCore.Interfaces.DTO
{
    public interface IUserIdDTO
    {
        long Id { get; set; }
        string IdStr { get; set; }
        string ScreenName { get; set; }
    }
}