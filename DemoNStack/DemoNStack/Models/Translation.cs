namespace DemoNStack.Models;

public class Translation : ResourceItem
{
    public Translation() : base() { }
    public Translation(ResourceItem item) : base(item) { }

    public DefaultSection Default => new DefaultSection(this[nameof(Default).FirstCharToLower()]);
    public TermsSection Terms => new TermsSection(this[nameof(Terms).FirstCharToLower()]);
}
