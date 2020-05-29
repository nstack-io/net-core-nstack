using System;
using System.Collections.Generic;
using System.Text;

namespace NStack.Models
{
    public class ResourceData
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public bool ShouldUpdate { get; set; }
        public Language Language { get; set; }
    }
}
