using TweetinviCore.Interfaces.Models;

namespace TweetinviCore.Interfaces.Controllers
{
    public interface ITrendsController
    {
        IPlaceTrends GetPlaceTrendsAt(long woeid);
        IPlaceTrends GetPlaceTrendsAt(IWoeIdLocation woeIdLocation);
    }
}