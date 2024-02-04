using Ekzakt.EmailSender.Smtp.Configuration;
using Smooth.Api.Application.Configuration;
using Smooth.Api.Application.Options;
using Smooth.Api.Application.WeatherForecasts;
using Smooth.Api.Infrastructure.Configuration;
using Smooth.Api.Infrastructure.WeatherForecasts;
using Smooth.Shared.Endpoints;
using Serilog;
using Smooth.Api.SignalR;
using Smooth.Api.Configuration;
using Ekzakt.FileManager.AzureBlob.Configuration;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();


builder.AddResponseSizeCompression();
builder.AddConfigurationOptions();
builder.AddAzureServices();
builder.AddAuthentication();
builder.AddCors();
builder.AddAzureSignalR();


builder.Services.AddControllers();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSmtpEmailSender(SmtpEmailSenderOptions.OptionsName);
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
builder.Services.AddAzureBlobFileManager();


var app = builder.Build();

app.UseMiddleware<CorrelationIdMiddleware>();
app.UseSerilogRequestLogging();
app.UseCors(CorsOptions.POLICY_NAME);
app.UseHttpsRedirection();
app.UseRouting();
app.UseResponseSizeCompression();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<NotificationsHub>(Hubs.NOTIFICATIONS_HUB);


app.Run();
