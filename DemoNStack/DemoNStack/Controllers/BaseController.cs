using Microsoft.AspNetCore.Mvc;

namespace DemoNStack.Controllers
{
    public class BaseController : Controller
    {
        private const string DefaultLanguage = "en-GB";

        protected string CurrentLanguage => Request.Query.ContainsKey("lang") ? (string)Request.Query["lang"] : DefaultLanguage;
    }
}
