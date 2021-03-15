using Moq;
using NStack.SDK.Models;
using NStack.SDK.Repositories;
using NStack.SDK.Repositories.Implementation;
using NStack.SDK.Services.Implementation;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

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
            ArgumentNullException exception = Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetAllTermsAsync(null));

            Assert.AreEqual("language", exception.ParamName);
        }

        [Test]
        public void GetTermsVersionsAsyncTermsIdNullThrowsException()
        {
            ArgumentNullException exception = Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetTermsVersionsAsync(null, "userId", "language"));

            Assert.AreEqual("termsId", exception.ParamName);
        }

        [Test]
        public void GetTermsVersionsAsyncUserIdNullThrowsException()
        {
            ArgumentNullException exception = Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetTermsVersionsAsync("termsId", null, "language"));

            Assert.AreEqual("userId", exception.ParamName);
        }

        [Test]
        public void GetTermsVersionsAsyncLanguageNullThrowsException()
        {
            ArgumentNullException exception = Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetTermsVersionsAsync("termsId", "userId", null));

            Assert.AreEqual("language", exception.ParamName);
        }

        [Test]
        public void GetNewestTermsAsyncTermsIdNullThrowsException()
        {
            ArgumentNullException exception = Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetNewestTermsAsync(null, "userId", "language"));

            Assert.AreEqual("termsId", exception.ParamName);
        }

        [Test]
        public void GetNewestTermsAsyncUserIdNullThrowsException()
        {
            ArgumentNullException exception = Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetNewestTermsAsync("termsId", null, "language"));

            Assert.AreEqual("userId", exception.ParamName);
        }

        [Test]
        public void GetNewestTermsAsyncLanguageNullThrowsException()
        {
            ArgumentNullException exception = Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetNewestTermsAsync("termsId", "userId", null));

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
            ArgumentNullException exception = Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetTermsAsync(42, null, "language"));

            Assert.AreEqual("userId", exception.ParamName);
        }

        [Test]
        public void GetTermsAsyncLanguageNullThrowsException()
        {
            ArgumentNullException exception = Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetTermsAsync(42, "userId", null));

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
            ArgumentNullException exception = Assert.ThrowsAsync<ArgumentNullException>(() => _service.MarkReadAsync(42, null, "language"));

            Assert.AreEqual("userId", exception.ParamName);
        }

        [Test]
        public void MarkReadAsyncLanguageNullThrowsException()
        {
            ArgumentNullException exception = Assert.ThrowsAsync<ArgumentNullException>(() => _service.MarkReadAsync(42, "userId", null));

            Assert.AreEqual("language", exception.ParamName);
        }
    }
}
