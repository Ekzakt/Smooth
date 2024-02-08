using AutoMapper;
using Ekzakt.FileManager.Core.Models;
using Smooth.Shared.Models;

namespace Smooth.Api.Mappers;

public class FileInformationMapper : Profile
{
	public FileInformationMapper()
	{
        CreateMap<FileInformation, FileInformationDto>();
    }
}