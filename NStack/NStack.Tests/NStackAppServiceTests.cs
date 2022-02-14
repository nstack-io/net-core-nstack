using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NStack.SDK.Models;
using NStack.SDK.Repositories;
using NStack.SDK.Services;
using NStack.SDK.Services.Implementation;
using NStack.Tests.Translations;
using NUnit.Framework;
using RestSharp;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NStack.SDK.Tests
{
    public class NStackAppServiceTests
    {
        private NStackAppService _service;
        private Mock<INStackRepository> _repository;
        private Mock<INStackLocalizeService> _localizeService;
        private IMemoryCache _memoryCache;
        private DataMetaWrapper<TranslationData> _danish;
        private const int LanguageId = 42;
        private const string LanguageLocale = "da-DK";

        [SetUp]
        public void Initialize()
        {
            _repository = new Mock<INStackRepository>(MockBehavior.Strict);
            _repository.Setup(r => r.DoRequestAsync<DataAppOpenWrapper>(It.Is<RestRequest>(s => s.Resource.EndsWith("api/v2/open")), It.IsAny<Action<HttpStatusCode>>()))
                .Returns(GetAppOpenMock);

            var danish = new TranslationData();
            var defaultSection = new DefaultSection();
            defaultSection.TryAdd("text", "Jeg er på dansk");
            danish.TryAdd("default", defaultSection);

            _danish = new DataMetaWrapper<TranslationData>
            {
                Data = danish,
                Meta = new MetaData
                {
                    Language = new Language
                    {
                        Direction = LanguageDirection.LRM,
                        Id = LanguageId,
                        IsBestFit = true,
                        IsDefault = true,
                        Locale = LanguageLocale,
                        Name = "Danish"
                    },
                    Platform = new ResourcePlatform
                    {
                        Id = LanguageId,
                        Slug = NStackPlatform.Web
                    }
                }
            };

            _localizeService = new Mock<INStackLocalizeService>(MockBehavior.Strict);
            _localizeService.Setup(r => r.GetResourceAsync<TranslationData>(It.Is<int>(id => id == LanguageId)))
                .Returns(Task.FromResult(_danish));

            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            _memoryCache = serviceProvider.GetService<IMemoryCache>();

            _service = new NStackAppService(_repository.Object, _localizeService.Object, _memoryCache);
        }

        #region GetResourceAsync
        [Test]
        public void GetResourceAsyncNullLocaleThrowsException()
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetResourceAsync(null, NStackPlatform.Web, "1.0.0"));

            Assert.AreEqual("locale", exception.ParamName);
        }

        [Test]
        public void GetResourceAsyncEmptyLocaleThrowsException()
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetResourceAsync(string.Empty, NStackPlatform.Web, "1.0.0"));

            Assert.AreEqual("locale", exception.ParamName);
        }

        [Test]
        public void GetResourceAsyncTrueDevelopmentAndProductionThrowsException()
        {
            Assert.ThrowsAsync<ArgumentException>(() => _service.GetResourceAsync("da-DK", NStackPlatform.Web, "1.0.0", developmentEnvironment: true, productionEnvironment: true));
        }

        [Test]
        public async Task GetResourceAsyncFetchesLocalization()
        {
            ResetCounters();

            DataMetaWrapper<TranslationData> resource = await _service.GetResourceAsync<TranslationData>(LanguageLocale, NStackPlatform.Web, "1.0.0");

            Assert.AreEqual(_danish, resource);
        }

        [Test]
        public async Task GetResourceAsyncDoesntFetchLocalizationOnSecondCall()
        {
            ResetCounters();

            DataMetaWrapper<TranslationData> resource = await _service.GetResourceAsync<TranslationData>(LanguageLocale, NStackPlatform.Web, "1.0.0");
            DataMetaWrapper<TranslationData> resource2 = await _service.GetResourceAsync<TranslationData>(LanguageLocale, NStackPlatform.Web, "1.0.0");

            _repository.Verify(s => s.DoRequestAsync<DataAppOpenWrapper>(It.IsAny<RestRequest>(), It.IsAny<Action<HttpStatusCode>>()), Times.Once());
            _localizeService.Verify(s => s.GetResourceAsync<TranslationData>(LanguageId), Times.Once());
        }

        [Test]
        public async Task GetResourceAsyncFetchLocalizationOnSecondCallOnExpiry()
        {
            ResetCounters();

            DataMetaWrapper<TranslationData> resource = await _service.GetResourceAsync<TranslationData>(LanguageLocale, NStackPlatform.Web, "1.0.0");

            _memoryCache.Set<DateTime>("nstack-last-updated", DateTime.UtcNow.AddHours(-2));

            DataMetaWrapper<TranslationData> resource2 = await _service.GetResourceAsync<TranslationData>(LanguageLocale, NStackPlatform.Web, "1.0.0");

            _repository.Verify(s => s.DoRequestAsync<DataAppOpenWrapper>(It.IsAny<RestRequest>(), It.IsAny<Action<HttpStatusCode>>()), Times.Exactly(2));
            _localizeService.Verify(s => s.GetResourceAsync<TranslationData>(LanguageId), Times.Exactly(2));
        }

        [Test]
        public async Task GetResourceAsyncHonoursShouldUpdate()
        {
            ResetCounters();

            DataMetaWrapper<TranslationData> resource = await _service.GetResourceAsync<TranslationData>(LanguageLocale, NStackPlatform.Web, "1.0.0");

            _memoryCache.Set<DateTime>("nstack-last-updated", DateTime.UtcNow.AddHours(-2));

            DataMetaWrapper<TranslationData> resource2 = await _service.GetResourceAsync<TranslationData>(LanguageLocale, NStackPlatform.Web, "1.0.0");

            _memoryCache.Set<DateTime>("nstack-last-updated", DateTime.UtcNow.AddHours(-2));

            DataMetaWrapper<TranslationData> resource3 = await _service.GetResourceAsync<TranslationData>(LanguageLocale, NStackPlatform.Web, "1.0.0");

            _repository.Verify(s => s.DoRequestAsync<DataAppOpenWrapper>(It.IsAny<RestRequest>(), It.IsAny<Action<HttpStatusCode>>()), Times.Exactly(3));
            _localizeService.Verify(s => s.GetResourceAsync<TranslationData>(LanguageId), Times.Exactly(2));
        }
        #endregion

        #region GetDefaultResourceAsync
        [Test]
        public void GetDefaultResourceAsyncTrueDevelopmentAndProductionThrowsException()
        {
            Assert.ThrowsAsync<ArgumentException>(() => _service.GetDefaultResourceAsync(NStackPlatform.Web, "1.0.0", developmentEnvironment: true, productionEnvironment: true));
        }

        [Test]
        public async Task GetDefaultResourceAsyncFetchesLocalization()
        {
            ResetCounters();

            DataMetaWrapper<TranslationData> resource = await _service.GetDefaultResourceAsync<TranslationData>(NStackPlatform.Web, "1.0.0");

            Assert.AreEqual(_danish, resource);
        }

        [Test]
        public async Task GetDefaultResourceAsyncDoesntFetchLocalizationOnSecondCall()
        {
            ResetCounters();

            DataMetaWrapper<TranslationData> resource = await _service.GetDefaultResourceAsync<TranslationData>(NStackPlatform.Web, "1.0.0");
            DataMetaWrapper<TranslationData> resource2 = await _service.GetDefaultResourceAsync<TranslationData>(NStackPlatform.Web, "1.0.0");

            _repository.Verify(s => s.DoRequestAsync<DataAppOpenWrapper>(It.IsAny<RestRequest>(), It.IsAny<Action<HttpStatusCode>>()), Times.Once());
            _localizeService.Verify(s => s.GetResourceAsync<TranslationData>(LanguageId), Times.Once());
        }

        [Test]
        public async Task GetDefaultResourceAsyncFetchLocalizationOnSecondCallOnExpiry()
        {
            ResetCounters();

            DataMetaWrapper<TranslationData> resource = await _service.GetDefaultResourceAsync<TranslationData>(NStackPlatform.Web, "1.0.0");

            _memoryCache.Set<DateTime>("nstack-last-updated", DateTime.UtcNow.AddHours(-2));

            DataMetaWrapper<TranslationData> resource2 = await _service.GetDefaultResourceAsync<TranslationData>(NStackPlatform.Web, "1.0.0");

            _repository.Verify(s => s.DoRequestAsync<DataAppOpenWrapper>(It.IsAny<RestRequest>(), It.IsAny<Action<HttpStatusCode>>()), Times.Exactly(2));
            _localizeService.Verify(s => s.GetResourceAsync<TranslationData>(LanguageId), Times.Exactly(2));
        }

        [Test]
        public async Task GetDefaultResourceAsyncHonoursShouldUpdate()
        {
            ResetCounters();

            DataMetaWrapper<TranslationData> resource = await _service.GetDefaultResourceAsync<TranslationData>(NStackPlatform.Web, "1.0.0");

            _memoryCache.Set<DateTime>("nstack-last-updated", DateTime.UtcNow.AddHours(-2));

            DataMetaWrapper<TranslationData> resource2 = await _service.GetDefaultResourceAsync<TranslationData>(NStackPlatform.Web, "1.0.0");

            _memoryCache.Set<DateTime>("nstack-last-updated", DateTime.UtcNow.AddHours(-2));

            DataMetaWrapper<TranslationData> resource3 = await _service.GetDefaultResourceAsync<TranslationData>(NStackPlatform.Web, "1.0.0");

            _repository.Verify(s => s.DoRequestAsync<DataAppOpenWrapper>(It.IsAny<RestRequest>(), It.IsAny<Action<HttpStatusCode>>()), Times.Exactly(3));
            _localizeService.Verify(s => s.GetResourceAsync<TranslationData>(LanguageId), Times.Exactly(2));
        }
        #endregion

        private void ResetCounters()
        {
            _memoryCache.Remove("nstack-last-updated");
            _memoryCache.Remove($"nstack-localization-{LanguageLocale}");
            _memoryCache.Remove("nstack-localization-default");
            appOpenCount = 0;
        }

        private static int appOpenCount = 0;
        private Task<DataAppOpenWrapper> GetAppOpenMock()
        {
            return Task.FromResult(new DataAppOpenWrapper
            {
                Data = new AppOpenData
                {
                    Count = 1,
                    CreatedAt = DateTime.UtcNow,
                    Localize = new []
                    {
                        new ResourceData
                        {
                            Id = LanguageId,
                            Language = new Language
                            {
                                Direction = LanguageDirection.LRM,
                                Id = LanguageId,
                                IsBestFit = true,
                                IsDefault = true,
                                Locale = LanguageLocale,
                                Name = "Danish"
                            },
                            LastUpdatedAt = DateTime.UtcNow.AddMonths(-1),
                            ShouldUpdate = appOpenCount++ < 2,
                            Url = "https://nstack.io"
                        }
                    },
                    Platform = NStackPlatform.Web,
                    Terms = Enumerable.Empty<AppOpenTerms>()
                },
                Meta = new AppOpenMetaData
                {
                    AcceptLanguage = null
                }
            });
        }
    }
}
