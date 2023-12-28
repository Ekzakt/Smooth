namespace Smooth.Api.Application.Options;

public class AzureOptions
{
    public const string OptionsName = "Azure";

    public KeyVaultOptions? KeyVault { get; init; }

}


