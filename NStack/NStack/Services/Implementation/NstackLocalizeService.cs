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
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<DataMetaWrapper<TSection>> GetDefaultResourceAsync<TSection>(NStackPlatform platform) where TSection : ResourceItem
        {
            var languages = await GetLanguagesAsync(platform);

            var defaultLanguage = languages.Data.First(l => l.Language.IsDefault);

            return await GetResourceAsync<TSection>(defaultLanguage.Id);
        }

        public Task<DataMetaWrapper<ResourceItem>> GetDefaultResourceAsync(NStackPlatform platform) => GetDefaultResourceAsync<ResourceItem>(platform);

        public Task<DataWrapper<List<ResourceData>>> GetLanguagesAsync(NStackPlatform platform)
        {
            var req = new RestRequest($"api/v2/content/localize/resources/platforms/{platform.ToString().ToLower()}");
            return _repository.DoRequest<DataWrapper<List<ResourceData>>>(req);
        }

        public Task<DataMetaWrapper<TSection>> GetResourceAsync<TSection>(int id) where TSection : ResourceItem
        {
            var req = new RestRequest($"api/v2/content/localize/resources/{id}");
            
            return _repository.DoRequest<DataMetaWrapper<TSection>>(req);
        }

        public Task<DataMetaWrapper<ResourceItem>> GetResourceAsync(int id) => GetResourceAsync<ResourceItem>(id);

        public async Task<DataMetaWrapper<TSection>> GetResourceAsync<TSection>(string locale, NStackPlatform platform) where TSection : ResourceItem
        {
            if (locale == null)
                throw new ArgumentNullException(locale);

            var languages = await GetLanguagesAsync(platform);

            var localeLanguage = languages.Data.FirstOrDefault(l => l.Language.Locale.Equals(locale));

            if (localeLanguage == null)
                return null;

            return await GetResourceAsync<TSection>(localeLanguage.Id);
        }

        public Task<DataMetaWrapper<ResourceItem>> GetResourceAsync(string locale, NStackPlatform platform) => GetResourceAsync<ResourceItem>(locale, platform);
    }
}
