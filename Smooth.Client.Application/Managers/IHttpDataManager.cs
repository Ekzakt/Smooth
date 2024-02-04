namespace Smooth.Client.Application.Managers;

public interface IHttpDataManager
{
    Task<T?> GetDataAsync<T>(string endpoint, bool usePublicHttpClient = false, CancellationToken cancellationToken = default);

    Task<string?> GetSerializedDataAsync<T>(string endpoint, bool usePublicHttpClient = false, CancellationToken cancellationToken = default);

    Task<T?> PostDataAsync<T, U>(string endpoint, U data, bool usePublicHttpClient = false, CancellationToken cancellationToken = default)
        where T : class
        where U : class;

    Task<T?> DeleteDataAsync<T>(string endpoint, bool usePublicHttpClient = false, CancellationToken cancellationToken = default)
        where T : class?;
}
