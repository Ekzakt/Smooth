namespace Smooth.Api.Application.Options;

public class AzureOptions
{
    public const string OptionsName = "Azure";
    public const string OptionsNameAzureAdB2C = "Azure:AdB2C";

    public AzureKeyVaultOptions? KeyVault { get; init; }

    public AzureStorageAccountOptions? StorageAccount { get; init; }

    public AzureSignalROptions? SignalR { get; init; }
}
