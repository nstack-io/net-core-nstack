using NStack.Models;
using NStack.Repositories.Implementation;
using RestSharp;
using System.Threading.Tasks;

namespace NStackTranslationGenerator
{
    internal class Repository : NStackRepository
    {
        internal Repository(NStackConfiguration configuration) : base(configuration)
        {
        }

        internal async Task<string> DoRequest(IRestRequest request)
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
