using System;
using System.Collections.Generic;
using System.Text;

namespace NStack.Models
{
    public class DataMetaWrapper<T>
    {
        public T Data { get; set; }
        public MetaData Meta { get; set; }
    }
}
