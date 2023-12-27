namespace Smooth.Shared.Configurations.MediaFiles.Options;

public class ImagesOptions : IMediaFileOptions
{
    public static string OptionsName => "Images";

    public long MaxLength { get; init; } = 10485760; // 10 MB
    public string OutputMimeTypeValue { get; init; } = string.Empty;
    public string[] Destinations { get; init; } = Array.Empty<string>();
    public MimeType[] MimeTypes { get; init; } = Array.Empty<MimeType>();
    public string[] Tags { get; init; } = Array.Empty<string>();
    public ImageTransformationOptions[] Transformations { get; init; } = Array.Empty<ImageTransformationOptions>();
    public int UploadCount => Transformations.Length == 0 ? 1 : Transformations.Length;
}
