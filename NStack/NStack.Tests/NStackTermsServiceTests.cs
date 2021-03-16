using Moq;
using NStack.SDK.Repositories;
using NStack.SDK.Services.Implementation;
using NUnit.Framework;
using System;

namespace NStack.SDK.Tests
{
    public class NStackTermsServiceTests
    {
        private Mock<INStackRepository> _repository;
        private NStackTermsService _service;

        [SetUp]
        public void Initialize()
        {
            _repository = new Mock<INStackRepository>(MockBehavior.Loose);

            _service = new NStackTermsService(_repository.Object);
        }

        [Test]
        public void GetAllTermsAsyncLanguageIsNullThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetAllTermsAsync(null));

            Assert.AreEqual("language", exception.ParamName);
        }

        [Test]
        public void GetAllTermsAsyncLanguageIsEmptyThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetAllTermsAsync(string.Empty));

            Assert.AreEqual("language", exception.ParamName);
        }

        [Test]
        public void GetTermsVersionsAsyncTermsIdNullThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsVersionsAsync(null, "userId", "language"));

            Assert.AreEqual("termsId", exception.ParamName);
        }

        [Test]
        public void GetTermsVersionsAsyncTermsIdEmptyThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsVersionsAsync(string.Empty, "userId", "language"));

            Assert.AreEqual("termsId", exception.ParamName);
        }

        [Test]
        public void GetTermsVersionsAsyncUserIdNullThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsVersionsAsync("termsId", null, "language"));

            Assert.AreEqual("userId", exception.ParamName);
        }

        [Test]
        public void GetTermsVersionsAsyncUserIdEmptyThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsVersionsAsync("termsId", string.Empty, "language"));

            Assert.AreEqual("userId", exception.ParamName);
        }

        [Test]
        public void GetTermsVersionsAsyncLanguageNullThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsVersionsAsync("termsId", "userId", null));

            Assert.AreEqual("language", exception.ParamName);
        }

        [Test]
        public void GetTermsVersionsAsyncLanguageEmptyThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsVersionsAsync("termsId", "userId", string.Empty));

            Assert.AreEqual("language", exception.ParamName);
        }

        [Test]
        public void GetNewestTermsAsyncTermsIdNullThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetNewestTermsAsync(null, "userId", "language"));

            Assert.AreEqual("termsId", exception.ParamName);
        }

        [Test]
        public void GetNewestTermsAsyncTermsIdEmptyThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetNewestTermsAsync(string.Empty, "userId", "language"));

            Assert.AreEqual("termsId", exception.ParamName);
        }

        [Test]
        public void GetNewestTermsAsyncUserIdNullThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetNewestTermsAsync("termsId", null, "language"));

            Assert.AreEqual("userId", exception.ParamName);
        }

        [Test]
        public void GetNewestTermsAsyncUserIdEmptyThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetNewestTermsAsync("termsId", string.Empty, "language"));

            Assert.AreEqual("userId", exception.ParamName);
        }

        [Test]
        public void GetNewestTermsAsyncLanguageNullThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetNewestTermsAsync("termsId", "userId", null));

            Assert.AreEqual("language", exception.ParamName);
        }

        [Test]
        public void GetNewestTermsAsyncLanguageEmptyThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetNewestTermsAsync("termsId", "userId", string.Empty));

            Assert.AreEqual("language", exception.ParamName);
        }

        [Test]
        public void GetTermsAsyncTermsIdTooLowThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsAsync(-42, "userId", "language"));

            Assert.AreEqual("termsId", exception.ParamName);
        }

        [Test]
        public void GetTermsAsyncUserIdNullThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsAsync(42, null, "language"));

            Assert.AreEqual("userId", exception.ParamName);
        }

        [Test]
        public void GetTermsAsyncUserIdEmptyThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsAsync(42, string.Empty, "language"));

            Assert.AreEqual("userId", exception.ParamName);
        }

        [Test]
        public void GetTermsAsyncLanguageNullThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsAsync(42, "userId", null));

            Assert.AreEqual("language", exception.ParamName);
        }

        [Test]
        public void GetTermsAsyncLanguageEmptyThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTermsAsync(42, "userId", string.Empty));

            Assert.AreEqual("language", exception.ParamName);
        }

        [Test]
        public void MarkReadAsyncTermsIdTooLowThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.MarkReadAsync(-42, "userId", "language"));

            Assert.AreEqual("termsId", exception.ParamName);
        }

        [Test]
        public void MarkReadAsyncUserIdNullThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.MarkReadAsync(42, null, "language"));

            Assert.AreEqual("userId", exception.ParamName);
        }

        [Test]
        public void MarkReadAsyncUserIdEmptyThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.MarkReadAsync(42, string.Empty, "language"));

            Assert.AreEqual("userId", exception.ParamName);
        }

        [Test]
        public void MarkReadAsyncLanguageNullThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.MarkReadAsync(42, "userId", null));

            Assert.AreEqual("language", exception.ParamName);
        }

        [Test]
        public void MarkReadAsyncLanguageEmptyThrowsException()
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(() => _service.MarkReadAsync(42, "userId", string.Empty));

            Assert.AreEqual("language", exception.ParamName);
        }
    }
}
