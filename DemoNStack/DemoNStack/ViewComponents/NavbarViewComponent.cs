using DemoNStack.Extensions;
using DemoNStack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NStack.SDK.Models;
using NStack.SDK.Services;
using System;
using System.Threading.Tasks;

namespace DemoNStack.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private INStackLocalizeService NStackLocalizeService { get; }
        private IMemoryCache Cache { get; }

        public NavbarViewComponent(INStackLocalizeService nStackLocalizeService, IMemoryCache memoryCache)
        {
            NStackLocalizeService = nStackLocalizeService ?? throw new ArgumentNullException(nameof(nStackLocalizeService));
            Cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var res = await Cache.GetOrCreateAsync($"nstack-translation-{Request.GetCurrentLanguage()}", async e =>
            {
                e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);

                var res = await NStackLocalizeService.GetResource<Translation>(Request.GetCurrentLanguage(), NStackPlatform.Backend);

                if (res == null)
                    res = await NStackLocalizeService.GetDefaultResource<Translation>(NStackPlatform.Backend);

                return res;
            });

            return View(res.Data);
        }
    }
}
