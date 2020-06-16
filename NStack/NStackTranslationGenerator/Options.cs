using CommandLine;

namespace NStackTranslationGenerator
{
    public class Options
    {
        [Option('k', "apiKey", Required = true, HelpText = "The API key for your NStack integration")]
        public string ApiKey { get; set; }

        [Option('i', "applicationId", Required = true, HelpText = "The ID for your NStack application")]
        public string ApplicationId { get; set; }

        [Option('u', "url", Default = "https://nstack.io/", HelpText = "The base url of the NStack service")]
        public string BaseUrl { get; set; }

        [Option('c', "className", Default = "Translation", HelpText = "The name of class holding the translation sections")]
        public string ClassName { get; set; }

        [Option('n', "namespace", Required = true, HelpText = "The namespace for the created classes")]
        public string Namespace { get; set; }

        [Option('s', "showJson", Default = false, HelpText = "Show the fetched JSON from NStack in the console?")]
        public bool ShowJson { get; set; }

        [Option('t', "translationId", Required = true, HelpText = "The ID of the translation to generate the classes from")]
        public string TranslationId { get; set; }

        [Option('o', "output", Default = "./output", HelpText = "The output folder for the generated classes")]
        public string OutputFolder { get; set; }
    }
}
