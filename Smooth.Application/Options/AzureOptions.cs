namespace Smooth.Api.Application.Options;

public class AzureOptions
{
    public const string OptionsName = "Azure";

    public AzureKeyVaultOptions? KeyVault { get; init; }

    public AzureStorageAccountOptions? StorageAccount { get; init; }

}


