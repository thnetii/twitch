using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace THNETII.Twitch.HelixApi.Models
{
    public class HelixGameRecord
    {
        /// <summary>Game ID.</summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        /// <summary>Game name.</summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        /// <summary>Template URL for the game’s box art.</summary>
        [JsonProperty(PropertyName = "box_art_url")]
        [SuppressMessage("Design", "CA1056: Uri properties should not be strings")]
        public string BoxArtUrl { get; protected set; }
    }
}
