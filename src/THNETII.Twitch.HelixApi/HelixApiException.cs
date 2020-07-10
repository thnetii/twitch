using System;
using System.Runtime.Serialization;

using THNETII.Twitch.HelixApi.Models;

namespace THNETII.Twitch.HelixApi
{
    [Serializable]
    public class HelixApiException : Exception
    {
        public HelixErrorResponse? ErrorResponse { get; }

        /// <inheritdoc/>
        public HelixApiException() : base() { }

        /// <inheritdoc/>
        public HelixApiException(string message) : base(message) { }

        /// <inheritdoc/>
        public HelixApiException(string message, Exception innerException)
            : base(message, innerException) { }

        public HelixApiException(HelixErrorResponse? errorResponse)
            : this(errorResponse?.Message!)
        {
            ErrorResponse = errorResponse;
        }

        /// <inheritdoc/>
        protected HelixApiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (ErrorResponse is HelixErrorResponse errorResponse)
                info.AddValue(nameof(ErrorResponse), errorResponse);
        }
    }
}
