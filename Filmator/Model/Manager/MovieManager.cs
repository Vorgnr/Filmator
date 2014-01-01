using System;
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
        public ICache<MovieStored> CacheHandler { get; private set; }
        public MovieStoredProvider StoredProvider { get; private set; }
        public MovieManager() {
            StoredProvider = new MovieStoredProvider();
            ResultProvider = new SearchMovieProvider();
            CacheHandler = new CacheHandler();
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
            var movie = CacheHandler.Get(id);
            if (movie != null)
                return movie;
            movie = ResultProvider.GetById(id);
            if (movie != null) {
                CacheHandler.Add(movie, DateTime.Now.AddMonths(6));
                return ResultProvider.GetById(id);
            }
            return null;
        }

        private SearchContainer<MovieResult> TopRated(int page, int numberByPage) {
            return ResultProvider.TopRated(page);
        }

        private SearchContainer<MovieResult> Popular(int page, int numberByPage) {
            return ResultProvider.Popular(page);
        }

        private SearchContainer<MovieResult> Custom(int page, int numberByPage) {
            throw new NotImplementedException();
        }

        private SearchContainer<MovieResult> NowPlaying(int page, int numberByPage) {
            return ResultProvider.NowPlaying(page);
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
