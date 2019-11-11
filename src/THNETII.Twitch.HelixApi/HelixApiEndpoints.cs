using System;
using System.Collections.Generic;
using System.Text;

namespace THNETII.Twitch.HelixApi
{
    public class HelixApiEndpoints
    {
        public const string GamesPath = @"games";
        public const string GamesTopPath = GamesPath + @"/top";

        public static HelixApiEndpoints Default { get; } =
            new HelixApiEndpoints(HelixApiDefaults.ApiBaseUri);

        public HelixApiEndpoints(Uri baseUri) : base()
        {
            BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
            Games = new Uri(BaseUri, GamesPath);
            GamesTop = new Uri(BaseUri, GamesTopPath);
        }

        public virtual Uri BaseUri { get; }
        public virtual Uri Games { get; }
        public virtual Uri GamesTop { get; }
    }
}
