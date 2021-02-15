using DemoNStack.Extensions;
using DemoNStack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NStack.SDK.Models;
using NStack.SDK.Services;
using System;
using System.Threading.Tasks;

namespace DemoNStack.Controllers
{
    public abstract class NStackController : Controller
    {
        protected INStackLocalizeService NStackLocalizeService { get; }
        protected IMemoryCache Cache { get; }

        protected NStackController(IMemoryCache cache, INStackLocalizeService nStackLocalizeService)
        {
            Cache = cache ?? throw new ArgumentNullException(nameof(cache));
            NStackLocalizeService = nStackLocalizeService ?? throw new ArgumentNullException(nameof(nStackLocalizeService));
        }

        protected async Task<DataMetaWrapper<Translation>> GetTranslations()
        {
            return await Cache.GetOrCreateAsync($"nstack-translation-{Request.GetCurrentLanguage()}", async e =>
            {
                e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);

                var res = await NStackLocalizeService.GetResource<Translation>(Request.GetCurrentLanguage(), NStackPlatform.Backend);

                if (res == null)
                    res = await NStackLocalizeService.GetDefaultResource<Translation>(NStackPlatform.Backend);

                return res;
            });
        }
    }
}
