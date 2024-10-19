namespace ChitChat.Application.Services.Caching
{
    public interface ICachingService
    {
        Task Set<T>(string key, T value);
        Task<T> Get<T>(string key);
        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> func);
        Task Remove(string key);
    }
}
