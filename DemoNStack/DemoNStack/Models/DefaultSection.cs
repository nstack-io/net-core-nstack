namespace DemoNStack.Models;

public class DefaultSection : ResourceInnerItem
{
    public DefaultSection() : base() { }
    public DefaultSection(ResourceInnerItem item) : base(item) { }

    public string BoolValue => this[nameof(BoolValue).FirstCharToLower()];
    public string BoolValueFalse => this[nameof(BoolValueFalse).FirstCharToLower()];
    public string Html => this[nameof(Html).FirstCharToLower()];
    public string Language => this[nameof(Language).FirstCharToLower()];
    public string Text => this[nameof(Text).FirstCharToLower()];
    public string FirstHeadline => this[nameof(FirstHeadline).FirstCharToLower()];
    public string SecondHeadline => this[nameof(SecondHeadline).FirstCharToLower()];
}
