namespace Smooth.Api.Application.Options;

public class AzureStorageAccountOptions
{
    public const string OptionsName = "Azure:StorageAccount";

    public string? ConnectionString { get; init; }
    public string[]? Containers { get; init; }
}
