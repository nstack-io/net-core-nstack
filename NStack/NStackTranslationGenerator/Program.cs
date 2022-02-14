namespace NStackTranslationGenerator;

public class Program
{
    private static Options Options { get; set; } = new Options();
    private static bool Parsed { get; set; } = false;

    public static async Task Main(string[] args)
    {
        var options = Parser.Default.ParseArguments<Options>(args)
            .WithParsed(SaveOptions)
            .WithNotParsed(HandleErrors);

        if (!Parsed)
        {
            return;
        }

        var translator = new Translator(Options);

        await translator.PerformTranslation();
    }

    private static void SaveOptions(Options opts)
    {
        Options = opts;
        Parsed = true;
    }

    private static void HandleErrors(IEnumerable<Error> errors)
    {
        foreach (var error in errors)
        {
            if(error is MissingRequiredOptionError missing)
            {
                Console.WriteLine($"Missing argument {missing.NameInfo.NameText}");
            }
        }

        Parsed = false;
    }
}
