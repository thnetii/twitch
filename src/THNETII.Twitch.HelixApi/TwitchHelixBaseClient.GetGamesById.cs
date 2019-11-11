using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using THNETII.Twitch.HelixApi.Models;

namespace THNETII.Twitch.HelixApi
{
    partial class TwitchHelixBaseClient
    {
        public async Task<List<HelixGameRecord>> GetGamesByIdsAsync(
            IEnumerable<string> ids,
            AuthenticationHeaderValue? authentication = null,
            CancellationToken cancelToken = default)
        {
            var response = await GetDataResponseAsync<HelixGameRecord>(
                Endpoints.Games,
                ids.Select(id => new KeyValuePair<string, string>(nameof(id), id)),
                authentication, cancelToken).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<List<HelixGameRecord>> GetGameByIdAsync(string id,
            AuthenticationHeaderValue? authentication = null,
            CancellationToken cancelToken = default)
        {
            var response = await GetDataResponseAsync<HelixGameRecord>(
                Endpoints.Games,
                new[] { new KeyValuePair<string, string>(nameof(id), id) },
                authentication, cancelToken).ConfigureAwait(false);
            return response.Data;
        }
    }
}
