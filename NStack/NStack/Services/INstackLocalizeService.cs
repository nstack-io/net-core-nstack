using NStack.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NStack.Services
{
    public interface INStackLocalizeService
    {
        /// <summary>
        /// Get a list of available languages on the given <paramref name="platform"/>.
        /// </summary>
        /// <param name="platform">The platform to fetch available languages from.</param>
        Task<DataWrapper<List<ResourceData>>> GetLanguages(NStackPlatform platform);

        /// <summary>
        /// Fetch translations for the given <paramref name="id"/>.
        /// </summary>
        /// <typeparam name="TSection">The type of the translations.</typeparam>
        /// <param name="id">The id of the translation to fetch.</param>
        Task<DataMetaWrapper<TSection>> GetResource<TSection>(int id) where TSection : ResourceItem;

        /// <summary>
        /// Fetch translations for the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id of the translation to fetch.</param>
        Task<DataMetaWrapper<ResourceItem>> GetResource(int id);
    }
}
