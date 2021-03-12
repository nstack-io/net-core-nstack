using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NStack.SDK.Models
{
    public class AppOpenData
    {
        public int Count { get; set; }

        public IEnumerable<ResourceData> Localize { get; set; }

        public NStackPlatform Platform
        {
            get
            {
                return PlatformString switch
                {
                    "web" => NStackPlatform.Web,
                    "backend" => NStackPlatform.Backend,
                    "mobile" => NStackPlatform.Mobile,
                    _ => throw new Exception($"Unknown platform {PlatformString}")
                };
            }
        }

        [JsonProperty("platform")]
        public string PlatformString { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        public IEnumerable<AppOpenTerms> Terms { get; set; }
    }
}
