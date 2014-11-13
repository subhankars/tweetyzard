using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TweetinviCore.Enum;
using TweetinviCore.Helpers;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Models;
using TweetinviLogic.DTO;
using TweetinviLogic.Model;
using TweetinviLogic.TwitterEntities;

namespace TweetinviLogic.JsonConverters
{
    /// <summary>
    /// Repository of multiple converters required to initialize a DTO property.
    /// </summary>
    public class JsonPropertyConverterRepository : JsonConverter, IJsonPropertyConverterRepository
    {
        public static readonly Dictionary<Type, JsonConverter> JsonConverters;

        static JsonPropertyConverterRepository()
        {
            JsonConverters = new Dictionary<Type, JsonConverter>();

            IntializeClassicalTypesConvertes();
            InitializeTweetinviObjectConverters();
            InitializeTweetinviInterfacesConverters();
            InitializeEntitiesConverters();
        }

        private static void IntializeClassicalTypesConvertes()
        {
            var nullableBoolConverter = new JsonTwitterNullableConverter<bool>();
            var nullableLongConverter = new JsonTwitterNullableConverter<long>();
            var nullableIntegerConverter = new JsonTwitterNullableConverter<int>();
            var nullableDoubleConverter = new JsonTwitterNullableConverter<double>();
            var dateTimeConverter = new JsonTwitterDateTimeConverter();

            JsonConverters.Add(typeof(bool), nullableBoolConverter);
            JsonConverters.Add(typeof(long), nullableLongConverter);
            JsonConverters.Add(typeof(int), nullableIntegerConverter);
            JsonConverters.Add(typeof(double), nullableDoubleConverter);
            JsonConverters.Add(typeof(DateTime), dateTimeConverter);
        }

        private static void InitializeTweetinviObjectConverters()
        {
            var privacyModeConverter = new JsonPrivacyModeConverter();
            var coordinatesConverter = new JsonInterfaceToObjectConverter<ICoordinates, Coordinates>();
            var languageConverter = new JsonLanguageConverter();

            JsonConverters.Add(typeof(PrivacyMode), privacyModeConverter);
            JsonConverters.Add(typeof(ICoordinates), coordinatesConverter);
            JsonConverters.Add(typeof(Language), languageConverter);
        }

        private static void InitializeTweetinviInterfacesConverters()
        {
            var userConverter = new JsonInterfaceToObjectConverter<IUserDTO, UserDTO>();
            var tweetConverter = new JsonInterfaceToObjectConverter<ITweetDTO, TweetDTO>();
            var tweetListConverter = new JsonInterfaceToObjectConverter<ITweetListDTO, TweetListDTO>();
            var messageConverter = new JsonInterfaceToObjectConverter<IMessageDTO, MessageDTO>();
            var oembedTweetConverter = new JsonInterfaceToObjectConverter<IOEmbedTweetDTO, OEmbedTweetDTO>();
            var relationshipConverter = new JsonInterfaceToObjectConverter<IRelationshipDTO, RelationshipDTO>();
            var relationshipStateConverter = new JsonInterfaceToObjectConverter<IRelationshipStateDTO, RelationshipStateDTO>();
            var accountSettingsConverter = new JsonInterfaceToObjectConverter<IAccountSettingsDTO, AccountSettingsDTO>();
            var geoConverter = new JsonInterfaceToObjectConverter<IGeo, Geo>();
            var timezoneConverter = new JsonInterfaceToObjectConverter<ITimeZone, Model.TimeZone>();
            var trendLocationConverter = new JsonInterfaceToObjectConverter<ITrendLocation, TrendLocation>();
            var placeConverter = new JsonInterfaceToObjectConverter<IPlace, Place>();
            var trendConverter = new JsonInterfaceToObjectConverter<ITrend, Trend>();
            var placeTrendsConverter = new JsonInterfaceToObjectConverter<IPlaceTrends, PlaceTrends>();
            var woeIdLocationConverter = new JsonInterfaceToObjectConverter<IWoeIdLocation, WoeIdLocation>();
            var tokenRateLimitConverter = new JsonInterfaceToObjectConverter<ITokenRateLimit, TokenRateLimit>();
            var tokenRateLimitsConverter = new JsonInterfaceToObjectConverter<ITokenRateLimits, TokenRateLimits>();
            var savedSearchConverter = new JsonInterfaceToObjectConverter<ISavedSearchDTO, SavedSearchDTO>();

            JsonConverters.Add(typeof(IUserDTO), userConverter);
            JsonConverters.Add(typeof(ITweetDTO), tweetConverter);
            JsonConverters.Add(typeof(ITweetListDTO), tweetListConverter);
            JsonConverters.Add(typeof(IMessageDTO), messageConverter);
            JsonConverters.Add(typeof(IOEmbedTweetDTO), oembedTweetConverter);
            JsonConverters.Add(typeof(IRelationshipDTO), relationshipConverter);
            JsonConverters.Add(typeof(IRelationshipStateDTO), relationshipStateConverter);
            JsonConverters.Add(typeof(IAccountSettingsDTO), accountSettingsConverter);
            
            JsonConverters.Add(typeof(IGeo), geoConverter);
            JsonConverters.Add(typeof(ITimeZone), timezoneConverter);
            JsonConverters.Add(typeof(ITrendLocation), trendLocationConverter);
            JsonConverters.Add(typeof(IPlace), placeConverter);
            JsonConverters.Add(typeof(IWoeIdLocation), woeIdLocationConverter);

            JsonConverters.Add(typeof(ITrend), trendConverter);
            JsonConverters.Add(typeof(IPlaceTrends), placeTrendsConverter);

            JsonConverters.Add(typeof(ITokenRateLimit), tokenRateLimitConverter);
            JsonConverters.Add(typeof(ITokenRateLimits), tokenRateLimitsConverter);
            JsonConverters.Add(typeof(ISavedSearch), savedSearchConverter);
        }

        private static void InitializeEntitiesConverters()
        {
            var hashtagEntityConverter = new JsonInterfaceToObjectConverter<IHashtagEntity, HashtagEntity>();
            var urlEntityConverter = new JsonInterfaceToObjectConverter<IHashtagEntity, HashtagEntity>();
            var mediaEntityConverter = new JsonInterfaceToObjectConverter<IHashtagEntity, HashtagEntity>();
            var mediaEntitySizeConverter = new JsonInterfaceToObjectConverter<IMediaEntitySize, MediaEntitySize>();
            var entitiesConverter = new JsonInterfaceToObjectConverter<ITweetEntities, TweetEntities>();

            JsonConverters.Add(typeof(IHashtagEntity), hashtagEntityConverter);
            JsonConverters.Add(typeof(IUrlEntity), urlEntityConverter);
            JsonConverters.Add(typeof(IMediaEntity), mediaEntityConverter);
            JsonConverters.Add(typeof(IMediaEntitySize), mediaEntitySizeConverter);
            JsonConverters.Add(typeof(ITweetEntities), entitiesConverter);
        }

        public JsonConverter GetObjectConverter(object objectToConvert)
        {
            return GetTypeConverter(objectToConvert.GetType());
        }

        public JsonConverter GetTypeConverter(Type objectType)
        {
            return JsonConverters[objectType];
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return GetTypeConverter(objectType).ReadJson(reader, objectType, existingValue, serializer);
        }

        public override bool CanConvert(Type objectType)
        {
            return JsonConverters.ContainsKey(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}