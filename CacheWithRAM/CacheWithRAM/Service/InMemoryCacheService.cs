namespace CacheWithRAM.Service
{
    public class InMemoryCacheService
    {
        private readonly Dictionary<string, object> _cache;
        private readonly Dictionary<string, object> _dataStore;

        public InMemoryCacheService()
        {
            _cache = new Dictionary<string, object>();
            _dataStore = new Dictionary<string, object>
            {
                { "Product_1", new { Id = "1", Name = "Product 1", Price = 100 } },
                { "Product_2", new { Id = "2", Name = "Product 2", Price = 200 } },
                { "Product_3", new { Id = "3", Name = "Product 3", Price = 300 } },
            };


        }
        public void AddToCache(string key, object value)
        {
            _cache[key] = value;
        }
        public object GetFromCache(string key)
        {
            return _cache.TryGetValue(key, out var value) ? value : null;
        }
        public void RemoveFromMemoryCache(string key)
        {
            if (_cache.ContainsKey(key))
            {
                _cache.Remove(key);
            }
        }
        public object GetFromDataStore(string key)
        {
            return _dataStore.ContainsKey(key) ? _dataStore[key] : null;
        }
        public void RemoveFromDataStore(string key)
        {
            if (_dataStore.ContainsKey(key))
            {
                _dataStore.Remove(key);
            }
        }
        public void AddToDataStore(string key, object value)
        {
            _dataStore[key] = value;
        }
    }
}
