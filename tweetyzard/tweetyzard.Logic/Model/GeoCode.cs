using TweetinviCore.Enum;
using TweetinviCore.Interfaces.Models;

namespace TweetinviLogic.Model
{
    public class GeoCode : IGeoCode
    {
        public ICoordinates Coordinates { get; set; }
        public double Radius { get; set; }
        public DistanceMeasure DistanceMeasure { get; set; }
    }
}