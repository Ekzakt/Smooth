using Microsoft.AspNetCore.Components.Forms;

namespace Smooth.Client.Application.Managers;

public interface IFileManager
{
    Task<Guid> SaveFileAsync(IBrowserFile file, CancellationToken cancellationToken = default);
}