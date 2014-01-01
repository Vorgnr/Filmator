using System;
using System.Collections;
using System.Reflection;
using Filmator.Model.Cache;
using Filmator.Model.Entities;
using Filmator.Model.Enums;
using Filmator.Model.Provider;
using Filmator.Model.Utils;
using TMDbLib.Objects.General;

namespace Filmator.Model.Manager {
    public class MovieManager : IMovieManager {
        public SearchMovieProvider ResultProvider { get; private set; }
        public MovieStoredProvider StoredProvider { get; private set; }
        public MovieManager() {
            StoredProvider = new MovieStoredProvider();
            ResultProvider = new SearchMovieProvider();
        }

        public SearchContainer<MovieResult> GetSearchByState(SearchState state, int page = 1, int numberByPage = 20) {
            var method = typeof(MovieManager).GetMethod(state.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
            if (method == null) {
                return new SearchContainer<MovieResult>();
            }
            var callback = FastDelegates.DelegateFactory.Create(method);
            return (SearchContainer<MovieResult>)callback(this , new object[] {page, numberByPage});
        }

        public MovieStored GetMovieStoredById(int id) {
            var cacheHandler = CacheHandlerFactory.GetCacheHandler<MovieStored>();
            var movie = cacheHandler.Get(id);
            if (movie != null)
                return movie;
            movie = ResultProvider.GetById(id);
            if (movie != null) {
                cacheHandler.Add(movie, DateTime.Now.AddMonths(6));
                return ResultProvider.GetById(id);
            }
            return null;
        }

        private SearchContainer<MovieResult> TopRated(int page, int numberByPage) {
            var cacheHandler = CacheHandlerFactory.GetCacheHandler<SearchContainer<MovieResult>>(
                new Hashtable {{"Page", page}, {"Type", "TopRated"}});
            var searchContainer = cacheHandler.Get();
            if (searchContainer != null)
                return searchContainer;
            searchContainer = ResultProvider.TopRated(page);
            if(searchContainer != null) {
                cacheHandler.Add(searchContainer, DateTime.Now.AddHours(5));
                return searchContainer;
            }
            return null;
        }

        private SearchContainer<MovieResult> Popular(int page, int numberByPage) {
            var cacheHandler = CacheHandlerFactory.GetCacheHandler<SearchContainer<MovieResult>>(
                new Hashtable { { "Page", page }, { "Type", "Popular" } });
            var searchContainer = cacheHandler.Get();
            if (searchContainer != null)
                return searchContainer;
            searchContainer = ResultProvider.Popular(page);
            if (searchContainer != null) {
                cacheHandler.Add(searchContainer, DateTime.Now.AddHours(5));
                return searchContainer;
            }
            return null;
        }

        private SearchContainer<MovieResult> Custom(int page, int numberByPage) {
            throw new NotImplementedException();
        }

        private SearchContainer<MovieResult> NowPlaying(int page, int numberByPage) {
            var cacheHandler = CacheHandlerFactory.GetCacheHandler<SearchContainer<MovieResult>>(
                new Hashtable { { "Page", page }, { "Type", "NowPlaying" } });
            var searchContainer = cacheHandler.Get();
            if (searchContainer != null)
                return searchContainer;
            searchContainer = ResultProvider.NowPlaying(page);
            if (searchContainer != null) {
                cacheHandler.Add(searchContainer, DateTime.Now.AddHours(5));
                return searchContainer;
            }
            return null;
        }

        private SearchContainer<MovieResult> MyMovies(int page, int numberByPage) {
           var movies  = StoredProvider.GetAll(page);
           return Translator.MoviesStoredToSearchContainerOfMovieResult(movies, page, movies.Count / numberByPage, movies.Count);
        }

        public MovieStored Add(MovieStored movie) {
            return StoredProvider.Create(movie);
        }
    }
}
