namespace NStack.SDK.Tests;

public class TermsTests
{
    [Test]
    public void PublishedAtTranslationSuccess()
    {
        var terms = new Terms
        {
            PublishedAtString = "2021-01-22 07:50:40"
        };

        var publishedAt = terms.PublishedAt;

        Assert.AreEqual(2021, publishedAt.Year);
        Assert.AreEqual(1, publishedAt.Month);
        Assert.AreEqual(22, publishedAt.Day);
        Assert.AreEqual(7, publishedAt.Hour);
        Assert.AreEqual(50, publishedAt.Minute);
        Assert.AreEqual(40, publishedAt.Second);
    }

    [Test]
    public void PublishedAtTranslationOnNullReturnsDefault()
    {
        var terms = new Terms
        {
            PublishedAtString = null
        };

        var publishedAt = terms.PublishedAt;

        Assert.AreEqual(default(DateTime), publishedAt);
    }

    [Test]
    public void PublishedAtTranslationOnInvalidStringReturnsDefault()
    {
        var terms = new Terms
        {
            PublishedAtString = "I'm not a valid date string"
        };

        var publishedAt = terms.PublishedAt;

        Assert.AreEqual(default(DateTime), publishedAt);
    }
}
