using Newtonsoft.Json.Linq;
using NStack.Models;
using RestSharp;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NStackTranslationGenerator
{
    public sealed class Translator
    {
        private Options Options { get; }
        private Repository Repository { get; }

        public Translator(Options options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            Options = options;

            var configuration = new NStackConfiguration
            {
                ApiKey = options.ApiKey,
                ApplicationId = options.ApplicationId,
                BaseUrl = options.BaseUrl
            };

            Repository = new Repository(configuration);
        }

        public async Task PerformTranslation()
        {
            var request = new RestRequest($"/api/v2/content/localize/resources/{Options.TranslationId}");

            var json = await Repository.DoRequest(request);

            if (Options.ShowJson)
                Console.WriteLine(json);

            Regex reg = new Regex("{\"data\":(.*),\"meta\":");

            var match = reg.Match(json);

            if(match.Groups.Count < 2)
            {
                Console.WriteLine("Unable to parse JSON");
                return;
            }

            var jsonToParse = JObject.Parse(match.Groups[1].Value);

            var filesToCreate = JsonToCSharpParser.ParseResourceItem(jsonToParse, Options.ClassName, Options.Namespace);
        }
    }
}
