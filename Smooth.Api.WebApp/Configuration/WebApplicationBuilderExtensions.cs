using Smooth.Api.Application.Options;
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
