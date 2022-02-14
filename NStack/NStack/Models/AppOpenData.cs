namespace NStack.SDK.Models;

public class AppOpenData
{
    public int Count { get; set; }

    public IEnumerable<ResourceData> Localize { get; set; } = Enumerable.Empty<ResourceData>();

    public NStackPlatform Platform { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    public IEnumerable<AppOpenTerms> Terms { get; set; } = Enumerable.Empty<AppOpenTerms>();
}
