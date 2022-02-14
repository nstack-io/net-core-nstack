namespace NStack.Tests.Translations;

public class TranslationData : ResourceItem
{
    public TranslationData() : base() { }
    public TranslationData(ResourceItem item) : base(item) { }

    public DefaultSection Default => new DefaultSection(this[nameof(Default).FirstCharToLower()]);
}
