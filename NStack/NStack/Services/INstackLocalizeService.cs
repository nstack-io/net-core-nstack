using NStack.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NStack.Services
{
    public interface INstackLocalizeService<TSection> where TSection : ResourceItem
    {
        Task<DataWrapper<List<ResourceData>>> GetLanguages();

        Task<DataMetaWrapper<TSection>> GetResource(int id);
    }
}
