using DemoNStack.Extensions;
using DemoNStack.Models;
using DemoNStack.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NStack.SDK.Models;
using NStack.SDK.Services;
using System;
using System.Threading.Tasks;

namespace DemoNStack.Controllers
{
    public class TermsController : NStackController
    {
        private INStackTermsService TermsService { get; }

        public TermsController(IMemoryCache cache, INStackLocalizeService localizeService, INStackTermsService termsService) : base(cache, localizeService)
        {
            TermsService = termsService ?? throw new ArgumentNullException(nameof(termsService));
        }

        public async Task<IActionResult> Index()
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if(!Guid.TryParse(userIdString, out Guid userId))
            {
                userId = Guid.NewGuid();

                HttpContext.Session.SetString("UserId", userId.ToString());
            }

            DataWrapper<TermsWithContent> newestTerms = await Cache.GetOrCreateAsync($"terms-{Request.GetCurrentLanguage()}-{userId}", async e => {
                e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);

                return await TermsService.GetNewestTerms("terms", userId.ToString(), Request.GetCurrentLanguage());
            });

            DataMetaWrapper<Translation> translations = await GetTranslations();

            var viewModel = new TermsViewModel
            {
                Content = newestTerms.Data.Content.Data,
                HasApproved = newestTerms.Data.HasViewed,
                Headline = translations.Data.Terms.Headline,
                ApproveButton = translations.Data.Terms.Approve,
                HasApprovedString = translations.Data.Terms.HasApproved
            };

            return View(viewModel);
        }
    }
}
