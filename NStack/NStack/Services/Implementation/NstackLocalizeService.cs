using NStack.Models;
using NStack.Repositories;
using RestSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NStack.Services.Implementation
{
    public class NstackLocalizeService : INstackLocalizeService<ResourceItem>
    {
        private readonly INstackRepository _repository;
        public NstackLocalizeService(INstackRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));
            _repository = repository;
        }

        public async Task<DataWrapper<List<ResourceData>>> GetLanguages()
        {
            var req = new RestRequest("api/v2/content/localize/resources/platforms/backend");
            return await _repository.DoRequest<DataWrapper<List<ResourceData>>>(req);
        }

        public async Task<DataMetaWrapper<ResourceItem>> GetResource(int id)
        {
            var req = new RestRequest(string.Format("/api/v2/content/localize/resources/{0}", id));
            return await _repository.DoRequest<DataMetaWrapper<ResourceItem>>(req);
        }
    }
}
