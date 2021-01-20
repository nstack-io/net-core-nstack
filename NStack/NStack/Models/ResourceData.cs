using Newtonsoft.Json;
using System;

namespace NStack.SDK.Models
{
    public class ResourceData
    {
        public int Id { get; set; }
        public string Url { get; set; }
        [JsonProperty("last_updated_at")]
        public DateTime LastUpdatedAt { get; set; }
        [JsonProperty("should_update")]
        public bool ShouldUpdate { get; set; }
        public Language Language { get; set; }
    }
}
