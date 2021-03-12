using NStack.SDK.Models;
using System;
using System.Threading.Tasks;

namespace NStack.SDK.Services
{
    public interface INStackAppService
    {
        /// <summary>
        /// Fetch meta data regarding localizations e.g. when the different localizations were last updated.
        /// </summary>
        /// <param name="platform">The platform to look for.</param>
        /// <param name="version">The version of the application asking.</param>
        /// <param name="environment">The name of the environment doing the request.</param>
        /// <param name="developmentEnvironment">true if the environment is a development environment.</param>
        /// <param name="productionEnvironment">true if the environment is a production environment.</param>
        /// <returns></returns>
        Task<DataAppOpenWrapper> AppOpen(NStackPlatform platform, string version, string environment = "production", bool developmentEnvironment = false, bool productionEnvironment = true);

        /// <summary>
        /// Fetch translations for the given <paramref name="locale"/>.
        /// </summary>
        /// <typeparam name="TSection">The type of the translations.</typeparam>
        /// <param name="locale">The locale to fetch.</param>
        /// <param name="platform">The platform to fetch the resource for.</param>
        /// <param name="version">The version of the application asking.</param>
        /// <param name="environment">The name of the environment doing the request.</param>
        /// <param name="developmentEnvironment">true if the environment is a development environment.</param>
        /// <param name="productionEnvironment">true if the environment is a production environment.</param>
        /// <exception cref="ArgumentException"></exception>
        Task<DataMetaWrapper<TSection>> GetResource<TSection>(string locale, NStackPlatform platform, string version, string environment = "production", bool developmentEnvironment = false, bool productionEnvironment = true) where TSection : ResourceItem;

        /// <summary>
        /// Fetch translations for the given <paramref name="locale"/>.
        /// </summary>
        /// <param name="id">The locale to fetch.</param>
        /// <param name="platform">The platform to fetch the resource for.</param>
        /// <param name="version">The version of the application asking.</param>
        /// <param name="environment">The name of the environment doing the request.</param>
        /// <param name="developmentEnvironment">true if the environment is a development environment.</param>
        /// <param name="productionEnvironment">true if the environment is a production environment.</param>
        /// <exception cref="ArgumentException"></exception>
        Task<DataMetaWrapper<ResourceItem>> GetResource(string locale, NStackPlatform platform, string version, string environment = "production", bool developmentEnvironment = false, bool productionEnvironment = true);

        /// <summary>
        /// Fetch the default translation.
        /// </summary>
        /// <typeparam name="TSection">The type of the translations.</typeparam>
        /// <param name="platform">The platform to fetch the resource for.</param>
        /// <param name="version">The version of the application asking.</param>
        /// <param name="environment">The name of the environment doing the request.</param>
        /// <param name="developmentEnvironment">true if the environment is a development environment.</param>
        /// <param name="productionEnvironment">true if the environment is a production environment.</param>
        Task<DataMetaWrapper<TSection>> GetDefaultResource<TSection>(NStackPlatform platform, string version, string environment = "production", bool developmentEnvironment = false, bool productionEnvironment = true) where TSection : ResourceItem;

        /// <summary>
        /// Fetch the default translation.
        /// </summary>
        /// <param name="platform">The platform to fetch the resource for.</param>
        /// <param name="version">The version of the application asking.</param>
        /// <param name="environment">The name of the environment doing the request.</param>
        /// <param name="developmentEnvironment">true if the environment is a development environment.</param>
        /// <param name="productionEnvironment">true if the environment is a production environment.</param>
        Task<DataMetaWrapper<ResourceItem>> GetDefaultResource(NStackPlatform platform, string version, string environment = "production", bool developmentEnvironment = false, bool productionEnvironment = true);
    }
}
