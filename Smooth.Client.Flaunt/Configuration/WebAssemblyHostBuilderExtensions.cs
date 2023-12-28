using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Smooth.Client.Application.HttpClients;
using Smooth.Shared;

namespace Smooth.Client.Flaunt.Configuration;

public static class WebAssemblyHostBuilderExtensions
{
    public static WebAssemblyHostBuilder AddHttpClients(this WebAssemblyHostBuilder builder)
    {
        var apiBaseAddress = builder.Configuration
            .GetValue<string>(Constants.API_BASE_ADDRESS);

        apiBaseAddress ??= builder.HostEnvironment.BaseAddress;

        builder.Services.AddHttpClient<ApiHttpClient>(config =>
        {
            config.BaseAddress = new Uri(apiBaseAddress);
        });

        return builder;
    }
}
