using Moq;
using NStack.SDK.Models;
using NStack.SDK.Repositories;
using NStack.SDK.Services.Implementation;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NStack.Tests
{

    public class NStackLocalizeServiceTests
    {
        private NStackLocalizeService _service;
        private Mock<INStackRepository> _repository;

        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<INStackRepository>
            {
                DefaultValue = DefaultValue.Empty
            };

            _service = new NStackLocalizeService(_repository.Object);
        }

        [TestCase(NStackPlatform.Backend, "backend")]
        [TestCase(NStackPlatform.Web, "web")]
        [TestCase(NStackPlatform.Mobile, "mobile")]
        public async Task TestCorrectUrl(NStackPlatform platform, string expectedTranslation)
        {
            await _service.GetLanguages(platform);

            _repository.Verify(r => r.DoRequest<DataWrapper<List<ResourceData>>>(It.Is<IRestRequest>(r => r.Resource.EndsWith(expectedTranslation)), It.IsAny<Action<HttpStatusCode>>()));
        }
    }
}
