using NStack.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NStack.Services
{
    public interface INStackLocalizeService
    {
        Task<DataWrapper<List<ResourceData>>> GetLanguages(NStackPlatform platform);

        Task<DataMetaWrapper<TSection>> GetResource<TSection>(int id) where TSection : ResourceItem;
    }
}
