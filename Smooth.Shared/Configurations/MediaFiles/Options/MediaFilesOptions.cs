namespace Smooth.Shared.Configurations.MediaFiles.Options;

public class MediaFilesOptions
{
    public static string OptionsName => "MediaFiles";

    public ImageOptions Images { get; init; } = new();
    public VideoOptions Videos { get; init; } = new();
    public SoundOptions Sound { get; set; } = new();
    public List<MimeType> AllMimeTypes => Images.MimeTypes.Concat(Videos.MimeTypes).ToList();
}
