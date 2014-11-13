using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using TweetinviCore.Interfaces.Models;

namespace TweetinviLogic.Model
{
    /// <summary>
    /// Coordinates of a geographical location
    /// </summary>
    public class Coordinates : ICoordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Coordinates() { }

        /// <summary>
        /// Create coordinates with its longitude and latitude
        /// </summary>
        [InjectionConstructor]
        public Coordinates(double longitude, double latitude)
        {
            Longitude = longitude; 
            Latitude = latitude;
        }

        [JsonProperty("coordinates")]
        private List<double> _coordinatesSetter
        {
            set
            {
                if (value != null)
                {
                    Longitude = value[0];
                    Latitude = value[1];
                }
            }
        }
    }
}