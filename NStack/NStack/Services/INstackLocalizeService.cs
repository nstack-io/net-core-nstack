using NStack.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NStack.Services
{
    public interface INStackLocalizeService<TSection> where TSection : ResourceItem
    {
        Task<DataWrapper<List<ResourceData>>> GetLanguages(NStackPlatform platform);

        Task<DataMetaWrapper<TSection>> GetResource(int id);
    }
}
