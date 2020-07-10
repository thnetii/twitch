using System.Net;

using Newtonsoft.Json;

using THNETII.Common;

namespace THNETII.Twitch.HelixApi.Models
{
    public class HelixErrorResponse
    {
        public const string ErrorPresencePropertyName = "error";

        private readonly DuplexConversionTuple<int, HttpStatusCode> status =
            new DuplexConversionTuple<int, HttpStatusCode>(
                rawConvert: v => (HttpStatusCode)v,
                rawReverseConvert: c => (int)c
                );

        [JsonProperty(ErrorPresencePropertyName)]
        public string ReasonPhrase { get; set; }

        [JsonProperty("status")]
        protected int StatusCodeValue
        {
            get => status.RawValue;
            set => status.RawValue = value;
        }

        [JsonIgnore]
        public HttpStatusCode StatusCode
        {
            get => status.ConvertedValue;
            set => status.ConvertedValue = value;
        }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
