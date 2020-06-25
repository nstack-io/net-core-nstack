using Newtonsoft.Json;
using NStack.SDK.Models;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NStack.SDK.Repositories.Implementation
{
    public class NStackRepository : INStackRepository
    {
        protected readonly RestClient Client;

        public NStackRepository(NStackConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (string.IsNullOrWhiteSpace(configuration.ApiKey))
                throw new ArgumentNullException(configuration.ApiKey);

            if (string.IsNullOrWhiteSpace(configuration.ApplicationId))
                throw new ArgumentNullException(configuration.ApplicationId);

            if (string.IsNullOrWhiteSpace(configuration.BaseUrl))
                throw new ArgumentNullException(configuration.BaseUrl);

            Client = new RestClient(new Uri(configuration.BaseUrl));

            Client.AddHandler("application/json", () =>
            {
                return new JsonSerializer();
            });

            Client.AddDefaultHeader("X-Application-Id", configuration.ApplicationId);
            Client.AddDefaultHeaders(new Dictionary<string, string>
            {
                { "X-Application-Id", configuration.ApplicationId },
                { "X-Rest-Api-Key", configuration.ApiKey }
            });
        }

        async Task<T> INStackRepository.DoRequest<T>(IRestRequest request, Action<HttpStatusCode> errorHandling)
        {
            var resp = await Client.ExecuteAsync<T>(request);
            var code = (int)resp.StatusCode;
            if (code > 299 || code < 200)
            {
                if(errorHandling != null)
                    errorHandling.Invoke(resp.StatusCode);
                return null;
            }

            return resp.Data;
        }
    }

    public class JsonSerializer : IDeserializer
    {
        public virtual T Deserialize<T>(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
