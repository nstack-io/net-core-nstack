namespace DemoNStack.Controllers;

public abstract class NStackController : Controller
{
    protected INStackAppService NStackAppService { get; }

    protected NStackController(INStackAppService nStackAppService)
    {
        NStackAppService = nStackAppService ?? throw new ArgumentNullException(nameof(nStackAppService));
    }

    protected async Task<DataMetaWrapper<Translation>> GetTranslations()
    {
        return await NStackAppService.GetResourceAsync<Translation>(Request.GetCurrentLanguage(), NStackPlatform.Web, "1.3.1");
    }

    protected Guid GetUserId()
    {
        var userIdString = HttpContext.Session.GetString("UserId");

        if (!Guid.TryParse(userIdString, out Guid userId))
        {
            userId = Guid.NewGuid();

            HttpContext.Session.SetString("UserId", userId.ToString());
        }

        return userId;
    }
}
