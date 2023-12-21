namespace Smooth.Shared.MediaFiles.Options;

public class SoundOptions : IMediaFileOptions
{
    public static string OptionsName => "Sounds";

    public long MaxLength { get; init; } = 52428800; // 50 MB
    public string OutputMimeTypeValue { get; init; } = string.Empty;
    public string[] Destinations { get; init; } = [];
    public MimeType[] MimeTypes { get; init; } = [];
    public string[] Tags { get; init; } = [];
}
