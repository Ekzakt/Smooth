// Imortant! Do not remove the namespace
// Azure.Identity. It will cause a namespace
// not found error when deploying because
// of conditional code in AddAzureKeyVault.
using Azure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Smooth.Api.WebApp.SignalR;
using Smooth.Shared.Configurations.MediaFiles.Options;
using Microsoft.Identity.Web;
using Smooth.Api.Application.Options;

namespace Smooth.Api.WebApp.Configuration;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddConfigurationOptions(this WebApplicationBuilder builder)
    {
        builder.AddMediaFilesOptions();

        return builder;
    }


    public static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        CorsOptions corsOptions = new();
        builder.Configuration.GetSection(CorsOptions.OptionsName).Bind(corsOptions);

        var origins = corsOptions?.AllowedOrigins;

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: CorsOptions.POLICY_NAME,
                policy =>
                {
                    policy.WithOrigins(origins ?? Array.Empty<string>());
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowCredentials();
                });
        });

        return builder;
    }


    public static WebApplicationBuilder AddAzureKeyVault(this WebApplicationBuilder builder)
    {
        AzureOptions azureOptions = new();
        builder.Configuration.GetSection(AzureOptions.OptionsName).Bind(azureOptions);

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


    public static WebApplicationBuilder AddAzureSignalR(this  WebApplicationBuilder builder)
    {
        AzureOptions azureOptions = new();
        builder.Configuration.GetSection(AzureOptions.OptionsName).Bind(azureOptions);

        builder.Services
            .AddSignalR()
            //.AddAzureSignalR(options =>
            //{
            //    options.ConnectionString = azureOptions?.SignalR?.ConnectionString;
            //})
            .AddHubOptions<NotificationsHub>(options =>
            {
                options.EnableDetailedErrors = builder.Environment.IsDevelopment();
            });

        return builder;
    }


    public static WebApplicationBuilder AddResponseSizeCompression(this WebApplicationBuilder builder)
    {
        if (!builder.Environment.IsDevelopment())
        {
            builder.Services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                      new[] { "application/octet-stream" });
            });
        }

        return builder;
    }


    public static WebApplicationBuilder AddAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(options =>
            {
                builder.Configuration.Bind(Application.Constants.AZUREB2C_CONFIG_NAME, options);

                options.TokenValidationParameters.NameClaimType = "display_name";
            },
            options =>
            { 
                builder.Configuration.Bind(Application.Constants.AZUREB2C_CONFIG_NAME, options);
            });

        return builder;
    }




    #region Helpers

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
