using System.Collections;
using Filmator.Model.Entities;
using TMDbLib.Objects.General;

namespace Filmator.Model.Cache {
    public static class CacheHandlerFactory {
        public static ICache<T> GetCacheHandler<T>(Hashtable args = null) {
            var type = typeof(T);
            if (type == typeof(MovieStored))
                return (ICache<T>)new MovieStoredCacheHandler();
            if (type == typeof(SearchContainer<MovieResult>))
                if (args != null) return (ICache<T>)new SearchContainerCacheHandler((string)args["Type"], (int)args["Page"]);
            return null;
        } 
    }
}
