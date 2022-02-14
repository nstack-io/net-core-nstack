namespace NStack.Tests;

public class NStackLocalizeServiceTests
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private NStackLocalizeService _service;
    private Mock<INStackRepository> _repository;
    private ResourceData _englishLanguage = new ResourceData
    {
        Id = 1208,
        Language = new Language
        {
            Direction = LanguageDirection.LRM,
            Id = 11,
            IsBestFit = true,
            IsDefault = true,
            Locale = "en-GB",
            Name = "English (UK)"
        },
        LastUpdatedAt = DateTime.UtcNow,
        ShouldUpdate = false,
        Url = "https://cdn-raw.vapor.cloud/nstack/data/localize-publish/publish-1208-QxU4106o_Uk9W04tNE8.json"
    };
    private ResourceData _danishLanguage = new ResourceData
    {
        Id = 1209,
        Language = new Language
        {
            Direction = LanguageDirection.LRM,
            Id = 6,
            IsBestFit = false,
            IsDefault = false,
            Locale = "da-DK",
            Name = "Danish"
        },
        LastUpdatedAt = DateTime.UtcNow,
        ShouldUpdate = false,
        Url = "https://cdn-raw.vapor.cloud/nstack/data/localize-publish/publish-1209-6IpAMIFn_j1j6QGoMwm.json"
    };
    private ResourceData _arabicLanguage = new ResourceData
    {
        Id = 1224,
        Language = new Language
        {
            Direction = LanguageDirection.RML,
            Id = 51,
            IsBestFit = false,
            IsDefault = false,
            Locale = "ar-QA",
            Name = "Arabic (Saudi)"
        },
        LastUpdatedAt = DateTime.UtcNow,
        ShouldUpdate = false,
        Url = "https://cdn-raw.vapor.cloud/nstack/data/localize-publish/publish-1224-xOGMjHzq_icl2JE2NY7.json"
    };

    private TranslationData _english;
    private TranslationData _danish;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<INStackRepository>
        {
            DefaultValue = DefaultValue.Empty
        };

        _repository.Setup(r => r.DoRequestAsync<DataWrapper<List<ResourceData>>>(It.Is<RestRequest>(s => s.Resource.EndsWith("platforms/backend")), It.IsAny<Action<HttpStatusCode>>()))
            .Returns(GetLanguageMock);

        _english = new TranslationData();
        var defaultSection = new DefaultSection();
        defaultSection.TryAdd("text", "I'm in English");
        _english.TryAdd("default", defaultSection);

        _repository.Setup(r => r.DoRequestAsync<DataMetaWrapper<TranslationData>>(It.Is<RestRequest>(s => s.Resource.EndsWith($"resources/{_englishLanguage.Id}")), It.IsAny<Action<HttpStatusCode>>()))
            .Returns(Task.FromResult(new DataMetaWrapper<TranslationData> { Data = _english }));

        _repository.Setup(r => r.DoRequestAsync<DataMetaWrapper<ResourceItem>>(It.Is<RestRequest>(s => s.Resource.EndsWith($"resources/{_englishLanguage.Id}")), It.IsAny<Action<HttpStatusCode>>()))
            .Returns(Task.FromResult(new DataMetaWrapper<ResourceItem> { Data = _english }));

        _danish = new TranslationData();
        var defaultDanishSection = new DefaultSection();
        defaultDanishSection.TryAdd("text", "Jeg er på dansk");
        _danish.TryAdd("default", defaultDanishSection);

        _repository.Setup(r => r.DoRequestAsync<DataMetaWrapper<TranslationData>>(It.Is<RestRequest>(s => s.Resource.EndsWith($"resources/{_danishLanguage.Id}")), It.IsAny<Action<HttpStatusCode>>()))
            .Returns(Task.FromResult(new DataMetaWrapper<TranslationData> { Data = _danish }));

        _repository.Setup(r => r.DoRequestAsync<DataMetaWrapper<ResourceItem>>(It.Is<RestRequest>(s => s.Resource.EndsWith($"resources/{_danishLanguage.Id}")), It.IsAny<Action<HttpStatusCode>>()))
            .Returns(Task.FromResult(new DataMetaWrapper<ResourceItem> { Data = _danish }));

        _service = new NStackLocalizeService(_repository.Object);
    }

    [Test]
    public async Task TestGetDefaultLanguage()
    {
        var translations = await _service.GetDefaultResourceAsync<TranslationData>(NStackPlatform.Backend);

        Assert.AreEqual(_english.Default.Text, translations.Data.Default.Text);
    }

    [Test]
    public async Task TestGetDefaultLanguageDefaultType()
    {
        var translations = await _service.GetDefaultResourceAsync(NStackPlatform.Backend);

        Assert.AreEqual(_english.Default.Text, translations.Data["default"]["text"]);
    }

    [Test]
    public async Task TestGetLanguageWithLocale()
    {
        var translations = await _service.GetResourceAsync<TranslationData>("da-DK", NStackPlatform.Backend);

        Assert.AreEqual(_danish.Default.Text, translations.Data.Default.Text);
    }

    [Test]
    public async Task TestGetLanguageWithLocaleDefaultType()
    {
        var translations = await _service.GetResourceAsync("da-DK", NStackPlatform.Backend);

        Assert.AreEqual(_danish.Default.Text, translations.Data["default"]["text"]);
    }

    [TestCase(NStackPlatform.Backend, "backend")]
    [TestCase(NStackPlatform.Web, "web")]
    [TestCase(NStackPlatform.Mobile, "mobile")]
    public async Task TestCorrectUrl(NStackPlatform platform, string expectedTranslation)
    {
        await _service.GetLanguagesAsync(platform);

        _repository.Verify(r => r.DoRequestAsync<DataWrapper<List<ResourceData>>>(It.Is<RestRequest>(r => r.Resource.EndsWith(expectedTranslation)), It.IsAny<Action<HttpStatusCode>>()));
    }

    private Task<DataWrapper<List<ResourceData>>> GetLanguageMock()
    {
        return Task.FromResult(new DataWrapper<List<ResourceData>>
        {
            Data = new List<ResourceData>
            {
                _englishLanguage,
                _danishLanguage,
                _arabicLanguage
            }
        });
    }
}
