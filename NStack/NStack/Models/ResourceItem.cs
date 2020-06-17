using System.Collections.Concurrent;

namespace NStack.SDK.Models
{
    public class ResourceItem: ConcurrentDictionary<string, ResourceInnerItem>
    {
        public ResourceItem() { }
        public ResourceItem(ResourceItem item)
        {
            foreach (var i in item)
            {
                TryAdd(i.Key, i.Value);
            }
        }
    }
}
