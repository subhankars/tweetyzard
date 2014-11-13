using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TweetinviCore.Interfaces.Models;

namespace TweetinviLogic.Model
{
    /// <summary>
    /// Geographic information of a location
    /// </summary>
    public class Geo : IGeo
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        //[JsonProperty("coordinates")]
        [JsonIgnore]
        private List<ICoordinates>[] _storedCoordinates
        {
            set
            {
                if (value == null)
                {
                    Coordinates = null;
                }
                else if (!value.Any())
                {
                    Coordinates = new List<ICoordinates>();
                }
                else
                {
                    Coordinates = value[0];
                }
            }
        }

        [JsonIgnore]
        public List<ICoordinates> Coordinates { get; set; }
    }
}