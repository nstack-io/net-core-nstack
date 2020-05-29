using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace NStack.Models
{
    public class ResourceData<T>: ConcurrentDictionary<string, T> where T: ResourceItem
    {
        
    }
}
