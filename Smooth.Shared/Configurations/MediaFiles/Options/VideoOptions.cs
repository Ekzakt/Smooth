namespace Smooth.Shared.Configurations.MediaFiles.Options;

public class VideoOptions : IMediaFileOptions
{
    public static string OptionsName => "Videos";

    public long MaxLength { get; init; } = 104857600; // 100 MB
    public string[] Destinations { get; init; } = Array.Empty<string>();
    public MimeType[] MimeTypes { get; init; } = Array.Empty<MimeType>();
    public string OutputMimeTypeValue { get; init; } = string.Empty;
    public string[] Tags { get; init; } = Array.Empty<string>();

}
