using NStack.SDK.Models;
using NStack.SDK.Repositories.Implementation;
using NStack.SDK.Services.Implementation;
using NUnit.Framework;
using System;

namespace NStack.Tests
{
    // Used for testing connection to NStack
    public class NStackServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestConnection()
        {
            Assert.Pass();
            var service = new NStackLocalizeService(new NStackRepository(Model));
            try
            {
                var temp = service.GetLanguages(NStackPlatform.Backend).Result;
                var something = temp.Data;
            }
            catch(Exception)
            {
            }
            
            Assert.IsTrue(true);
        }

        [Test]
        public void TestResources()
        {
            Assert.Pass();
            var service = new NStackLocalizeService(new NStackRepository(Model));
            try
            {
                var temp = service.GetResource(1208).Result;
                var something = temp.Data;
            }
            catch (Exception)
            {
            }

            Assert.IsTrue(true);
        }

        private NStackConfiguration Model
        {
            get
            {
                return new NStackConfiguration
                {
                };
            }
        }
    }
}
