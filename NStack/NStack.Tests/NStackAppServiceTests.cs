using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using NStack.SDK.Models;
using NStack.SDK.Repositories.Implementation;
using NStack.SDK.Services.Implementation;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace NStack.SDK.Tests
{
    public class NStackAppServiceTests
    {
        private NStackAppService _service;

        [SetUp]
        public void Initialize()
        {
            var repository = new NStackRepository(new NStackConfiguration
            {
                ApiKey = "qd1GiPnq8sJuChbFxjOQxV9t1AN71oIMBuWF",
                ApplicationId = "9vJhjXzSBUxBOuQx2B2mFIZSoa2aK4UJzt7y"
            });

            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            var localizationService = new NStackLocalizeService(repository);

            _service = new NStackAppService(repository, localizationService, serviceProvider.GetService<IMemoryCache>());
        }

        [Test]
        public async Task DeleteMe()
        {
            var userId = Guid.NewGuid();

            var data = await _service.AppOpenAsync(NStackPlatform.Backend, userId, "1.0.0", "development", true, false);
            var data2 = await _service.AppOpenAsync(NStackPlatform.Backend, userId, "1.0.0", "development", true, false);
        }

        [Test]
        public async Task DeleteMe2()
        {
            var data = await _service.GetResourceAsync("da-DK", NStackPlatform.Web, "1.0.0", "development", true, false);
            var data2 = await _service.GetResourceAsync("da-DK", NStackPlatform.Web, "1.0.0", "development", true, false);
        }
    }
}
