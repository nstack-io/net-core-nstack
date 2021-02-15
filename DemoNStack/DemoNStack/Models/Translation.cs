using NStack.SDK.Extensions;
using NStack.SDK.Models;

namespace DemoNStack.Models
{
    public class Translation : ResourceItem
    {
        public Translation() : base() { }
        public Translation(ResourceItem item) : base(item) { }

        public DefaultSection Default => new DefaultSection(this[nameof(Default).FirstCharToLower()]);
        public SecondSectionSection SecondSection => new SecondSectionSection(this[nameof(SecondSection).FirstCharToLower()]);
        public TermsSection Terms => new TermsSection(this[nameof(Terms).FirstCharToLower()]);
    }
}
