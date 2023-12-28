namespace Smooth.Client.Application.Managers;

public interface IHttpDataManager
{
    Task<T?> GetDataAsync<T>(string endpoint);
    Task<string?> GetSerializedDataAsync<T>(string endpoint);
}
