namespace NStack.SDK.Tests;

public class NStackTermsServiceTests
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Mock<INStackRepository> _repository;
    private NStackTermsService _service;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [SetUp]
    public void Initialize()
    {
        _repository = new Mock<INStackRepository>(MockBehavior.Loose);

        _service = new NStackTermsService(_repository.Object);
    }

    [Test]
    public void GetAllTermsAsyncLanguageIsNullThrowsException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetAllTermsAsync(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        Assert.AreEqual("language", exception?.ParamName);
    }

    [Test]
    public void GetAllTermsAsyncLanguageIsEmptyThrowsException()
    {
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetAllTermsAsync(string.Empty));

        Assert.AreEqual("language", exception?.ParamName);
    }

    [Test]
    public void GetTermsVersionsAsyncTermsIdNullThrowsException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsVersionsAsync(null, "userId", "language"));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        Assert.AreEqual("termsId", exception?.ParamName);
    }

    [Test]
    public void GetTermsVersionsAsyncTermsIdEmptyThrowsException()
    {
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsVersionsAsync(string.Empty, "userId", "language"));

        Assert.AreEqual("termsId", exception?.ParamName);
    }

    [Test]
    public void GetTermsVersionsAsyncUserIdNullThrowsException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsVersionsAsync("termsId", null, "language"));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        Assert.AreEqual("userId", exception?.ParamName);
    }

    [Test]
    public void GetTermsVersionsAsyncUserIdEmptyThrowsException()
    {
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsVersionsAsync("termsId", string.Empty, "language"));

        Assert.AreEqual("userId", exception?.ParamName);
    }

    [Test]
    public void GetTermsVersionsAsyncLanguageNullThrowsException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsVersionsAsync("termsId", "userId", null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        Assert.AreEqual("language", exception?.ParamName);
    }

    [Test]
    public void GetTermsVersionsAsyncLanguageEmptyThrowsException()
    {
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsVersionsAsync("termsId", "userId", string.Empty));

        Assert.AreEqual("language", exception?.ParamName);
    }

    [Test]
    public void GetNewestTermsAsyncTermsIdNullThrowsException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetNewestTermsAsync(null, "userId", "language"));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        Assert.AreEqual("termsId", exception?.ParamName);
    }

    [Test]
    public void GetNewestTermsAsyncTermsIdEmptyThrowsException()
    {
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetNewestTermsAsync(string.Empty, "userId", "language"));

        Assert.AreEqual("termsId", exception?.ParamName);
    }

    [Test]
    public void GetNewestTermsAsyncUserIdNullThrowsException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetNewestTermsAsync("termsId", null, "language"));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        Assert.AreEqual("userId", exception?.ParamName);
    }

    [Test]
    public void GetNewestTermsAsyncUserIdEmptyThrowsException()
    {
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetNewestTermsAsync("termsId", string.Empty, "language"));

        Assert.AreEqual("userId", exception?.ParamName);
    }

    [Test]
    public void GetNewestTermsAsyncLanguageNullThrowsException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetNewestTermsAsync("termsId", "userId", null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        Assert.AreEqual("language", exception?.ParamName);
    }

    [Test]
    public void GetNewestTermsAsyncLanguageEmptyThrowsException()
    {
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetNewestTermsAsync("termsId", "userId", string.Empty));

        Assert.AreEqual("language", exception?.ParamName);
    }

    [Test]
    public void GetTermsAsyncTermsIdTooLowThrowsException()
    {
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsAsync(-42, "userId", "language"));

        Assert.AreEqual("termsId", exception?.ParamName);
    }

    [Test]
    public void GetTermsAsyncUserIdNullThrowsException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsAsync(42, null, "language"));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        Assert.AreEqual("userId", exception?.ParamName);
    }

    [Test]
    public void GetTermsAsyncUserIdEmptyThrowsException()
    {
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsAsync(42, string.Empty, "language"));

        Assert.AreEqual("userId", exception?.ParamName);
    }

    [Test]
    public void GetTermsAsyncLanguageNullThrowsException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsAsync(42, "userId", null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        Assert.AreEqual("language", exception?.ParamName);
    }

    [Test]
    public void GetTermsAsyncLanguageEmptyThrowsException()
    {
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsAsync(42, "userId", string.Empty));

        Assert.AreEqual("language", exception?.ParamName);
    }

    [Test]
    public void MarkReadAsyncTermsIdTooLowThrowsException()
    {
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.MarkReadAsync(-42, "userId", "language"));

        Assert.AreEqual("termsId", exception?.ParamName);
    }

    [Test]
    public void MarkReadAsyncUserIdNullThrowsException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.MarkReadAsync(42, null, "language"));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        Assert.AreEqual("userId", exception?.ParamName);
    }

    [Test]
    public void MarkReadAsyncUserIdEmptyThrowsException()
    {
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.MarkReadAsync(42, string.Empty, "language"));

        Assert.AreEqual("userId", exception?.ParamName);
    }

    [Test]
    public void MarkReadAsyncLanguageNullThrowsException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.MarkReadAsync(42, "userId", null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        Assert.AreEqual("language", exception?.ParamName);
    }

    [Test]
    public void MarkReadAsyncLanguageEmptyThrowsException()
    {
        ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(() => _service.MarkReadAsync(42, "userId", string.Empty));

        Assert.AreEqual("language", exception?.ParamName);
    }
}
