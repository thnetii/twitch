using System;

namespace THNETII.Twitch.HelixApi
{
    public static class HelixApiDefaults
    {
        public const string ClientIdHeaderName = "Client-ID";
        public static Uri ApiBaseUri { get; } = new Uri(@"https://api.twitch.tv/helix/");
    }
}
