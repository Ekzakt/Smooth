using Microsoft.AspNetCore.ResponseCompression;
using Smooth.Api.Application.Configuration;
using Smooth.Api.Application.WeatherForecasts;
using Smooth.Api.Infrastructure.Configuration;
using Smooth.Api.Infrastructure.WeatherForecasts;
using Smooth.Api.WebApp.Configuration;
using Smooth.Api.WebApp.SignalR;
using Smooth.Shared.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
          new[] { "application/octet-stream" });
});

builder.AddConfigurationOptions();
builder.AddAzure();
builder.AddCors();
builder.AddSignalRHubConnections();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();



var app = builder.Build();

app.UseStaticFiles();
app.UseResponseCompression();
app.UseCors(CorsOptions.OptionsName);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHub<NotificationsHub>(SignalREndpoints.NOTIFICATIONS_HUB);

app.Run();
