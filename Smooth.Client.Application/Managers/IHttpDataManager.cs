namespace Smooth.Client.Application.Managers;

public interface IHttpDataManager
{
    Task<T?> GetDataAsync<T>(string endpoint);
    Task<string?> GetSerializedDataAsync<T>(string endpoint);
    Task<T?> Insert<T, U>(string endpoint, U data)
        where T : class
        where U : class;
}
