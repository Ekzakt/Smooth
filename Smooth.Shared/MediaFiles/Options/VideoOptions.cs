namespace Smooth.Shared.MediaFiles.Options;

public class VideoOptions : IMediaFileOptions
{
    public static string OptionsName => "Videos";

    public long MaxLength { get; init; } = 104857600; // 100 MB
    public string[] Destinations { get; init; } = [];
    public MimeType[] MimeTypes { get; init; } = [];
    public string OutputMimeTypeValue { get; init; } = string.Empty;
    public string[] Tags { get; init; } = [];

}
