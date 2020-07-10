using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace THNETII.Twitch.HelixApi
{
    public partial class TwitchHelixBaseClient : IDisposable
    {
        private readonly HttpClient httpClient;
        private readonly bool disposeClient;

        public TwitchHelixBaseClient(string clientId, HttpClient httpClient,
            bool noDispose = false) : base()
        {
            ClientId = clientId;
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            disposeClient = !noDispose;
        }

        public TwitchHelixBaseClient(string clientId)
            : this(clientId, new HttpClient()) { }

        public TwitchHelixBaseClient(string clientId,
            HttpMessageHandler messageHandler, bool noDispose = false)
            : this(clientId, new HttpClient(messageHandler, !noDispose)) { }

        protected static JsonSerializer DefaultSerializer { get; } =
            JsonSerializer.CreateDefault();

        protected virtual JsonSerializer JsonSerializer { get; } =
            DefaultSerializer;

        public virtual string ClientId { get; set; }

        public virtual HelixApiEndpoints Endpoints { get; } =
            HelixApiEndpoints.Default;

        public static AuthenticationHeaderValue? GetAuthentication(string? token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;
            return new AuthenticationHeaderValue("Bearer", token);
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (disposeClient)
                httpClient.Dispose();
        }

        ~TwitchHelixBaseClient()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
