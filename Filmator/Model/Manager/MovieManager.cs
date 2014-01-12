using System;
using System.Collections;
using System.Reflection;
using Filmator.Model.Cache;
using Filmator.Model.Entities;
using Filmator.Model.Enums;
using Filmator.Model.Provider;
using Filmator.Model.Utils;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;

namespace Filmator.Model.Manager {
    public class MovieManager : IMovieManager {
        public SearchMovieProvider ResultProvider { get; private set; }
        public MovieInfoProvider MovieInfoProvider { get; private set; }
        public MovieManager() {
            MovieInfoProvider = new MovieInfoProvider();
            ResultProvider = new SearchMovieProvider();
        }

        public SearchContainer<MovieResult> GetSearchByState(SearchState state, int page = 1, string name = "", int numberByPage = 20) {
            var method = typeof(MovieManager).GetMethod(state.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
            if (method == null) {
                return new SearchContainer<MovieResult>();
            }
            var callback = FastDelegates.DelegateFactory.Create(method);
            return (SearchContainer<MovieResult>)callback(this, new object[] { page, numberByPage, name });
        }

        public Movie GetMovieById(int id) {
            var cacheHandler = CacheHandlerFactory.GetCacheHandler<Movie>();
            var movie = cacheHandler.Get(id);
            if (movie != null)
                return movie;
            movie = ResultProvider.GetById(id);
            if (movie != null) {
                cacheHandler.Add(movie, DateTime.Now.AddMonths(6));
                return movie;
            }
            return null;
        }

        private SearchContainer<MovieResult> TopRated(int page, int numberByPage, string name) {
            var cacheHandler = CacheHandlerFactory.GetCacheHandler<SearchContainer<MovieResult>>();
            var searchContainer = cacheHandler.Get(new Hashtable { { "Page", page }, { "State", SearchState.TopRated.ToString() } });
            if (searchContainer != null)
                return searchContainer;
            searchContainer = ResultProvider.TopRated(page);
            if (searchContainer != null) {
                cacheHandler.Add(searchContainer, DateTime.Now.AddHours(5), SearchState.TopRated.ToString());
                return searchContainer;
            }
            return null;
        }

        private SearchContainer<MovieResult> Popular(int page, int numberByPage, string name) {
            var cacheHandler = CacheHandlerFactory.GetCacheHandler<SearchContainer<MovieResult>>();
            var searchContainer = cacheHandler.Get(new Hashtable { { "Page", page }, { "State", SearchState.Popular.ToString() } });
            if (searchContainer != null)
                return searchContainer;
            searchContainer = ResultProvider.Popular(page);
            if (searchContainer != null) {
                cacheHandler.Add(searchContainer, DateTime.Now.AddHours(5), SearchState.Popular.ToString());
                return searchContainer;
            }
            return null;
        }

        private SearchContainer<MovieResult> NowPlaying(int page, int numberByPage, string name) {
            var cacheHandler = CacheHandlerFactory.GetCacheHandler<SearchContainer<MovieResult>>();
            var searchContainer = cacheHandler.Get(new Hashtable { { "Page", page }, { "State", SearchState.NowPlaying.ToString() } });
            if (searchContainer != null)
                return searchContainer;
            searchContainer = ResultProvider.NowPlaying(page);
            if (searchContainer != null) {
                cacheHandler.Add(searchContainer, DateTime.Now.AddHours(5), SearchState.NowPlaying.ToString());
                return searchContainer;
            }
            return null;
        }

        private SearchContainer<MovieResult> MyMovies(int page, int numberByPage, string name) {
            var movies = MovieInfoProvider.GetAll(page);
            return Translator.MovieInfosToSearchContainerOfMovieResult(movies, page, movies.Count / numberByPage, movies.Count);
        }

        private SearchContainer<MovieResult> Custom(int page, int numberByPage, string name) {
            if (name == "")
                return new SearchContainer<MovieResult>();
            var res = ResultProvider.GetSearchByMovieName(name, page);
            return Translator.SearchMoviesToSearchContainerOfMovieResult(res.Results, res.Page, res.TotalPages, res.TotalResults);
        }

        public MovieInfo Add(MovieInfo movie) {
            return MovieInfoProvider.Create(movie);
        }

        public MovieInfo GetMovieInfoByRemoteId(int id) {
            return MovieInfoProvider.GetByRemoteId(id);
        }


        public MovieInfo Update(MovieInfo movie) {
            return MovieInfoProvider.Update(movie);
        }
    }
}
