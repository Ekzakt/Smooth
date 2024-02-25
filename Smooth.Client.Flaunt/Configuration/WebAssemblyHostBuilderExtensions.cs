﻿using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Smooth.Client.Application;
using Smooth.Client.Application.HttpClients;

namespace Smooth.Client.Flaunt.Configuration;

public static class WebAssemblyHostBuilderExtensions
{
    public static WebAssemblyHostBuilder AddHttpClients(this WebAssemblyHostBuilder builder)
    {
        var apiBaseAddress = builder.Configuration
            .GetValue<string>(Constants.API_BASE_ADDRESS_CONFIG_NAME);

        apiBaseAddress ??= builder.HostEnvironment.BaseAddress;


        builder.Services
            .AddHttpClient<SecureHttpClient>(config =>
                {
                    config.BaseAddress = new Uri(apiBaseAddress);
                    config.DefaultRequestHeaders.Add("X-Correlation-Id", Guid.NewGuid().ToString());
                    config.Timeout = TimeSpan.FromSeconds(300);
                })
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();


        builder.Services
            .AddHttpClient<PublicHttpClient>(config =>
            {
                config.BaseAddress = new Uri(apiBaseAddress);
                config.DefaultRequestHeaders.Add("X-Correlation-Id", Guid.NewGuid().ToString());
                config.Timeout = TimeSpan.FromSeconds(300);
            });

        return builder;
    }


    public static WebAssemblyHostBuilder AddMsalAuthentication(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddMsalAuthentication(options =>
        {
            builder.Configuration.Bind(Constants.AZUREB2C_CONFIG_NAME, options.ProviderOptions.Authentication);

            options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
            options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access");

            // TODO: Place this in some variables (appsettings, environmentvariables).
            options.ProviderOptions.DefaultAccessTokenScopes.Add("https://ekzaktb2cdev.onmicrosoft.com/5835b0d0-9e03-4b6c-97c4-4213c87f3808/api_fullaccess");

            options.ProviderOptions.LoginMode = "redirect";
        });

        return builder;
    }
}
