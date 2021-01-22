using Newtonsoft.Json;

namespace NStack.SDK.Models
{
    public class TermsReadConfirmation
    {
        [JsonProperty("term_version_id")]
        public int TermVersionId { get; set; }

        [JsonProperty("guid")]
        public string UserId { get; set; }

        public string Identifier { get; set; }

        public string Locale { get; set; }
    }
}
