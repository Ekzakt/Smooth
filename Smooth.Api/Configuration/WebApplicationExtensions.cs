namespace Smooth.Api.WebApp.Configuration;

public static class WebApplicationExtensions
{
    public static WebApplication UseResponseSizeCompression(this WebApplication application)
    {
        if (!application.Environment.IsDevelopment())
        {
            application.UseResponseCompression(); ;
        }

        return application;
    }
}
