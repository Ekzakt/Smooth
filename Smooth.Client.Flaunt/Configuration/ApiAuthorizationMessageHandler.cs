using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;
using Smooth.Shared;

namespace Smooth.Client.Flaunt.Configuration
{
    public class ApiAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public ApiAuthorizationMessageHandler(
            IAccessTokenProvider provider,
            NavigationManager navigation,
            IConfiguration configuration)
            : base(provider, navigation)
        {

            var apiBaseAddress = configuration
                .GetValue<string>(Constants.API_BASE_ADDRESS_CONFIG_NAME);

            ConfigureHandler(
                authorizedUrls: new[] { apiBaseAddress, "https://localhost:7084" });
                //scopes: new[] { "api.read", "api.write" });
        }
    }
}
