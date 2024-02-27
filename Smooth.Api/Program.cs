using Ekzakt.EmailSender.Smtp.Configuration;
using Serilog;
using Smooth.Api.Application.Configuration;
using Smooth.Api.Application.WeatherForecasts;
using Smooth.Api.Configuration;
using Smooth.Api.Hubs;
using Smooth.Api.Infrastructure.Configuration;
using Smooth.Api.Infrastructure.WeatherForecasts;
using Smooth.Shared.Configurations.Options;
using Smooth.Shared.Endpoints;
using System.Reflection;
using Ekzakt.FileManager.AzureBlob.Configuration;
using Azure.Monitor.OpenTelemetry.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


//builder.Host.UseSerilog((context, configuration) =>
//    configuration.ReadFrom.Configuration(context.Configuration));

//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    .CreateBootstrapLogger();


builder.AddResponseSizeCompression();
builder.AddConfigurationOptions();
builder.AddAzureClientServices();
builder.AddAzureKeyVault();
builder.AddAuthentication();
builder.AddCors();
builder.AddAzureSignalR();
builder.AddApplicationInsights();

builder.Services.AddOpenTelemetry().UseAzureMonitor();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSmtpEmailSender(SmtpEmailSenderOptions.OptionsName);
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
builder.Services.AddAzureBlobFileManager();

//builder.Services.AddApplicationInsightsTelemetry(new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions
//{
//    ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
//});


var app = builder.Build();

app.UseMiddleware<CorrelationIdMiddleware>();
//app.UseSerilogRequestLogging();
app.UseCors(CorsOptions.POLICY_NAME);
app.UseHttpsRedirection();
app.UseRouting();
app.UseResponseSizeCompression();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<NotificationsHub>(Hubs.NOTIFICATIONS_HUB);
app.MapHub<ProgressHub>(Hubs.PROGRESS_HUB);


app.Run();
