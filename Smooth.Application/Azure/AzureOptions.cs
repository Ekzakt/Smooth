namespace Smooth.Api.Application.Azure;

public class AzureOptions
{
    public const string OptionsName = "Azure";

    public KeyVaultOptions? KeyVault { get; init; }

}


