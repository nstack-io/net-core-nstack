namespace NStack.SDK.Models;

public class DataMetaWrapper<T> where T : new()
{
    public T Data { get; set; } = new T();
    public MetaData Meta { get; set; } = new MetaData();
}
