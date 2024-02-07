using Microsoft.AspNetCore.Components.Forms;
using Smooth.Shared.Endpoints;
using Smooth.Shared.Models.Responses;
using System.Net.Http.Headers;

namespace Smooth.Client.Application.Managers;

public class FileManager : IFileManager
{
    private readonly IHttpDataManager _httpDataManager;

    public FileManager(IHttpDataManager dataMananger)
    {
        _httpDataManager = dataMananger;
    }


    public async Task<Guid> SaveFileAsync(IBrowserFile file, CancellationToken cancellationToken = default)
    {
        var content = new MultipartFormDataContent();

        var fileContent =
            new StreamContent(file.OpenReadStream(file.Size));

        fileContent.Headers.ContentType =
            new MediaTypeHeaderValue(file.ContentType);

        content.Add(
            content: fileContent,
            name: "\"file\"",
            fileName: file.Name);

        var endpoint = EndPoints.POST_FILE();

        var result = await _httpDataManager.PostHttpContent<PostFileResponse>(endpoint, content, true, cancellationToken);

        return result?.FileId ?? Guid.Empty;
    }
}