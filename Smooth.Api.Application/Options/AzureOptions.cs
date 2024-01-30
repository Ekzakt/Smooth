namespace Smooth.Api.Application.Options;

public class AzureOptions
{
    public const string SectionName = "Azure";
    public const string SectionNameAzureB2C = "Azure:AdB2C";

    public AzureKeyVaultOptions? KeyVault { get; init; }

    public AzureStorageOptions? Storage { get; init; }

    public AzureSignalROptions? SignalR { get; init; }
}
