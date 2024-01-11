// Imortant! Do not remove the namespace
// Azure.Identity. It will cause a namespace
// not found error when deploying because
// of conditional code in AddAzureKeyVault.
using Azure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;
using Smooth.Api.Application.Options;
using Smooth.Api.WebApp.SignalR;
using Smooth.Shared.Configurations.MediaFiles.Options;
using Microsoft.Identity.Web;

namespace Smooth.Api.WebApp.Configuration;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddConfigurationOptions(this WebApplicationBuilder builder)
    {
        builder
            .AddCorsOptions()
            .AddAzureOptions()
            .AddAzureStorageAccountOptions()
            .AddApplicationDbOptions()
            .AddMediaFilesOptions();

        return builder;
    }


    public static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        var corsOptions = builder.Services
            .BuildServiceProvider()
            .GetService<IOptions<CorsOptions>>()?.Value;

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
        var azureOptions = builder.Services
            .BuildServiceProvider()
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


    public static WebApplicationBuilder AddAzureSignalR(this  WebApplicationBuilder builder)
    {
        var azureOptions = builder.Services
            .BuildServiceProvider()
            .GetService<IOptions<AzureOptions>>()?.Value;

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


    public static WebApplicationBuilder AddResponseCompression(this WebApplicationBuilder builder)
    {
        builder.Services.AddResponseCompression(opts =>
        {
            opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                  new[] { "application/octet-stream" });
        });

        return builder;
    }


    public static WebApplicationBuilder AddAuthentication(this WebApplicationBuilder builder)
    {
        //var x = builder.Configuration.GetSection("AzureAdB2C");

        //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

        //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //    .AddMicrosoftIdentityWebApi(options =>
        //    {
        //        builder.Configuration.Bind("AzureAdB2C", options);

        //        //options.TokenValidationParameters.NameClaimType = "name";
        //        //options.TokenValidationParameters.ValidateIssuer = true;
        //        //options.TokenValidationParameters.ValidIssuer = "https://ekzaktb2cdev.b2clogin.com/bf8ba302-a667-4e57-9cdb-84e37c41bc58/v2.0/";
        //    },
        //    options => { builder.Configuration.Bind("AzureAdB2C", options); });

        // End of the Microsoft Identity platform block    

        return builder;
    }




    #region Helpers

    private static WebApplicationBuilder AddCorsOptions(this WebApplicationBuilder builder)
    {
        builder.Services
           .Configure<CorsOptions>
           (
               builder.Configuration.GetSection(CorsOptions.OptionsName)
           );

        return builder;
    }


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
