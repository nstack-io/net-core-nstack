namespace NStackTranslationGenerator;

internal class Repository : NStackRepository
{
    internal Repository(NStackConfiguration configuration) : base(configuration)
    {
    }

    internal async Task<string> DoRequest(RestRequest request)
    {
        var resp = await Client.ExecuteAsync(request);
        var code = (int)resp.StatusCode;
        if (code > 299 || code < 200)
        {
            return string.Empty;
        }

        return resp.Content ?? string.Empty;
    }
}
