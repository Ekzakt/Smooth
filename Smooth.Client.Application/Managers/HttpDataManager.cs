using Smooth.Client.Application.HttpClients;
using System.Net.Http.Json;
using System.Text.Json;

namespace Smooth.Client.Application.Managers;

public class HttpDataManager : IHttpDataManager
{
    private readonly SecureHttpClient _secureHttpClient;
    private readonly PublicHttpClient _publicHttpClient;


    public HttpDataManager(SecureHttpClient secureHttpClient,  PublicHttpClient publicHttpClient)
    {
        _secureHttpClient = secureHttpClient;
        _publicHttpClient = publicHttpClient;
    }


    public async Task<T?> DeleteDataAsync<T>(string endpoint, bool usePublicHttpClient = false, CancellationToken cancellationToken = default)
        where T : class?
    {
        var response = usePublicHttpClient
            ? await _publicHttpClient.Client.DeleteAsync(endpoint, cancellationToken)
            : await _secureHttpClient.Client.DeleteAsync(endpoint, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<T>();
            return content;
        }

        throw new Exception(response.ReasonPhrase);
    }


    public async Task<T?> GetDataAsync<T>(string endpoint, bool usePublicHttpClient = false, CancellationToken cancellationToken = default)
        where T : class?
    {
        var response = usePublicHttpClient
            ? await _publicHttpClient.Client.GetFromJsonAsync<T>(endpoint, cancellationToken)
            : await _secureHttpClient.Client.GetFromJsonAsync<T>(endpoint, cancellationToken);

        return response;
    }


    public async Task<string?> GetSerializedDataAsync<T>(string endpoint, bool usePublicHttpClient = false, CancellationToken cancellationToken = default)
        where T : class?
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


    public async Task<TResponse?> PostDataAsync<TResponse, TRequest>(string endpoint, TRequest request, bool usePublicHttpClient = false, CancellationToken cancellationToken = default)
        where TResponse : class
        where TRequest : class
    {
        var response = usePublicHttpClient
            ? await _publicHttpClient.Client.PostAsJsonAsync(endpoint, request, cancellationToken)
            : await _secureHttpClient.Client.PostAsJsonAsync(endpoint, request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<TResponse>();
            return content;
        }

        throw new Exception(response.ReasonPhrase);

    }
}
