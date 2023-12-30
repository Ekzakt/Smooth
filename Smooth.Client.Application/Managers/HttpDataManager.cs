using Smooth.Client.Application.HttpClients;
using Smooth.Shared.Endpoints;
using System.Net.Http.Json;
using System.Text.Json;

namespace Smooth.Client.Application.Managers;

public class HttpDataManager : IHttpDataManager
{

    private readonly ApiHttpClient _httpClient;

    public HttpDataManager(ApiHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T?> GetDataAsync<T>(string endpoint)
    {
        var result = await _httpClient.Client.GetFromJsonAsync<T>(endpoint);

        return result;
    }

    public async Task<string?> GetSerializedDataAsync<T>(string endpoint)
    {
        var result = await GetDataAsync<T>(endpoint);

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
        var response = await _httpClient.Client.PostAsJsonAsync(endpoint, data);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<T>();
            return content;
        }

        return null;

        //return (T)Convert.ChangeType(response, typeof(T));
        //return (T)(object)response;
    }
}
