using Newtonsoft.Json;
using TweetinviCore.Interfaces.Credentials.QueryDTO;

namespace TweetinviCredentials.QueryDTO
{
    public class IdsCursorQueryResultDTO : BaseCursorQueryDTO, IIdsCursorQueryResultDTO
    {
        [JsonProperty("ids")]
        public long[] Ids { get; set; }

        public override int GetNumberOfObjectRetrieved()
        {
            return Ids.Length;
        }
    }
}