using Newtonsoft.Json;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces.Models;

namespace TweetinviLogic.Model
{
    public class TrendLocation : ITrendLocation
    {
        private class PlaceTypeDTO
        {
            [JsonProperty("code")]
            public int Code { get; set; }
        }

        [JsonProperty("woeid")]
        public long WoeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("parent_id")]
        public long ParentId { get; set; }

        public PlaceType PlaceType
        {
            get { return (PlaceType)_placeTypeDTO.Code; }
            set
            {
                if (_placeTypeDTO == null)
                {
                    _placeTypeDTO = new PlaceTypeDTO();
                }

                _placeTypeDTO.Code = (int) value;
            }
        }

        [JsonProperty("place_type")]
        private PlaceTypeDTO _placeTypeDTO { get; set; }
    }
}