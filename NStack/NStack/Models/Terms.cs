namespace NStack.SDK.Models;

public class Terms
{
    public int Id { get; set; }
    public string Version { get; set; } = string.Empty;
    [JsonPropertyName("version_name")]
    public string VersionName { get; set; } = string.Empty;
    [JsonPropertyName("published_at")]
    public string PublishedAtString { get; set; } = string.Empty;
    public DateTime PublishedAt
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(PublishedAtString) &&
                DateTime.TryParseExact(PublishedAtString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime publishedAt))
            {
                return publishedAt;
            }

            return default(DateTime);
        }
    }
    [JsonPropertyName("has_viewed")]
    public bool HasViewed { get; set; }
}
