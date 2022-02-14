namespace NStack.SDK.Models;

public class DataWrapper<T> where T : new()
{
    public T Data { get; set; } = new T();
}
