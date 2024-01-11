using Microsoft.IdentityModel.Logging;
using Smooth.Api.Application.Configuration;
using Smooth.Api.Application.Options;
using Smooth.Api.Application.WeatherForecasts;
using Smooth.Api.Infrastructure.Configuration;
using Smooth.Api.Infrastructure.WeatherForecasts;
using Smooth.Api.WebApp.Configuration;
using Smooth.Api.WebApp.SignalR;
using Smooth.Shared.Endpoints;


var builder = WebApplication.CreateBuilder(args);

//builder.AddResponseCompression();
builder.AddConfigurationOptions();
builder.AddAzureKeyVault();
builder.AddAuthentication();
builder.AddCors();
builder.AddAzureSignalR();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();



var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    IdentityModelEventSource.ShowPII = true;
//}

app.UseCors(CorsOptions.POLICY_NAME);
app.UseHttpsRedirection();
app.UseRouting();
//app.UseResponseCompression();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationsHub>(SignalREndpoints.NOTIFICATIONS_HUB);


app.Run();
