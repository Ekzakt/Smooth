using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Smooth.Client.Application.Managers;
using Smooth.Client.Flaunt;
using Smooth.Client.Flaunt.Configuration;
using Smooth.Client.Flaunt.Pages;


var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.AddHttpClients();
builder.AddMsalAuthentication();

builder.Services.AddScoped<ApiAuthorizationMessageHandler>();
builder.Services.AddScoped<IHttpDataManager, HttpDataManager>();
builder.Services.AddScoped<IFileManager, FileManager>();

await builder.Build().RunAsync();
