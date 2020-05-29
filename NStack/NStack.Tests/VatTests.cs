using NUnit.Framework;

namespace NStack.Tests
{
    public class VatTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TwoPlusTwoIsFiveWithVat()
        {
            int first = 2;
            int second = 2;
            double vat = 0.25;

            int price = (int)((first + second) * (1 + vat));

            Assert.AreEqual(5, price);
        }
    }
}