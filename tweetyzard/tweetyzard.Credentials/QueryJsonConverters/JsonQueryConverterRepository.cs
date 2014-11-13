using System.Linq;
using Newtonsoft.Json;
using TweetinviCore.Interfaces.Credentials.QueryDTO;
using TweetinviCredentials.QueryDTO;
using TweetinviLogic.JsonConverters;

namespace TweetinviCredentials.QueryJsonConverters
{
    public static class JsonQueryConverterRepository
    {
        public static JsonConverter[] Converters { get; private set; }

        static JsonQueryConverterRepository()
        {
            Initialize();
        }

        private static void Initialize()
        {
            var converters = JsonPropertiesConverterRepository.Converters.ToList();
            converters.AddRange(new JsonConverter[]
            {
                new JsonInterfaceToObjectConverter<IIdsCursorQueryResultDTO, IdsCursorQueryResultDTO>(),
                new JsonInterfaceToObjectConverter<IUserCursorQueryResultDTO, UserCursorQueryResultDTO>(),
            });

            Converters = converters.ToArray();
        }
    }
}