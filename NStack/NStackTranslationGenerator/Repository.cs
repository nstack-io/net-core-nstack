using NStack.SDK.Models;
using NStack.SDK.Repositories.Implementation;
using RestSharp;

namespace NStackTranslationGenerator
{
    internal class Repository : NStackRepository
    {
        internal Repository(NStackConfiguration configuration) : base(configuration)
        {
        }

        internal async Task<string> DoRequest(RestRequest request)
        {
            var resp = await Client.ExecuteAsync(request);
            var code = (int)resp.StatusCode;
            if (code > 299 || code < 200)
            {
                return null;
            }

            return resp.Content;
        }
    }
}
