using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using THNETII.GraphQL.Http;
using THNETII.Twitch.GraphQL.Internal;

namespace THNETII.Twitch.GraphQL
{
    partial class TwitchGqlBaseClient
    {
        public async Task<GraphQLResponse<TData>> RequestAsync<TData>(
            GraphQLRequest request,
            AuthenticationHeaderValue authentication,
            CancellationToken cancelToken = default)
        {
            using var graphqlClient = new TwitchGqlHttpClient(httpClient,
                ClientId, authentication);
            return await graphqlClient.SendAsync<TData>(Endpoint, request,
                cancelToken).ConfigureAwait(false);
        }
    }
}
