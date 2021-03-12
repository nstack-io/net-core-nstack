using Newtonsoft.Json;

namespace NStack.SDK.Models
{
    public class AppOpenMetaData
    {
        [JsonProperty("accept_Language")]
        public string AcceptLanguage { get; set; }
    }
}
