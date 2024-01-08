using Smooth.Client.Application.HttpClients;
using Smooth.Shared.Endpoints;
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


    public async Task<T?> GetDataAsync<T>(string endpoint, CancellationToken cancellationToken, bool usePublicHttpClient = false)
    {
        if (usePublicHttpClient)
        {
            return await _publicHttpClient.Client.GetFromJsonAsync<T>(endpoint, cancellationToken);
        }

        return await _apiHttpClient!.Client!.GetFromJsonAsync<T>(endpoint, cancellationToken);
    }


    public async Task<string?> GetSerializedDataAsync<T>(string endpoint, CancellationToken cancellationToken, bool usePublicHttpClient = false)
    {
        var result = await GetDataAsync<T>(endpoint, cancellationToken, usePublicHttpClient);

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


    public async Task<T?> Insert<T, U>(string endpoint, U data)
        where T : class
        where U : class
    {
        var response = await _apiHttpClient.Client.PostAsJsonAsync(endpoint, data);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<T>();
            return content;
        }

        return null;
    }
}
