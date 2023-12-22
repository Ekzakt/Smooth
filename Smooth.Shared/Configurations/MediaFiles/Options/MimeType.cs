namespace Smooth.Shared.Configurations.MediaFiles.Options;

public class MimeType : Ekzakt.Core.Files.MimeType
{
    public string[] Destinations { get; init; } = Array.Empty<string>();
}
