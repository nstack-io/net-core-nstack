namespace NStack.SDK.Models
{
    public class AppOpenTerms
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public TermsVersion Version { get; set; }
    }
}
