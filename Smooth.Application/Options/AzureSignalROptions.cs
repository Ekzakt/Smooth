namespace Smooth.Api.Application.Options;

public class AzureSignalROptions
{
    public const string OptionsName = "Azure:SignalR";

    public string? ConnectionString { get; init; }
}
