using Microsoft.Extensions.Caching.Memory;
using NStack.SDK.Models;
using NStack.SDK.Repositories;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NStack.SDK.Services.Implementation
{
    public class NStackAppService : INStackAppService
    {
        private readonly INStackRepository _repository;
        private readonly INStackLocalizeService _localizeService;
        private readonly IMemoryCache _memoryCache;
        private readonly int _howOftenToCheckInMinutes;
        private const string OldVersionCacheKey = "nstack-old-version";
        private const string LastUpdatedCacheKey = "nstack-last-updated";
        private const string LocalizationCacheKeyPrefix = "nstack-localization-";

        public NStackAppService(INStackRepository repository, INStackLocalizeService localizeService, IMemoryCache memoryCache, int? howOftenToCheckInMinutes = null)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _localizeService = localizeService ?? throw new ArgumentNullException(nameof(localizeService));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _howOftenToCheckInMinutes = howOftenToCheckInMinutes ?? 5;
        }

        public async Task<DataAppOpenWrapper> AppOpenAsync<TSection>(NStackPlatform platform,
                                                      Guid userId,
                                                      string version,
                                                      string environment = "production",
                                                      bool developmentEnvironment = false,
                                                      bool productionEnvironment = true) where TSection : ResourceItem
        {
            if (string.IsNullOrWhiteSpace(version) && (platform != NStackPlatform.Web || platform != NStackPlatform.Backend))
                throw new ArgumentException("Version is required on all platforms except web", nameof(version));

            if (productionEnvironment && developmentEnvironment)
                throw new ArgumentException($"{nameof(productionEnvironment)} and {nameof(developmentEnvironment)} can't both be true");

            var request = new RestRequest("api/v2/open", Method.POST);
            request.AddHeader("N-Meta", $"{GetPlatformString(platform)};{environment};");

            request.AddJsonBody(new
            {
                platform = GetPlatformString(platform),
                guid = userId,
                version = version,
                old_version = GetOldVersion() ?? version,
                last_updated = GetLastUpdatedString(),
                dev = developmentEnvironment,
                test = !productionEnvironment
            }, "application/x-www-form-urlencoded");

            var response = await _repository.DoRequestAsync<DataAppOpenWrapper>(request);

            if (response == null)
                return null;

            _memoryCache.Set<DateTime>(LastUpdatedCacheKey, DateTime.UtcNow);
            _memoryCache.Set<string>(OldVersionCacheKey, version);

            // Fetch and store all updated localizations into the memory
            IEnumerable<Task<DataMetaWrapper<TSection>>> localizationFetches = response.Data.Localize.Where(l => l.ShouldUpdate).Select(GetLocalizationAsync<TSection>);

            await Task.WhenAll(localizationFetches);

            return response;
        }

        public Task<DataAppOpenWrapper> AppOpenAsync(NStackPlatform platform,
                                                      Guid userId,
                                                      string version,
                                                      string environment = "production",
                                                      bool developmentEnvironment = false,
                                                      bool productionEnvironment = true) => AppOpenAsync<ResourceItem>(platform, userId, version, environment, developmentEnvironment, productionEnvironment);

        private string GetLastUpdatedString()
        {
            if (_memoryCache.TryGetValue<DateTime>(LastUpdatedCacheKey, out DateTime lastUpdated))
                return $"{lastUpdated:yyyy-MM-ddTHH:mm:ss+00:00}";

            return null;
        }

        private string GetOldVersion()
        {
            if (_memoryCache.TryGetValue<string>(OldVersionCacheKey, out string oldVersion))
                return oldVersion;

            return null;
        }

        private string GetPlatformString(NStackPlatform platform)
        {
            return platform switch
            {
                var x when x == NStackPlatform.Web || x == NStackPlatform.Backend => "web",
                NStackPlatform.Mobile => "windows",
                _ => throw new ArgumentException($"Unknown platform {platform:g}")
            };
        }

        public async Task<DataMetaWrapper<TSection>> GetResourceAsync<TSection>(string locale,
                                                                           NStackPlatform platform,
                                                                           string version,
                                                                           string environment = "production",
                                                                           bool developmentEnvironment = false,
                                                                           bool productionEnvironment = true) where TSection : ResourceItem
        {
            if (string.IsNullOrWhiteSpace(locale))
                throw new ArgumentException($"{nameof(locale)} must not be null or empty", nameof(locale));

            if (!AppOpenIsExpired() && _memoryCache.TryGetValue<DataMetaWrapper<TSection>>($"{LocalizationCacheKeyPrefix}{locale}", out DataMetaWrapper<TSection> localization))
                return localization;

            DataAppOpenWrapper appOpenData = await AppOpenAsync<TSection>(platform, Guid.NewGuid(), version, environment, developmentEnvironment, productionEnvironment);

            if (appOpenData == null)
                return null;

            if (_memoryCache.TryGetValue<DataMetaWrapper<TSection>>($"{LocalizationCacheKeyPrefix}{locale}", out var fetchedLocalization))
                return fetchedLocalization;

            return _memoryCache.Get<DataMetaWrapper<TSection>>($"{LocalizationCacheKeyPrefix}default");
        }

        public Task<DataMetaWrapper<ResourceItem>> GetResourceAsync(string locale,
                                                               NStackPlatform platform,
                                                               string version,
                                                               string environment = "production",
                                                               bool developmentEnvironment = false,
                                                               bool productionEnvironment = true) => GetResourceAsync<ResourceItem>(locale, platform, version, environment, developmentEnvironment, productionEnvironment);

        public async Task<DataMetaWrapper<TSection>> GetDefaultResourceAsync<TSection>(NStackPlatform platform, string version, string environment = "production", bool developmentEnvironment = false, bool productionEnvironment = true) where TSection : ResourceItem
        {
            if (!AppOpenIsExpired() && _memoryCache.TryGetValue<DataMetaWrapper<TSection>>($"{LocalizationCacheKeyPrefix}default", out DataMetaWrapper<TSection> localization))
                return localization;

            var appOpenData = await AppOpenAsync<TSection>(platform, Guid.NewGuid(), version, environment, developmentEnvironment, productionEnvironment);

            if (appOpenData == null)
                return null;

            return _memoryCache.Get<DataMetaWrapper<TSection>>($"{LocalizationCacheKeyPrefix}default");
        }

        public Task<DataMetaWrapper<ResourceItem>> GetDefaultResourceAsync(NStackPlatform platform,
                                                                      string version,
                                                                      string environment = "production",
                                                                      bool developmentEnvironment = false,
                                                                      bool productionEnvironment = true) => GetDefaultResourceAsync<ResourceItem>(platform, version, environment, developmentEnvironment, productionEnvironment);

        private bool AppOpenIsExpired() => !_memoryCache.TryGetValue<DateTime>(LastUpdatedCacheKey, out DateTime lastUpdated) 
                                            || lastUpdated < DateTime.UtcNow.AddMinutes(_howOftenToCheckInMinutes * -1);

        private async Task<DataMetaWrapper<TSection>> GetLocalizationAsync<TSection>(ResourceData localizeToFetch) where TSection : ResourceItem
        {
            if (localizeToFetch == null)
                throw new ArgumentNullException(nameof(localizeToFetch));

            //If there is an update or the data isn't cached, fetch it
            if (!localizeToFetch.ShouldUpdate && _memoryCache.TryGetValue<DataMetaWrapper<TSection>>($"{LocalizationCacheKeyPrefix}{localizeToFetch.Language.Locale}", out DataMetaWrapper<TSection> localization))
                return localization;

            localization = await _localizeService.GetResourceAsync<TSection>(localizeToFetch.Id);

            if (localization != null)
            {
                _memoryCache.Set<DataMetaWrapper<TSection>>($"{LocalizationCacheKeyPrefix}{localizeToFetch.Language.Locale}", localization);

                if (localizeToFetch.Language.IsDefault)
                    _memoryCache.Set<DataMetaWrapper<TSection>>($"{LocalizationCacheKeyPrefix}default", localization);
            }

            return localization;
        }
    }
}
