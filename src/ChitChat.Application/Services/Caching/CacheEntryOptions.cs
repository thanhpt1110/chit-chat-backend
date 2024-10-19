namespace ChitChat.Application.Services.Caching
{
    public class CacheEntryOptions
    {
        public TimeSpan SlidingExpiration { get; set; }
        public TimeSpan AbsoluteExpirationRelativeToNow { get; set; }

        public static CacheEntryOptions Default = new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1),
            SlidingExpiration = TimeSpan.FromHours(1)
        };
    }
}
