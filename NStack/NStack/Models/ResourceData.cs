namespace NStack.SDK.Models;

public class ResourceData
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    [JsonPropertyName("last_updated_at")]
    public DateTime LastUpdatedAt { get; set; }
    [JsonPropertyName("should_update")]
    public bool ShouldUpdate { get; set; }
    public Language Language { get; set; } = new Language();
}
