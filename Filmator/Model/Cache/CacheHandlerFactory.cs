using System.Collections;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;

namespace Filmator.Model.Cache {
    public static class CacheHandlerFactory {
        public static ICache<T> GetCacheHandler<T>() {
            var type = typeof(T);
            if (type == typeof(Movie))
                return (ICache<T>)new MovieStoredCacheHandler();
            if (type == typeof(SearchContainer<MovieResult>))
                return (ICache<T>)new SearchContainerCacheHandler();
            return null;
        } 
    }
}
