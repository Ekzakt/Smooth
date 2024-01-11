using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Smooth.Shared;

namespace Smooth.Client.Flaunt.Configuration;

public class ApiAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public ApiAuthorizationMessageHandler(
        IAccessTokenProvider provider,
        NavigationManager navigation,
        IConfiguration configuration)
        : base(provider, navigation)
    {

        var scope = $"https://ekzaktb2cdev.onmicrosoft.com/5835b0d0-9e03-4b6c-97c4-4213c87f3808/api_fullaccess";

        var apiBaseAddress = configuration
            .GetValue<string>(Constants.API_BASE_ADDRESS_CONFIG_NAME);

        ConfigureHandler(
            authorizedUrls: new[] { "https://dev.ekzakt.be", "https://api.ekzakt.be", "https://localhost:7083", "https://localhost:7084" },
            scopes: new[] { scope });
    }
}