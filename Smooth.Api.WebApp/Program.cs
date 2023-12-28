using Smooth.Api.Application.Configuration;
using Smooth.Api.Application.WeatherForecasts;
using Smooth.Api.Infrastructure.Configuration;
using Smooth.Api.Infrastructure.WeatherForecasts;
using Smooth.Api.WebApp.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.AddConfigurationOptions();
builder.AddAzure();
builder.AddCors();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();



var app = builder.Build();

app.UseCors(CorsOptions.OptionsName);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
