using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.Models;

namespace TweetinviControllers.Geo
{
    public class GeoController : IGeoController
    {
        private readonly IGeoQueryExecutor _geoQueryExecutor;

        public GeoController(IGeoQueryExecutor geoQueryExecutor)
        {
            _geoQueryExecutor = geoQueryExecutor;
        }

        public IPlace GetPlaceFromId(string placeId)
        {
            return _geoQueryExecutor.GetPlaceFromId(placeId);
        }
    }
}