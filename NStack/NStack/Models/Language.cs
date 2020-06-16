using System.Text.Json.Serialization;

namespace NStack.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Locale { get; set; }
        public string Direction { get; set; }
        [JsonPropertyName("is_default")]
        public bool IsDefault { get; set; }
        [JsonPropertyName("is_best_fit")]
        public bool IsBestFit { get; set; }
    }
}
