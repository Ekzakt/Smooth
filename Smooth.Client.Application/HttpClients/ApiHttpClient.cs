namespace Smooth.Client.Application.HttpClients;

public class ApiHttpClient
{
    public HttpClient Client { get; }

    public ApiHttpClient(HttpClient client)
    {
        Client = client;
    }
}
