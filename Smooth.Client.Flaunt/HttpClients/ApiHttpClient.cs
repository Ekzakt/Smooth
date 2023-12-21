using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Smooth.Client.Flaunt.HttpClients;

public class ApiHttpClient
{
    public HttpClient Client { get; }

    public ApiHttpClient(HttpClient client)
    {
        Client = client;

        //var baseAddress = _configuration.GetValue<string>("ApiBaseAddress");

        //if (string.IsNullOrEmpty(baseAddress))
        //{
        //    baseAddress = environment.BaseAddress;
        //}

        //Client.BaseAddress = new Uri(baseAddress);

        //Client.Timeout = new TimeSpan(0, 0, 30);
        //Client.DefaultRequestHeaders.Clear();
    }
}
