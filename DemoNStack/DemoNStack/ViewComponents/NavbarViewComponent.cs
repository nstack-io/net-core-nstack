namespace DemoNStack.ViewComponents;

public class NavbarViewComponent : ViewComponent
{
    private INStackAppService NStackAppService { get; }

    public NavbarViewComponent(INStackAppService nStackAppService)
    {
        NStackAppService = nStackAppService ?? throw new ArgumentNullException(nameof(nStackAppService));
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var res = await NStackAppService.GetResourceAsync<Translation>(Request.GetCurrentLanguage(), NStackPlatform.Web, "1.3.1");

        var viewModel = new NavbarViewModel
        {
            CurrentLanguage = Request.GetCurrentLanguage(),
            Language = res.Data.Default.Language
        };

        return View(viewModel);
    }
}
