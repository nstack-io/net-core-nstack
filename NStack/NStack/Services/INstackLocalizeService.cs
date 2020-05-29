using NStack.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NStack.Services
{
    public interface INstackLocalizeService<TSection> where TSection : ResourceItem
    {
        Task<DataWrapper<List<ResourceData>>> GetLanguages();

        Task<DataMetaWrapper<TSection>> GetResource(int id);
    }
}
