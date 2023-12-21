namespace Smooth.Shared.MediaFiles.Options;

public class MediaFilesOptions
{
    public static string OptionsName => "MediaFiles";

    public ImagesOptions Images { get; init; } = new();
    public VideoOptions Videos { get; init; } = new();
    public List<MimeType> AllMimeTypes => Images.MimeTypes.Concat(Videos.MimeTypes).ToList();
}
