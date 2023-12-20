using System.Net.Http;

namespace Smooth.Client.Flaunt.HttpClients;

public class FunctionAppApiHttpClient : BaseHttpClient
{
    public FunctionAppApiHttpClient(HttpClient httpClient)
        : base(httpClient)
    {
    }
}
