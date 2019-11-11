using Newtonsoft.Json;

namespace THNETII.Twitch.HelixApi.Models
{
    public class HelixPaginationInfo
    {
        /// <summary>
        /// A cursor value, to be used in a subsequent request to specify the starting point of the next set of results.
        /// </summary>
        [JsonProperty(PropertyName = "cursor")]
        public string Cursor { get; protected set; }
    }
}
