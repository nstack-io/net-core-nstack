using Moq;
using NStack.SDK.Models;
using NStack.SDK.Repositories;
using NStack.SDK.Repositories.Implementation;
using NStack.SDK.Services.Implementation;
using NUnit.Framework;
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
            _repository = new Mock<INStackRepository>();

            var repository = new NStackRepository(new NStackConfiguration
            {
                ApiKey = "qd1GiPnq8sJuChbFxjOQxV9t1AN71oIMBuWF",
                ApplicationId = "9vJhjXzSBUxBOuQx2B2mFIZSoa2aK4UJzt7y"
            });

            _service = new NStackTermsService(repository);
        }

        [Test]
        public async Task RemoveMeGetAll()
        {
            var temp = await _service.GetAllTerms("en-GB");
            var tempDa = await _service.GetAllTerms("da-DK");
            var tempAr = await _service.GetAllTerms("ar-QA");
        }

        [Test]
        public async Task RemoveMeGetVersions()
        {
            var temp = await _service.GetTermsVersions("terms", "1", "en-GB");
            var tempDa = await _service.GetTermsVersions("terms", "1", "da-DK");
            var tempAr = await _service.GetTermsVersions("terms", "1", "ar-QA");
        }

        [Test]
        public async Task RemoveMeGetNewest()
        {
            var temp = await _service.GetNewestTerms("terms", "1", "en-GB");
            var tempDa = await _service.GetNewestTerms("terms", "1", "da-DK");
            var tempAr = await _service.GetNewestTerms("terms", "1", "ar-QA");
        }

        [Test]
        public async Task RemoveMeGetTerms()
        {
            var temp = await _service.GetTerms(11, "1", "en-GB");
            var tempDa = await _service.GetTerms(11, "1", "da-DK");
            var tempAr = await _service.GetTerms(11, "1", "ar-QA");
        }

        [Test]
        public async Task RemoveMeMarkRead()
        {
            var temp = await _service.MarkRead(11, "1", "en-GB");

            var temp2 = await _service.GetTerms(11, "1", "en-GB");
            //var tempDa = await _service.GetTerms(11, "1", "da-DK");
            //var tempAr = await _service.GetTerms(11, "1", "ar-QA");
        }
    }
}
