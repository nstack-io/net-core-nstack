namespace DemoNStack.Controllers;

public class TermsController : NStackController
{
    private INStackTermsService TermsService { get; }
    private IMemoryCache Cache { get; }

    public TermsController(INStackAppService appService, IMemoryCache cache, INStackTermsService termsService) : base(appService)
    {
        Cache = cache ?? throw new ArgumentNullException(nameof(cache));
        TermsService = termsService ?? throw new ArgumentNullException(nameof(termsService));
    }

    public async Task<IActionResult> Index()
    {
        Guid userId = GetUserId();

        TermsWithContent newestTerms = await GetNewestTerms(userId, Request.GetCurrentLanguage());

        DataMetaWrapper<Translation> translations = await GetTranslations();

        var viewModel = new TermsViewModel
        {
            Content = newestTerms.Content.Data,
            HasApproved = newestTerms.HasViewed,
            Headline = translations.Data.Terms.Headline,
            ApproveButton = translations.Data.Terms.Approve,
            HasApprovedString = translations.Data.Terms.HasApproved
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AcceptTerms()
    {
        Guid userId = GetUserId();
        string language = Request.GetCurrentLanguage();

        TermsWithContent newestTerms = await GetNewestTerms(userId, language);

        await TermsService.MarkReadAsync(newestTerms.Id, userId.ToString(), language);

        //Clear cache so we get the updated boolean that the user has read the terms
        Cache.Remove($"terms-{language}-{userId}");

        return RedirectToAction("index", new { lang = language });
    }

    protected async Task<TermsWithContent> GetNewestTerms(Guid userId, string language)
    {
        return (await Cache.GetOrCreateAsync($"terms-{language}-{userId}", async e => {
            e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);

            return await TermsService.GetNewestTermsAsync("terms", userId.ToString(), language);
        })).Data;
    }
}
