using Azure.Identity;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Options;
using Smooth.Api.Application.Options;
using Smooth.Api.WebApp.SignalR;
using Smooth.Shared.Configurations.MediaFiles.Options;

namespace Smooth.Api.WebApp.Configuration;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddConfigurationOptions(this WebApplicationBuilder builder)
    {
        builder
            .AddAzureOptions()
            .AddAzureStorageAccountOptions()
            .AddApplicationDbOptions()
            .AddMediaFilesOptions();

        return builder;
    }


    public static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            var corsValues = CorsOptions.CorsValues;

            options.AddPolicy(name: CorsOptions.OptionsName,
                              policy =>
                              {
                                  policy.WithOrigins(corsValues);
                                  policy.AllowAnyHeader();
                                  policy.AllowAnyMethod();
                              });
        });

        return builder;
    }


    public static WebApplicationBuilder AddAzure(this WebApplicationBuilder builder)
    {
        var azureOptions = builder.Services.BuildServiceProvider()
            .GetService<IOptions<AzureOptions>>()?.Value;

#if !DEBUG
        builder.Configuration.AddAzureKeyVault(
            new Uri($"https://{azureOptions?.KeyVault?.Name}.vault.azure.net/"),
            new DefaultAzureCredential(new DefaultAzureCredentialOptions
            {
                ExcludeEnvironmentCredential = true,
                ExcludeInteractiveBrowserCredential = true,
                ExcludeAzurePowerShellCredential = true,
                ExcludeSharedTokenCacheCredential = true,
                ExcludeVisualStudioCodeCredential = true,
                ExcludeVisualStudioCredential = true,
                ExcludeAzureCliCredential = !builder.Environment.IsDevelopment(),
                ExcludeManagedIdentityCredential = builder.Environment.IsDevelopment()
            }));
#endif

        return builder;
    }


    public static WebApplicationBuilder AddSignalRHubConnections(this  WebApplicationBuilder builder)
    {
        builder.Services.AddSignalR().AddHubOptions<NotificationsHub>(options =>
        {
            options.EnableDetailedErrors = builder.Environment.IsDevelopment();
        });

        return builder;
    }




    #region Helpers

    private static WebApplicationBuilder AddAzureOptions(this WebApplicationBuilder builder)
    {
        builder.Services
           .Configure<AzureOptions>
           (
               builder.Configuration.GetSection(AzureOptions.OptionsName)
           );

        return builder;
    }


    private static WebApplicationBuilder AddAzureStorageAccountOptions(this WebApplicationBuilder builder)
    {
        builder.Services
            .Configure<AzureStorageAccountOptions>
            (
                builder.Configuration.GetSection(AzureStorageAccountOptions.OptionsName)
            );

        return builder;
    }


    private static WebApplicationBuilder AddApplicationDbOptions(this WebApplicationBuilder builder)
    {
        builder.Services
            .Configure<ApplicationDbOptions>
            (
                builder.Configuration.GetSection(ApplicationDbOptions.OptionsName)
            );

        return builder;
    }


    private static WebApplicationBuilder AddMediaFilesOptions(this WebApplicationBuilder builder)
    {
        builder.Services
            .Configure<MediaFilesOptions>
            (
                builder.Configuration
                    .GetSection(MediaFilesOptions.OptionsName)
            );

        builder.Services
           .Configure<ImageOptions>
           (
                builder.Configuration
                    .GetSection(MediaFilesOptions.OptionsName)
                    .GetSection(ImageOptions.OptionsName)
           );

        builder.Services
           .Configure<VideoOptions>
           (
                builder.Configuration
                    .GetSection(MediaFilesOptions.OptionsName)
                    .GetSection(VideoOptions.OptionsName)
           );

        builder.Services
          .Configure<SoundOptions>
          (
               builder.Configuration
                   .GetSection(MediaFilesOptions.OptionsName)
                   .GetSection(SoundOptions.OptionsName)
          );

        return builder;
    }


    #endregion Helpers
}
