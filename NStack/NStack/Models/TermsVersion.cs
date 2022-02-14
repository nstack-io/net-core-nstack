namespace NStack.SDK.Models;

public class TermsVersion
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
            if (DateTime.TryParseExact(PublishedAtString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                return result;

            return DateTime.MinValue;
        }
    }

    public bool HasViewed { get; set; }
}
