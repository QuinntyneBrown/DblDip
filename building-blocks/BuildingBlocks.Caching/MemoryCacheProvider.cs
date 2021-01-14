namespace BuildingBlocks.Caching
{
    public class MemoryCacheProvider : ICacheProvider
    {
        public ICache GetCache()
        {
            return MemoryCache.Current;
        }
    }
}
