using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NStack.SDK.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Locale { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public LanguageDirection Direction { get; set; }
        [JsonProperty("is_default")]
        public bool IsDefault { get; set; }
        [JsonProperty("is_best_fit")]
        public bool IsBestFit { get; set; }

        public override string ToString() => $"{Name} ({Locale})";
    }
}
