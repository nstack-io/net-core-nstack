using NUnit.Framework;
using NStack.Extensions;

namespace NStack.Tests
{
    public class NStackStringExtensionsTests
    {
        [TestCase("PascalCase", "pascalCase")]
        [TestCase("camelCase", "camelCase")]
        public void FirstCharToLowerTest(string input, string expectedOutput)
        {
            var output = input.FirstCharToLower();

            Assert.AreEqual(expectedOutput, output);
        }

        [TestCase("1", true)]
        [TestCase("true", false)]
        [TestCase("false", false)]
        [TestCase("__boolValueFalse", false)]
        public void TranslateToBoolTest(string input, bool expectedOutput)
        {
            bool output = input.TranslateToBool();

            Assert.AreEqual(expectedOutput, output);
        }
    }
}
