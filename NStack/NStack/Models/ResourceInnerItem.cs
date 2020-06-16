using System.Collections.Concurrent;

namespace NStack.Models
{
    public class ResourceInnerItem: ConcurrentDictionary<string, string>
    {
        public ResourceInnerItem() { }

        public ResourceInnerItem(ResourceInnerItem item)
        {
            foreach (var i in item)
            {
                TryAdd(i.Key, i.Value);
            }
        }
    }
}
