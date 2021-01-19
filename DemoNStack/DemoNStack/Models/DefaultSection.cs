using NStack.SDK.Extensions;
using NStack.SDK.Models;

namespace DemoNStack.Models
{
    public class DefaultSection : ResourceInnerItem
    {
        public DefaultSection() : base() { }
        public DefaultSection(ResourceInnerItem item) : base(item) { }

        public string Text => this[nameof(Text).FirstCharToLower()];
        public string Html => this[nameof(Html).FirstCharToLower()];
        public string BoolValue => this[nameof(BoolValue).FirstCharToLower()];
        public string BoolValueFalse => this[nameof(BoolValueFalse).FirstCharToLower()];
    }
}
