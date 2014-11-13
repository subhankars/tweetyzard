using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Interfaces.Models;
using TweetinviCore.Wrappers;

namespace TweetinviFactories.Geo
{
    public class GeoFactory : IGeoFactory
    {
        private readonly IUnityFactory<ICoordinates> _coordinatesUnityFactory;
        private readonly IUnityFactory<ILocation> _locationUnityFactory;

        public GeoFactory(
            IUnityFactory<ICoordinates> coordinatesUnityFactory,
            IUnityFactory<ILocation> locationUnityFactory)
        {
            _coordinatesUnityFactory = coordinatesUnityFactory;
            _locationUnityFactory = locationUnityFactory;
        }

        public ICoordinates GenerateCoordinates(double longitude, double latitude)
        {
            var longitudeParameter = _locationUnityFactory.GenerateParameterOverrideWrapper("longitude", longitude);
            var latitudeParameter = _locationUnityFactory.GenerateParameterOverrideWrapper("latitude", latitude);

            var coordinates = _coordinatesUnityFactory.Create(new IResolverOverrideWrapper[]
            {
                longitudeParameter,
                latitudeParameter
            });

            return coordinates;
        }

        public ILocation GenerateLocation(ICoordinates coordinates1, ICoordinates coordinates2)
        {
            var coordinates1Parameter = _locationUnityFactory.GenerateParameterOverrideWrapper("coordinates1", coordinates1);
            var coordinates2Parameter = _locationUnityFactory.GenerateParameterOverrideWrapper("coordinates2", coordinates2);

            var location = _locationUnityFactory.Create(new IResolverOverrideWrapper[]
            {
                coordinates1Parameter,
                coordinates2Parameter
            });

            return location;
        }

        public ILocation GenerateLocation(double longitude1, double latitude1, double longitude2, double latitude2)
        {
            var coordinates1 = GenerateCoordinates(longitude1, latitude1);
            var coordinates2 = GenerateCoordinates(longitude2, latitude2);

            return GenerateLocation(coordinates1, coordinates2);
        }
    }
}