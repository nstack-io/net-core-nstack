using NStack.Extensions;

namespace NStack.Tests.Translations
{
    public class SecondSection : ResourceInnerItem
    {
        public SecondSection() : base() { }
        public SecondSection(ResourceInnerItem item) : base(item) { }

        public string First => this[nameof(First).FirstCharToLower()];
        public string Second => this[nameof(Second).FirstCharToLower()];
    }
}
