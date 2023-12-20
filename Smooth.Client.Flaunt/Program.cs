using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Smooth.Client.Flaunt;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



var apiBaseAddress = builder.Configuration.GetValue<string>("ApiBaseAddress");
if (string.IsNullOrEmpty(apiBaseAddress))
{
    apiBaseAddress = builder.HostEnvironment.BaseAddress;
}

builder.Services.AddHttpClient<ApiHttpClient>(config => config.BaseAddress = new Uri(apiBaseAddress));


await builder.Build().RunAsync();
