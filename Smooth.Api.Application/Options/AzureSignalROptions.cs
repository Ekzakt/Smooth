namespace Smooth.Api.Application.Options;

public class AzureSignalROptions
{
    public const string SectionName = "Azure:SignalR";

    public string? ConnectionString { get; init; }
}
