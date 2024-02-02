namespace Smooth.Api.Application.Options;

public class AzureStorageOptions
{
    public const string SectionName = "Azure:Storage";

    public string? ServiceUri { get; init; }

    public string[]? Containers { get; init; }
}
