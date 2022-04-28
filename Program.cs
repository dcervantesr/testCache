using LazyCache;
using Microsoft.Extensions.Caching.Memory;

Console.WriteLine("Memory Cache");
var cache = new MemoryCache(new MemoryCacheOptions());
int counter = 0;
Parallel.ForEach(Enumerable.Range(1, 30), i =>
{
    var item = cache.GetOrCreate("test-key", cacheEntry =>
    {
        cacheEntry.SlidingExpiration = TimeSpan.FromSeconds(10);
        return Interlocked.Increment(ref counter);
    });
    Console.Write($"{item} ");
});

Console.WriteLine("\nLazy Cache");
IAppCache cache2 = new CachingService();
int counter2 = 0;
Parallel.ForEach(Enumerable.Range(1, 30), i =>
{
    var item = cache2.GetOrAdd("test-key", cacheEntry =>
    {
        cacheEntry.SlidingExpiration = TimeSpan.FromSeconds(10);
        return Interlocked.Increment(ref counter2);
    });
    Console.Write($"{item} ");
});