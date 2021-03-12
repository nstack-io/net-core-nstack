using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoNStack.Models;
using NStack.SDK.Services;
using NStack.SDK.Models;

namespace DemoNStack.Controllers
{
    public class HomeController : NStackController
    {
        public HomeController(INStackAppService nStackAppService) : base(nStackAppService)
        {
        }

        public async Task<IActionResult> Index()
        {
            DataMetaWrapper<Translation> res = await GetTranslations();
            
            return View(res.Data.Default);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
