using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smoorth.Infrastructure.WeatherForecasts;
using Smooth.Application.Azure;
using Smooth.Application.WeatherForecasts;


var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services
            .AddOptions<AzureOptions>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(AzureOptions.OptionsName).Bind(settings);

            });

        services.AddScoped<IWeatherForecastService, WeatherForecastService>();

        //services.Configuration.AddAzureKeyVault(
        //    new Uri($"https://{azureOptions?.KeyVault?.Name}.vault.azure.net/"),
        //    new DefaultAzureCredential(new DefaultAzureCredentialOptions
        //    {
        //        ExcludeEnvironmentCredential = true,
        //        ExcludeInteractiveBrowserCredential = true,
        //        ExcludeAzurePowerShellCredential = true,
        //        ExcludeSharedTokenCacheCredential = true,
        //        ExcludeVisualStudioCodeCredential = true,
        //        ExcludeVisualStudioCredential = true,
        //        ExcludeAzureCliCredential = true,
        //        ExcludeManagedIdentityCredential = false
        //    }));

    })
    .Build();



host.Run();


