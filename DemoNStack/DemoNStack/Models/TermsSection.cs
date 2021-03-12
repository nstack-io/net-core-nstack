using NStack.SDK.Extensions;
using NStack.SDK.Models;

namespace DemoNStack.Models
{
    public class TermsSection : ResourceInnerItem
    {
        public TermsSection() : base() { }
        public TermsSection(ResourceInnerItem item) : base(item) { }

        public string Approve => this[nameof(Approve).FirstCharToLower()];
        public string HasApproved => this[nameof(HasApproved).FirstCharToLower()];
        public string Headline => this[nameof(Headline).FirstCharToLower()];
    }
}
