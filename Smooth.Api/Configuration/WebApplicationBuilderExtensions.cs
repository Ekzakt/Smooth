﻿// Imortant! Do not remove the namespace
// Azure.Identity. It will cause a namespace
// not found error when deploying because
// of conditional code in AddAzureKeyVault.
using Azure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Identity.Web;
using Smooth.Api.Application.Options;
using Smooth.Api.SignalR;
using Smooth.Api.Configuration;
using Microsoft.Extensions.Azure;
using Ekzakt.FileManager.AzureBlob.Configuration;
using Smooth.Shared.Configurations.Options.MediaFiles;
using Smooth.Shared.Configurations.Options.Azure;

namespace Smooth.Api.Configuration;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddConfigurationOptions(this WebApplicationBuilder builder)
    {
        builder.AddMediaFilesOptions();

        return builder;
    }


    public static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        CorsOptions options = new();
        builder.Configuration
            .GetSection(CorsOptions.SectionName)
            .Bind(options);

        var origins = options?.AllowedOrigins;

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


    public static WebApplicationBuilder AddAzureClientServices(this WebApplicationBuilder builder)
    {
        AzureOptions options = new();
        builder.Configuration
            .GetSection(AzureOptions.SectionName)
            .Bind(options);

#if !DEBUG
        var azureCredentialOptions = new DefaultAzureCredentialOptions
        {
            ExcludeEnvironmentCredential = true,
            ExcludeInteractiveBrowserCredential = true,
            ExcludeAzurePowerShellCredential = true,
            ExcludeSharedTokenCacheCredential = true,
            ExcludeVisualStudioCodeCredential = true,
            ExcludeVisualStudioCredential = true,
            ExcludeAzureCliCredential = !builder.Environment.IsDevelopment(),
            ExcludeManagedIdentityCredential = builder.Environment.IsDevelopment()
        };
#endif

        builder.Services
                .AddAzureClients(clientBuilder => {
#if !DEBUG
                    clientBuilder
                        .AddSecretClient(builder.Configuration.GetSection(AzureKeyVaultOptions.SectionName));
#endif
                    clientBuilder
                        .UseCredential(new DefaultAzureCredential());
                    clientBuilder
                        .AddBlobServiceClient(builder.Configuration.GetSection(AzureStorageOptions.SectionName));
                    clientBuilder
                        .ConfigureDefaults(builder.Configuration.GetSection(AzureDefaultsOptions.SectionName));
                });

        return builder;
    }


    public static WebApplicationBuilder AddAzureSignalR(this WebApplicationBuilder builder)
    {
        AzureOptions options = new();
        builder.Configuration
            .GetSection(AzureOptions.SectionName)
            .Bind(options);


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


    public static WebApplicationBuilder AddApplicationInsights(this WebApplicationBuilder builder)
    {
        AzureApplicationInsightsOptions options = new();
        builder.Configuration
            .GetSection(AzureApplicationInsightsOptions.SectionName)
            .Bind(options);

        builder.Services.AddApplicationInsightsTelemetry(options => 
        {
            options.ConnectionString = options.ConnectionString;
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
                builder.Configuration.Bind(AzureOptions.SectionNameAzureB2C, options);

                options.TokenValidationParameters.NameClaimType = "display_name";
            },
            options =>
            {
                builder.Configuration.Bind(AzureOptions.SectionNameAzureB2C, options);
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
                    .GetSection(MediaFilesOptions.SectionName)
            );

        builder.Services
           .Configure<ImageOptions>
           (
                builder.Configuration
                    .GetSection(MediaFilesOptions.SectionName)
                    .GetSection(ImageOptions.SectionName)
           );

        builder.Services
           .Configure<VideoOptions>
           (
                builder.Configuration
                    .GetSection(MediaFilesOptions.SectionName)
                    .GetSection(VideoOptions.SectionName)
           );

        builder.Services
          .Configure<SoundOptions>
          (
               builder.Configuration
                   .GetSection(MediaFilesOptions.SectionName)
                   .GetSection(SoundOptions.SectionName)
          );

        // TODO: This can be all gone in production.
        builder.Services
          .Configure<AzureOptions>
          (
               builder.Configuration
                   .GetSection(AzureOptions.SectionName)
          );

        return builder;
    }


    #endregion Helpers
}
