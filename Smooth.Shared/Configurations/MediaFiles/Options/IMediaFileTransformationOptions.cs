﻿namespace Smooth.Shared.Configurations.MediaFiles.Options;

public interface IMediaFileTransformationOptions
{
    string Name { get; init; }
    string[] Tags { get; init; }
}
