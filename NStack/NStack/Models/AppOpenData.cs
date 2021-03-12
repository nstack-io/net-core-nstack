using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace NStack.SDK.Models
{
    public class AppOpenData
    {
        public int Count { get; set; }

        public IEnumerable<ResourceData> Localize { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public NStackPlatform Platform { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        public IEnumerable<AppOpenTerms> Terms { get; set; }
    }
}
