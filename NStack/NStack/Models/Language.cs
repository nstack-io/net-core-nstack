namespace NStack.SDK.Models;

public class Language
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Locale { get; set; } = string.Empty;
    public LanguageDirection Direction { get; set; }
    [JsonPropertyName("is_default")]
    public bool IsDefault { get; set; }
    [JsonPropertyName("is_best_fit")]
    public bool IsBestFit { get; set; }

    public override string ToString() => $"{Name} ({Locale})";
}
