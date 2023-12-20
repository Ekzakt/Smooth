using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Smooth.Client.Flaunt;
using Smooth.Client.Flaunt.HttpClients;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var functionAppApiPrefix = builder.Configuration.GetValue<string>("FunctionAppApiPrefix");
var webAppApiPrefix = builder.Configuration.GetValue<string>("WebAppApiPrefix");

builder.Services
            .AddHttpClient<FunctionAppApiHttpClient>(client =>
                client.BaseAddress = new Uri(functionAppApiPrefix!));

builder.Services
            .AddHttpClient<WebAppApiHttpClient>(client =>
                client.BaseAddress = new Uri(webAppApiPrefix!));

await builder.Build().RunAsync();
