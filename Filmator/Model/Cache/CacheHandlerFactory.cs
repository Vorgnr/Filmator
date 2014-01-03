using System.Collections;
using Filmator.Model.Entities;
using TMDbLib.Objects.General;

namespace Filmator.Model.Cache {
    public static class CacheHandlerFactory {
        public static ICache<T> GetCacheHandler<T>() {
            var type = typeof(T);
            if (type == typeof(MovieStored))
                return (ICache<T>)new MovieStoredCacheHandler();
            if (type == typeof(SearchContainer<MovieResult>))
                return (ICache<T>)new SearchContainerCacheHandler();
            return null;
        } 
    }
}
