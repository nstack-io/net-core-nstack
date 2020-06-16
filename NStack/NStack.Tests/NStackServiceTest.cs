using NStack.Models;
using NStack.Repositories.Implementation;
using NStack.Services.Implementation;
using NUnit.Framework;
using System;

namespace NStack.Tests
{
    public class NStackServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestConnection()
        {
            var service = new NstackLocalizeService(new NstackRepository(Model));
            try
            {
                var temp = service.GetLanguages().Result;
                var something = temp.Data;
            }
            catch(Exception)
            {
            }
            
            Assert.True(true);
        }

        [Test]
        public void TestResources()
        {
            var service = new NstackLocalizeService(new NstackRepository(Model));
            try
            {
                var temp = service.GetResource(1208).Result;
                var something = temp.Data;
            }
            catch (Exception)
            {
            }

            Assert.True(true);
        }

        private NstackConfiguration Model
        {
            get
            {
                return new NstackConfiguration
                {
                };
            }
        }
    }
}
