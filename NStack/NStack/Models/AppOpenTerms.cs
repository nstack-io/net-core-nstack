namespace NStack.SDK.Models;

public class AppOpenTerms
{
    public int Id { get; set; }

    public string Type { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public TermsVersion Version { get; set; } = new TermsVersion();
}
