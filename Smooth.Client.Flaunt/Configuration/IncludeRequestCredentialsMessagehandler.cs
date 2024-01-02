using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace Smooth.Client.Flaunt.Configuration;

public class IncludeRequestCredentialsMessagHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        return base.SendAsync(request, cancellationToken);
    }
}
