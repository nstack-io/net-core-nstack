using Microsoft.Extensions.Caching.Memory;
using NStack.SDK.Models;
using NStack.SDK.Repositories;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace NStack.SDK.Services.Implementation
{
    public class NStackAppService : INStackAppService
    {
        private readonly INStackRepository _repository;
        private readonly IMemoryCache _memoryCache;
        private const string OldVersionCacheKey = "nstack-old-version";
        private const string LastUpdatedCacheKey = "nstack-last-updated";

        public NStackAppService(INStackRepository repository, IMemoryCache memoryCache)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task<DataAppOpenWrapper> AppOpen(NStackPlatform platform, string version, Guid userId, string environment = "production", bool developmentEnvironment = false, bool productionEnvironment = true)
        {
            if (string.IsNullOrWhiteSpace(version) && (platform != NStackPlatform.Web || platform != NStackPlatform.Backend))
                throw new ArgumentException($"Version is required on all platforms except web", nameof(version));

            if (userId == Guid.Empty)
                throw new ArgumentException("userId must be set", nameof(userId));

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

            var response = await _repository.DoRequest<DataAppOpenWrapper>(request);

            _memoryCache.Set<DateTime>(LastUpdatedCacheKey, DateTime.UtcNow);
            _memoryCache.Set<string>(OldVersionCacheKey, version);

            return response;
        }

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
    }
}
