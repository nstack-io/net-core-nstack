using Microsoft.AspNetCore.Http;

namespace DemoNStack.Extensions
{
    public static class HttpRequestExtensions
    {
        private const string DefaultLanguage = "en-GB";

        public static string GetCurrentLanguage(this HttpRequest request) => request.Query.ContainsKey("lang") ? (string)request.Query["lang"] : DefaultLanguage;
    }
}
