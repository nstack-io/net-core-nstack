namespace NStack.Tests.Translations;

public class DefaultSection : ResourceInnerItem
{
    public DefaultSection() : base() { }
    public DefaultSection(ResourceInnerItem item) : base(item) { }

    public string Text => this[nameof(Text).FirstCharToLower()];
}
