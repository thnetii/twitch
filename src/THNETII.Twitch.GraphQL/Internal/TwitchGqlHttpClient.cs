using System.Net.Http;
using System.Net.Http.Headers;

using THNETII.GraphQL.Http;

namespace THNETII.Twitch.GraphQL.Internal
{
    internal class TwitchGqlHttpClient : GraphQLHttpClient
    {
        private readonly bool hasClientId;
        private readonly string clientId;
        private readonly AuthenticationHeaderValue authentication;

        public TwitchGqlHttpClient(HttpClient httpClient, string clientId,
            AuthenticationHeaderValue authentication)
            : base(httpClient, noDispose: true)
        {
            hasClientId = !string.IsNullOrWhiteSpace(clientId);
            this.clientId = clientId;
            this.authentication = authentication;
        }

        protected override void ConfigureMessage(HttpRequestMessage reqMsg)
        {
            if (hasClientId)
                reqMsg.Headers.Add(TwitchGqlDefaults.ClientIdHeaderName, clientId);
            if (!(authentication is null))
                reqMsg.Headers.Authorization = authentication;
        }
    }
}
