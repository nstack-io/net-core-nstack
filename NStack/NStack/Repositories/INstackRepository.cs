namespace NStack.SDK.Repositories;

public interface INStackRepository
{
    /// <summary>
    /// Perform a request to the NStack backend.
    /// </summary>
    /// <typeparam name="T">The expected type to get from NStack from this request.</typeparam>
    /// <param name="request">The request to NStack.</param>
    /// <param name="errorHandling">Custom error handling.</param>
    /// <returns></returns>
    internal Task<T> DoRequestAsync<T>(RestRequest request, Action<HttpStatusCode>? errorHandling = null) where T : class, new();
}
