using Microsoft.Extensions.Caching.Memory;
using NStack.SDK.Models;
using NStack.SDK.Repositories;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NStack.SDK.Services.Implementation
{
    public class NStackAppService : INStackAppService
    {
        private readonly INStackRepository _repository;
        private readonly INStackLocalizeService _localizeService;
        private readonly IMemoryCache _memoryCache;
        private const string OldVersionCacheKey = "nstack-old-version";
        private const string LastUpdatedCacheKey = "nstack-last-updated";
        private const string CurrentExecutionIdCacheKey = "nstack-guid";
        private const string LocalizationCacheKeyPrefix = "nstack-localization-";

        public NStackAppService(INStackRepository repository, INStackLocalizeService localizeService, IMemoryCache memoryCache)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _localizeService = localizeService ?? throw new ArgumentNullException(nameof(localizeService));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task<DataAppOpenWrapper> AppOpen(NStackPlatform platform, string version, string environment = "production", bool developmentEnvironment = false, bool productionEnvironment = true)
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
                guid = GetCurrentExecutionGuid(),
                version = version,
                old_version = GetOldVersion() ?? version,
                last_updated = GetLastUpdatedString(),
                dev = developmentEnvironment,
                test = !productionEnvironment
            }, "application/x-www-form-urlencoded");

            var response = await _repository.DoRequest<DataAppOpenWrapper>(request);

            if (response == null)
                return null;

            _memoryCache.Set<DateTime>(LastUpdatedCacheKey, DateTime.UtcNow);
            _memoryCache.Set<string>(OldVersionCacheKey, version);

            return response;
        }

        private Guid GetCurrentExecutionGuid() => _memoryCache.GetOrCreate<Guid>(CurrentExecutionIdCacheKey, entry => Guid.NewGuid());

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

        public async Task<DataMetaWrapper<TSection>> GetResource<TSection>(string locale,
                                                                           NStackPlatform platform,
                                                                           string version,
                                                                           string environment = "production",
                                                                           bool developmentEnvironment = false,
                                                                           bool productionEnvironment = true) where TSection : ResourceItem
        {
            if (string.IsNullOrWhiteSpace(locale))
                throw new ArgumentException($"{nameof(locale)} must not be null or empty", nameof(locale));

            DataAppOpenWrapper appOpenData = await AppOpen(platform, version, environment, developmentEnvironment, productionEnvironment);

            if (appOpenData == null)
                return null;

            ResourceData localizeToFetch = appOpenData.Data.Localize.FirstOrDefault(l => locale.Equals(l.Language.Locale, StringComparison.InvariantCultureIgnoreCase));

            if(localizeToFetch == null)
            {
                localizeToFetch = appOpenData.Data.Localize.FirstOrDefault(l => l.Language.IsDefault);

                if(localizeToFetch == null)
                    return null;
            }

            return await GetLocalization<TSection>(localizeToFetch);
        }

        public Task<DataMetaWrapper<ResourceItem>> GetResource(string locale,
                                                               NStackPlatform platform,
                                                               string version,
                                                               string environment = "production",
                                                               bool developmentEnvironment = false,
                                                               bool productionEnvironment = true) => GetResource<ResourceItem>(locale, platform, version, environment, developmentEnvironment, productionEnvironment);

        public async Task<DataMetaWrapper<TSection>> GetDefaultResource<TSection>(NStackPlatform platform, string version, string environment = "production", bool developmentEnvironment = false, bool productionEnvironment = true) where TSection : ResourceItem
        {
            var appOpenData = await AppOpen(platform, version, environment, developmentEnvironment, productionEnvironment);

            if (appOpenData == null)
                return null;

            ResourceData localizeToFetch = appOpenData.Data.Localize.FirstOrDefault(l => l.Language.IsDefault);

            if (localizeToFetch == null)
                return null;

            return await GetLocalization<TSection>(localizeToFetch);
        }

        public Task<DataMetaWrapper<ResourceItem>> GetDefaultResource(NStackPlatform platform,
                                                                      string version,
                                                                      string environment = "production",
                                                                      bool developmentEnvironment = false,
                                                                      bool productionEnvironment = true) => GetDefaultResource<ResourceItem>(platform, version, environment, developmentEnvironment, productionEnvironment);

        private async Task<DataMetaWrapper<TSection>> GetLocalization<TSection>(ResourceData localizeToFetch) where TSection : ResourceItem
        {
            if (localizeToFetch == null)
                throw new ArgumentNullException(nameof(localizeToFetch));

            //If there is an update or the data isn't cached, fetch it
            if (!localizeToFetch.ShouldUpdate && _memoryCache.TryGetValue<DataMetaWrapper<TSection>>($"{LocalizationCacheKeyPrefix}{localizeToFetch.Id}", out DataMetaWrapper<TSection> localization))
                return localization;

            localization = await _localizeService.GetResource<TSection>(localizeToFetch.Id);

            if (localization != null)
                _memoryCache.Set<DataMetaWrapper<TSection>>($"{LocalizationCacheKeyPrefix}{localizeToFetch.Id}", localization);

            return localization;
        }
    }
}
