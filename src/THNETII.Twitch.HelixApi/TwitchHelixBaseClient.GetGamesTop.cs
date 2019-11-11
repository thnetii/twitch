using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using THNETII.Twitch.HelixApi.Models;

namespace THNETII.Twitch.HelixApi
{
    partial class TwitchHelixBaseClient
    {
        public Task<HelixDataResponse<HelixGameRecord>> GetGamesTopAsync(
            in HelixPaginationRequestParameters pagination = default,
            AuthenticationHeaderValue? authentication = null,
            CancellationToken cancelToken = default)
        {
            return GetDataResponseAsync<HelixGameRecord>(Endpoints.GamesTop,
                pagination.GetQueryPairs(), authentication, cancelToken);
        }

        public IAsyncEnumerable<HelixGameRecord> GetGamesTopStream(
            AuthenticationHeaderValue? authentication = null,
            CancellationToken cancelToken = default)
        {
            return GetDataResponseStream<HelixGameRecord>(Endpoints.GamesTop,
                queryValues: null, authentication, cancelToken);
        }
    }
}
