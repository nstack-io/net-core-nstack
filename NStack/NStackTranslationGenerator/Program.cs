using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NStackTranslationGenerator
{
    public class Program
    {
        private static Options Options { get; set; }

        public static async Task Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<Options>(args)
                .WithParsed(SaveOptions);

            var translator = new Translator(Options);

            await translator.PerformTranslation();
        }

        private static void SaveOptions(Options opts)
        {
            Options = opts;
        }
    }
}
