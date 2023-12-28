namespace Smooth.Api.Application.Options;

public class ApplicationDbOptions
{
    public const string OptionsName = "ApplicationDb";

    public string? ConnectionString { get; init; }

}
