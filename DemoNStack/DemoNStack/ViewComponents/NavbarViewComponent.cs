using DemoNStack.Extensions;
using DemoNStack.Models;
using DemoNStack.Models.ViewModels;
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
        private INStackAppService NStackAppService { get; }
        private IMemoryCache Cache { get; }

        public NavbarViewComponent(INStackAppService nStackAppService, IMemoryCache memoryCache)
        {
            NStackAppService = nStackAppService ?? throw new ArgumentNullException(nameof(nStackAppService));
            Cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var res = await NStackAppService.GetResourceAsync<Translation>(Request.GetCurrentLanguage(), NStackPlatform.Web, "1.3.0");

            var viewModel = new NavbarViewModel
            {
                CurrentLanguage = Request.GetCurrentLanguage(),
                Language = res.Data.Default.Language
            };

            return View(viewModel);
        }
    }
}
