using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoNStack.Models;
using NStack.SDK.Services;
using Microsoft.Extensions.Caching.Memory;
using NStack.SDK.Models;

namespace DemoNStack.Controllers
{
    public class HomeController : BaseController
    {
        private INStackLocalizeService NStackLocalizeService { get; }
        private IMemoryCache Cache { get; }

        public HomeController(INStackLocalizeService nStackLocalizeService, IMemoryCache memoryCache)
        {
            NStackLocalizeService = nStackLocalizeService ?? throw new ArgumentNullException(nameof(nStackLocalizeService));
            Cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task<IActionResult> Index()
        {
            var res = await Cache.GetOrCreateAsync($"nstack-translation-{CurrentLanguage}", async e =>
            {
                e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);

                var res = await NStackLocalizeService.GetResource<Translation>(CurrentLanguage, NStackPlatform.Backend);

                if (res == null)
                    res = await NStackLocalizeService.GetDefaultResource<Translation>(NStackPlatform.Backend);

                return res;
            });
            
            return View(res.Data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
