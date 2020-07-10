using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace THNETII.Twitch.HelixApi.Models
{
    [SuppressMessage("Performance", "CA1815: Override equals and operator equals on value types")]
    public struct HelixPaginationRequestParameters
    {
        /// <summary>
        /// Cursor for forward pagination: tells the server where to start
        /// fetching the next set of results, in a multi-page response.
        /// <para>
        /// The cursor value specified here is from the
        /// <see cref="HelixPaginationInfo.Cursor"/> property of the
        /// <see cref="HelixDataResponse{T}.Pagination"/> property in the
        /// response of a prior query.
        /// </para>
        /// </summary>
        public string After { get; set; }
        /// <summary>
        /// Cursor for backward pagination: tells the server where to start
        /// fetching the next set of results, in a multi-page response.
        /// <para>
        /// The cursor value specified here is from the
        /// <see cref="HelixPaginationInfo.Cursor"/> property of the
        /// <see cref="HelixDataResponse{T}.Pagination"/> property in the
        /// response of a prior query.
        /// </para>
        /// </summary>
        public string Before { get; set; }
        /// <summary>
        /// Maximum number of objects to return.
        /// <para>If omitted, the server will decide a default value.</para>
        /// </summary>
        public int? First { get; set; }

        public IEnumerable<KeyValuePair<string, string>> GetQueryPairs()
        {
            var (after, before, first) = (After, Before, First);
            if (!string.IsNullOrWhiteSpace(after))
                yield return new KeyValuePair<string, string>(nameof(after), after);
            if (!string.IsNullOrWhiteSpace(before))
                yield return new KeyValuePair<string, string>(nameof(before), before);
            if (first.HasValue)
                yield return new KeyValuePair<string, string>(nameof(first),
                    first.Value.ToString(CultureInfo.InvariantCulture));
        }
    }
}
