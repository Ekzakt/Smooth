using Smooth.Api.Application.Configuration;
using Smooth.Api.Application.WeatherForecasts;
using Smooth.Api.Infrastructure.Configuration;
using Smooth.Api.Infrastructure.WeatherForecasts;
using Smooth.Api.WebApp.Configuration;
using Smooth.Api.WebApp.SignalR;
using Smooth.Shared.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.AddResponseCompression();
builder.AddConfigurationOptions();
builder.AddAuthentication();
builder.AddAzureKeyVault();
builder.AddCors();
builder.AddAzureSignalR();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();



var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseCors(CorsOptions.POLICY_NAME);
app.UseResponseCompression();
app.UseRouting();
app.MapControllers();
app.UseAuthorization();

app.MapHub<NotificationsHub>(SignalREndpoints.NOTIFICATIONS_HUB);


app.Run();
