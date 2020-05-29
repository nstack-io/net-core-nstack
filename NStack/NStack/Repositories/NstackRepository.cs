using NStack.Models;
using RestSharp;
using System;
using System.Collections.Generic;

namespace NStack.Repositories
{
    public sealed class NstackRepository : INstackRepository
    {
        private readonly RestClient _client;

        public NstackRepository(NstackConfiguration configuration)
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

            _client.AddDefaultHeader("X-Application-Id", configuration.ApplicationId);
            _client.AddDefaultHeaders(new Dictionary<string, string>
            {
                { "X-Application-Id", configuration.ApplicationId },
                { "X-Rest-Api-Key", configuration.ApiKey }
            });
        }

        IRestResponse<T> INstackRepository.DoRequest<T>(IRestRequest request)
        {
            return _client.Execute<T>(request);
        }
    }
}
