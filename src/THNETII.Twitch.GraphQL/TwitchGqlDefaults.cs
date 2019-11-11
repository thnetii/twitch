using System;

using THNETII.Twitch.HelixApi;

namespace THNETII.Twitch.GraphQL
{
    public static class TwitchGqlDefaults
    {
        public const string ClientIdHeaderName = HelixApiDefaults.ClientIdHeaderName;

        public static Uri Endpoint { get; } =
            new Uri(@"https://gql.twitch.tv/gql");
    }
}
