namespace NStack.SDK.Models;

public class TermsVersion
{
    public int Id { get; set; }

    public string Version { get; set; } = string.Empty;

    [JsonPropertyName("version_name")]
    public string VersionName { get; set; } = string.Empty;

    [JsonPropertyName("published_at")]
    public DateTime PublishedAt { get; set; }

    public bool HasViewed { get; set; }
}
