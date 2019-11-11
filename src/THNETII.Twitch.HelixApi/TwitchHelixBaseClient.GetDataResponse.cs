using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using THNETII.Networking.Http;
using THNETII.Twitch.HelixApi.Models;

namespace THNETII.Twitch.HelixApi
{
    partial class TwitchHelixBaseClient
    {
        private async Task<JObject> GetDataResponseJObjectAsync(Uri requestUri,
            AuthenticationHeaderValue? authentication = null,
            CancellationToken cancelToken = default)
        {
            using (var reqMsg = CreateRequest(requestUri, ClientId, authentication))
            using (var respMsg = await httpClient.SendAsync(reqMsg, cancelToken)
                .ConfigureAwait(continueOnCapturedContext: false))
            {
                if (!respMsg.Content.IsJson() && !respMsg.Content.IsText())
                    throw new HttpRequestException($"Invalid Content-Type in response body: {respMsg.Content.Headers.ContentType}");
                using var respTxtReader = await respMsg.Content
                    .ReadAsStreamReaderAsync().ConfigureAwait(false);
                using var respJsonReader = new JsonTextReader(respTxtReader);
                return await JObject.LoadAsync(respJsonReader, cancelToken)
                    .ConfigureAwait(continueOnCapturedContext: false);
            }

            static HttpRequestMessage CreateRequest(Uri requestUri,
                string? clientId, AuthenticationHeaderValue? authentication)
            {
                var msg = new HttpRequestMessage(HttpMethod.Get, requestUri);
                if (!(authentication is null))
                    msg.Headers.Authorization = authentication;
                if (!(clientId is null))
                    msg.Headers.Add(HelixApiDefaults.ClientIdHeaderName, clientId);
                return msg;
            }
        }

        protected async Task<HelixDataResponse<T>> GetDataResponseAsync<T>(
            Uri methodEndpoint,
            IEnumerable<KeyValuePair<string, string>>? queryValues,
            AuthenticationHeaderValue? authentication = null,
            CancellationToken cancelToken = default
            )
        {
            var requestUri = (HttpUrlHelper.ToQueryString(queryValues!)) switch
            {
                "?" => methodEndpoint,
                var queryString => new Uri(methodEndpoint, queryString),
            };
            var respJsonObject = await GetDataResponseJObjectAsync(requestUri,
                authentication, cancelToken).ConfigureAwait(false);
            if (respJsonObject.ContainsKey(HelixErrorResponse.ErrorPresencePropertyName))
            {
                var errorResponse = respJsonObject.ToObject<HelixErrorResponse>(JsonSerializer);
                throw new HelixApiException(errorResponse);
            }
            return respJsonObject.ToObject<HelixDataResponse<T>>(JsonSerializer)!;
        }

        protected async IAsyncEnumerable<T> GetDataResponseStream<T>(
            Uri methodEndpoint,
            IEnumerable<KeyValuePair<string, string>>? queryValues,
            AuthenticationHeaderValue? authentication = null,
            [EnumeratorCancellation] CancellationToken cancelToken = default
            )
        {
            HelixDataResponse<T> dataResponse;
            var origQueryValues = queryValues ?? Enumerable.Empty<KeyValuePair<string, string>>();
            var paginationParams = default(HelixPaginationRequestParameters);
            do
            {
                dataResponse = await GetDataResponseAsync<T>(
                    methodEndpoint,
                    origQueryValues.Concat(paginationParams.GetQueryPairs()),
                    authentication, cancelToken)
                    .ConfigureAwait(false);
                foreach (var item in dataResponse.Data)
                    yield return item;
                paginationParams.After = dataResponse.Pagination?.Cursor;
            } while (string.IsNullOrEmpty(dataResponse.Pagination?.Cursor));
        }
    }
}
