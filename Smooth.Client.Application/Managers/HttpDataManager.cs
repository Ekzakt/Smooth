using Smooth.Client.Application.HttpClients;
using System.Net.Http.Json;
using System.Text.Json;

namespace Smooth.Client.Application.Managers;

public class HttpDataManager : IHttpDataManager
{
    private readonly ApiHttpClient _apiHttpClient;
    private readonly PublicHttpClient _publicHttpClient;


    public HttpDataManager(ApiHttpClient apiHttpClient,  PublicHttpClient publicHttpClient)
    {
        _apiHttpClient = apiHttpClient;
        _publicHttpClient = publicHttpClient;
    }


    public async Task<T?> GetDataAsync<T>(string endpoint, bool usePublicHttpClient = false, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = usePublicHttpClient
                ? await _publicHttpClient.Client.GetFromJsonAsync<T>(endpoint, cancellationToken)
                : await _apiHttpClient.Client.GetFromJsonAsync<T>(endpoint, cancellationToken);

            return response;

        }
        catch (Exception ex)
        {
            throw;
        }
    }


    public async Task<string?> GetSerializedDataAsync<T>(string endpoint, bool usePublicHttpClient = false, CancellationToken cancellationToken = default)
    {
        var result = await GetDataAsync<T>(endpoint, usePublicHttpClient, cancellationToken);

        if (result is not null)
        {
            var output = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                IgnoreReadOnlyFields = false,
                WriteIndented = true
            });

            return output.ToString();
        }

        return string.Empty;
    }


    public async Task<T?> Insert<T, U>(string endpoint, U data, bool usePublicHttpClient = false, CancellationToken cancellationToken = default)
        where T : class
        where U : class
    {
        var response = usePublicHttpClient
            ? await _publicHttpClient.Client.PostAsJsonAsync(endpoint, data, cancellationToken)
            : await _apiHttpClient.Client.PostAsJsonAsync(endpoint, data, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<T>();
            return content;
        }

        throw new Exception(response.ReasonPhrase);

    }
}
