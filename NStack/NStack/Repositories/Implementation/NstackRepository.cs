using Newtonsoft.Json;
using NStack.Models;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NStack.Repositories.Implementation
{
    public sealed class NStackRepository : INStackRepository
    {
        private readonly RestClient _client;

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

            _client = new RestClient(new Uri(configuration.BaseUrl));

            _client.AddHandler("application/json", () =>
            {
                return new JsonSerializer();
            });

            _client.AddDefaultHeader("X-Application-Id", configuration.ApplicationId);
            _client.AddDefaultHeaders(new Dictionary<string, string>
            {
                { "X-Application-Id", configuration.ApplicationId },
                { "X-Rest-Api-Key", configuration.ApiKey }
            });
        }

        async Task<T> INStackRepository.DoRequest<T>(IRestRequest request, Action<HttpStatusCode> errorHandling)
        {
            var resp = await _client.ExecuteAsync<T>(request);
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
