using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace THNETII.Twitch.GraphQL
{
    public partial class TwitchGqlBaseClient : IDisposable
    {
        private readonly HttpClient httpClient;
        private readonly bool disposeClient;

        public TwitchGqlBaseClient(string clientId, HttpClient httpClient,
            bool noDispose = false) : base()
        {
            ClientId = clientId;
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            disposeClient = !noDispose;
        }

        public TwitchGqlBaseClient(string clientId)
            : this(clientId, new HttpClient()) { }

        public TwitchGqlBaseClient(string clientId,
            HttpMessageHandler messageHandler, bool noDispose = false)
            : this(clientId, new HttpClient(messageHandler, !noDispose)) { }

        protected static JsonSerializer DefaultSerializer { get; } =
            JsonSerializer.CreateDefault();

        protected virtual JsonSerializer JsonSerializer { get; } =
            DefaultSerializer;

        public virtual string ClientId { get; set; }

        public virtual Uri Endpoint { get; } = TwitchGqlDefaults.Endpoint;

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (disposeClient)
                httpClient.Dispose();
        }

        ~TwitchGqlBaseClient()
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
