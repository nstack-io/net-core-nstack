using NStack.SDK.Extensions;
using NStack.SDK.Models;

namespace DemoNStack.Models
{
    public class SecondSectionSection : ResourceInnerItem
    {
        public SecondSectionSection() : base() { }
        public SecondSectionSection(ResourceInnerItem item) : base(item) { }

        public string First => this[nameof(First).FirstCharToLower()];
        public string Second => this[nameof(Second).FirstCharToLower()];
    }
}
