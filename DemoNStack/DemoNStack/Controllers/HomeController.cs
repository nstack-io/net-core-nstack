using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoNStack.Models;
using NStack.SDK.Services;

namespace DemoNStack.Controllers
{
    public class HomeController : Controller
    {
        private INStackLocalizeService NStackLocalizeService { get; }

        public HomeController(INStackLocalizeService nStackLocalizeService)
        {
            NStackLocalizeService = nStackLocalizeService ?? throw new ArgumentNullException(nameof(nStackLocalizeService));
        }

        public async Task<IActionResult> Index()
        {
            var langs = await NStackLocalizeService.GetLanguages(NStack.SDK.Models.NStackPlatform.Backend);
            var res = await NStackLocalizeService.GetResource<Translation>(langs.Data.First().Id);
            
            return View(res.Data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
