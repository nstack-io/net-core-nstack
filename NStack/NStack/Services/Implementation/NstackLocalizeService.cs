using NStack.SDK.Models;
using NStack.SDK.Repositories;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NStack.SDK.Services.Implementation
{
    public class NStackLocalizeService : INStackLocalizeService
    {
        private readonly INStackRepository _repository;
        public NStackLocalizeService(INStackRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            _repository = repository;
        }

        public async Task<DataMetaWrapper<TSection>> GetDefaultResource<TSection>(NStackPlatform platform) where TSection : ResourceItem
        {
            var languages = await GetLanguages(platform);

            var defaultLanguage = languages.Data.First(l => l.Language.IsDefault);

            return await GetResource<TSection>(defaultLanguage.Id);
        }

        public Task<DataMetaWrapper<ResourceItem>> GetDefaultResource(NStackPlatform platform) => GetDefaultResource<ResourceItem>(platform);

        public Task<DataWrapper<List<ResourceData>>> GetLanguages(NStackPlatform platform)
        {
            var req = new RestRequest($"api/v2/content/localize/resources/platforms/{platform.ToString().ToLower()}");
            return _repository.DoRequest<DataWrapper<List<ResourceData>>>(req);
        }

        public Task<DataMetaWrapper<TSection>> GetResource<TSection>(int id) where TSection : ResourceItem
        {
            var req = new RestRequest($"api/v2/content/localize/resources/{id}");
            
            return _repository.DoRequest<DataMetaWrapper<TSection>>(req);
        }

        public Task<DataMetaWrapper<ResourceItem>> GetResource(int id) => GetResource<ResourceItem>(id);

        public async Task<DataMetaWrapper<TSection>> GetResource<TSection>(string locale, NStackPlatform platform) where TSection : ResourceItem
        {
            if (locale == null)
                throw new ArgumentNullException(locale);

            var languages = await GetLanguages(platform);

            var localeLanguage = languages.Data.FirstOrDefault(l => l.Language.Locale.Equals(locale));

            if (localeLanguage == null)
                return null;

            return await GetResource<TSection>(localeLanguage.Id);
        }

        public Task<DataMetaWrapper<ResourceItem>> GetResource(string locale, NStackPlatform platform) => GetResource<ResourceItem>(locale, platform);
    }
}
