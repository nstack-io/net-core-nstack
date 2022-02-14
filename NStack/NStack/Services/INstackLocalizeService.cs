namespace NStack.SDK.Services;

public interface INStackLocalizeService
{
    /// <summary>
    /// Get a list of available languages on the given <paramref name="platform"/>.
    /// </summary>
    /// <param name="platform">The platform to fetch available languages from.</param>
    Task<DataWrapper<List<ResourceData>>> GetLanguagesAsync(NStackPlatform platform);

    /// <summary>
    /// Fetch the default translation.
    /// </summary>
    /// <typeparam name="TSection">The type of the translations.</typeparam>
    /// <param name="platform">The platform to fetch the resource for.</param>
    Task<DataMetaWrapper<TSection>> GetDefaultResourceAsync<TSection>(NStackPlatform platform) where TSection : ResourceItem, new();

    /// <summary>
    /// Fetch the default translation.
    /// </summary>
    /// <param name="platform">The platform to fetch the resource for.</param>
    Task<DataMetaWrapper<ResourceItem>> GetDefaultResourceAsync(NStackPlatform platform);

    /// <summary>
    /// Fetch translations for the given <paramref name="id"/>.
    /// </summary>
    /// <typeparam name="TSection">The type of the translations.</typeparam>
    /// <param name="id">The id of the translation to fetch.</param>
    Task<DataMetaWrapper<TSection>> GetResourceAsync<TSection>(int id) where TSection : ResourceItem, new();

    /// <summary>
    /// Fetch translations for the given <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The id of the translation to fetch.</param>
    Task<DataMetaWrapper<ResourceItem>> GetResourceAsync(int id);

    /// <summary>
    /// Fetch translations for the given <paramref name="locale"/>.
    /// </summary>
    /// <typeparam name="TSection">The type of the translations.</typeparam>
    /// <param name="locale">The locale to fetch.</param>
    /// <param name="platform">The platform to fetch the resource for.</param>
    /// <exception cref="ArgumentNullException"></exception>
    Task<DataMetaWrapper<TSection>> GetResourceAsync<TSection>(string locale, NStackPlatform platform) where TSection : ResourceItem, new();

    /// <summary>
    /// Fetch translations for the given <paramref name="locale"/>.
    /// </summary>
    /// <param name="id">The locale to fetch.</param>
    /// <param name="platform">The platform to fetch the resource for.</param>
    /// <exception cref="ArgumentNullException"></exception>
    Task<DataMetaWrapper<ResourceItem>> GetResourceAsync(string locale, NStackPlatform platform);
}
