using Newtonsoft.Json;
using System;
using System.Globalization;

namespace NStack.SDK.Models
{
    public class Terms
    {
        public int Id { get; set; }
        public string Version { get; set; }
        [JsonProperty("version_name")]
        public string VersionName { get; set; }
        [JsonProperty("published_at")]
        public string PublishedAtString { get; set; }
        public DateTime PublishedAt
        {
            get
            {
                if(!string.IsNullOrWhiteSpace(PublishedAtString) &&
                    DateTime.TryParseExact(PublishedAtString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime publishedAt))
                {
                    return publishedAt;
                }

                return default(DateTime);
            }
        }
        [JsonProperty("has_viewed")]
        public bool HasViewed { get; set; }
        public TermsContent Content { get; set; }
    }
}
