namespace NStack.SDK.Repositories.Implementation;

public class NStackRepository : INStackRepository
{
    protected readonly RestClient Client;

    public NStackRepository(NStackConfiguration configuration)
    {
        if (configuration == null)
            throw new ArgumentNullException(nameof(configuration));

        if (string.IsNullOrWhiteSpace(configuration.ApiKey))
            throw new ArgumentNullException(configuration.ApiKey);

        if (string.IsNullOrWhiteSpace(configuration.ApplicationId))
            throw new ArgumentNullException(configuration.ApplicationId);

        if (string.IsNullOrWhiteSpace(configuration.BaseUrl))
            throw new ArgumentNullException(configuration.BaseUrl);

        var options = new RestClientOptions(configuration.BaseUrl);

        Client = new RestClient(options);

        Client.AddDefaultHeader("X-Application-Id", configuration.ApplicationId);
        Client.AddDefaultHeaders(new Dictionary<string, string>
        {
            { "X-Application-Id", configuration.ApplicationId },
            { "X-Rest-Api-Key", configuration.ApiKey }
        });
    }

    async Task<T> INStackRepository.DoRequestAsync<T>(RestRequest request, Action<HttpStatusCode>? errorHandling)
    {
        var resp = await Client.ExecuteAsync<T>(request);
        var code = (int)resp.StatusCode;
        if (code > 299 || code < 200)
        {
            if(errorHandling != null)
                errorHandling.Invoke(resp.StatusCode);
            return new T();
        }

        return resp.Data ?? new T();
    }
}
