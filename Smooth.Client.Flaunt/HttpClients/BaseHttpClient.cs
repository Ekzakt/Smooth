namespace Smooth.Client.Flaunt.HttpClients;

public abstract class BaseHttpClient
{
    public HttpClient Client { get; }

    public BaseHttpClient(HttpClient httpClient)
    {
        Client = httpClient;
    }
}
