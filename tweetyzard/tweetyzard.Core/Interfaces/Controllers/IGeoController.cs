using TweetinviCore.Interfaces.Models;

namespace TweetinviCore.Interfaces.Controllers
{
    public interface IGeoController
    {
        IPlace GetPlaceFromId(string placeId);
    }
}