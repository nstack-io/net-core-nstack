namespace NStack.SDK.Services;

public interface INStackAppService
{
    /// <summary>
    /// Fetch meta data regarding localizations e.g. when the different localizations were last updated.
    /// </summary>
    /// <param name="platform">The platform to look for.</param>
    /// <param name="userId">The ID of the user to show the page for.</param>
    /// <param name="version">The version of the application asking.</param>
    /// <param name="environment">The name of the environment doing the request.</param>
    /// <param name="developmentEnvironment">true if the environment is a development environment.</param>
    /// <param name="productionEnvironment">true if the environment is a production environment.</param>
    /// <returns></returns>
    Task<DataAppOpenWrapper> AppOpenAsync<TSection>(NStackPlatform platform, Guid userId, string version, string environment = "production", bool developmentEnvironment = false, bool productionEnvironment = true) where TSection : ResourceItem, new();

    /// <summary>
    /// Fetch meta data regarding localizations e.g. when the different localizations were last updated.
    /// </summary>
    /// <param name="platform">The platform to look for.</param>
    /// <param name="userId">The ID of the user to show the page for.</param>
    /// <param name="version">The version of the application asking.</param>
    /// <param name="environment">The name of the environment doing the request.</param>
    /// <param name="developmentEnvironment">true if the environment is a development environment.</param>
    /// <param name="productionEnvironment">true if the environment is a production environment.</param>
    /// <returns></returns>
    Task<DataAppOpenWrapper> AppOpenAsync(NStackPlatform platform, Guid userId, string version, string environment = "production", bool developmentEnvironment = false, bool productionEnvironment = true);

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
    Task<DataMetaWrapper<TSection>> GetResourceAsync<TSection>(string locale, NStackPlatform platform, string version, string environment = "production", bool developmentEnvironment = false, bool productionEnvironment = true) where TSection : ResourceItem, new();

    /// <summary>
    /// Fetch translations for the given <paramref name="locale"/>.
    /// </summary>
    /// <param name="locale">The locale to fetch.</param>
    /// <param name="platform">The platform to fetch the resource for.</param>
    /// <param name="version">The version of the application asking.</param>
    /// <param name="environment">The name of the environment doing the request.</param>
    /// <param name="developmentEnvironment">true if the environment is a development environment.</param>
    /// <param name="productionEnvironment">true if the environment is a production environment.</param>
    /// <exception cref="ArgumentException"></exception>
    Task<DataMetaWrapper<ResourceItem>> GetResourceAsync(string locale, NStackPlatform platform, string version, string environment = "production", bool developmentEnvironment = false, bool productionEnvironment = true);

    /// <summary>
    /// Fetch the default translation.
    /// </summary>
    /// <typeparam name="TSection">The type of the translations.</typeparam>
    /// <param name="platform">The platform to fetch the resource for.</param>
    /// <param name="version">The version of the application asking.</param>
    /// <param name="environment">The name of the environment doing the request.</param>
    /// <param name="developmentEnvironment">true if the environment is a development environment.</param>
    /// <param name="productionEnvironment">true if the environment is a production environment.</param>
    Task<DataMetaWrapper<TSection>> GetDefaultResourceAsync<TSection>(NStackPlatform platform, string version, string environment = "production", bool developmentEnvironment = false, bool productionEnvironment = true) where TSection : ResourceItem, new();

    /// <summary>
    /// Fetch the default translation.
    /// </summary>
    /// <param name="platform">The platform to fetch the resource for.</param>
    /// <param name="version">The version of the application asking.</param>
    /// <param name="environment">The name of the environment doing the request.</param>
    /// <param name="developmentEnvironment">true if the environment is a development environment.</param>
    /// <param name="productionEnvironment">true if the environment is a production environment.</param>
    Task<DataMetaWrapper<ResourceItem>> GetDefaultResourceAsync(NStackPlatform platform, string version, string environment = "production", bool developmentEnvironment = false, bool productionEnvironment = true);
}
