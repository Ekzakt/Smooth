namespace Smooth.Shared.MediaFiles.Options;

public class ImagesOptions : IMediaFileOptions
{
    public static string OptionsName => "Images";

    public long MaxLength { get; init; } = 10485760; // 10 MB
    public string OutputMimeTypeValue { get; init; } = string.Empty;
    public string[] Destinations { get; init; } = [];
    public MimeType[] MimeTypes { get; init; } = [];
    public string[] Tags { get; init; } = [];
    public ImageTransformationOptions[] Transformations { get; init; } = [];
}
