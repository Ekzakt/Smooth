namespace Smooth.Shared.Configurations.MediaFiles.Options;

public interface IMediaFileOptions
{
    static string? OptionsName { get; }

    long MaxLength { get; init; }
    string OutputMimeTypeValue { get; init; }
    string[] Destinations { get; init; }
    MimeType[] MimeTypes { get; init; }
    string[] Tags { get; init; }
}
