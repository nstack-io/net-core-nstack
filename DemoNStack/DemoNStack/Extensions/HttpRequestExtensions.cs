namespace DemoNStack.Extensions;

public static class HttpRequestExtensions
{
    private const string DefaultLanguage = "en-GB";

    public static string GetCurrentLanguage(this HttpRequest request) => request.Query.ContainsKey("lang") ? (string)request.Query["lang"] : DefaultLanguage;

    public static string GetLanguageDirection(this HttpRequest request) => request.GetCurrentLanguage().StartsWith("ar-QA") ? "rtl" : "ltr";
}
