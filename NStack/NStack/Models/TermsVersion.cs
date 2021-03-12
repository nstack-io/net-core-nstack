using Newtonsoft.Json;
using System;

namespace NStack.SDK.Models
{
    public class TermsVersion
    {
        public int Id { get; set; }

        public string Version { get; set; }

        [JsonProperty("version_name")]
        public string VersionName { get; set; }

        [JsonProperty("published_at")]
        public DateTime PublishedAt { get; set; }

        public bool HasViewed { get; set; }
    }
}
