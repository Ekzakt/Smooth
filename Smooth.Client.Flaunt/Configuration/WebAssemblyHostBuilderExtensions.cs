using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Smooth.Client.Application.HttpClients;
using Smooth.Shared;

namespace Smooth.Client.Flaunt.Configuration;

public static class WebAssemblyHostBuilderExtensions
{
    public static WebAssemblyHostBuilder AddHttpClients(this WebAssemblyHostBuilder builder)
    {
        var apiBaseAddress = builder.Configuration
            .GetValue<string>(Constants.API_BASE_ADDRESS_CONFIG_NAME);

        apiBaseAddress ??= builder.HostEnvironment.BaseAddress;


        builder.Services
            .AddHttpClient<ApiHttpClient>(config =>
                {
                    config.BaseAddress = new Uri(apiBaseAddress);
                })
            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();


        builder.Services
            .AddHttpClient<PublicHttpClient>(config =>
            {
                config.BaseAddress = new Uri(apiBaseAddress);
            });

        return builder;
    }


    public static WebAssemblyHostBuilder AddMsalAuthentication(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddMsalAuthentication(options =>
        {
            builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);

            //options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
            //options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access");
            options.ProviderOptions.DefaultAccessTokenScopes.Add("https://ekzaktb2cdev.onmicrosoft.com/8a1d8d43-59b0-4efa-98fc-577e24d2089a/api.fullaccess");


            options.ProviderOptions.LoginMode = "redirect";
        });

        return builder;
    }
}
