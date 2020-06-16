using NStack.Extensions;
using NStack.Models;

namespace NStack.Tests.Translations
{
    public class TranslationData : ResourceItem
    {
        public TranslationData() : base() { }
        public TranslationData(ResourceItem item) : base(item) { }

        public DefaultSection Default => new DefaultSection(this[nameof(Default).FirstCharToLower()]);
        public SecondSection SecondSection => new SecondSection(this[nameof(SecondSection).FirstCharToLower()]);
    }
}
