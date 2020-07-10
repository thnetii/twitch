using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace THNETII.Twitch.HelixApi.Models
{
    public class HelixDataResponse<T>
    {
        [JsonProperty("data")]
        public List<T> Data { get; } = new List<T>();

        [JsonProperty("pagination")]
        public HelixPaginationInfo Pagination { get; protected set; }

        [JsonExtensionData]
        internal IDictionary<string, JToken> AdditionalProperties { get; set; }
    }
}
