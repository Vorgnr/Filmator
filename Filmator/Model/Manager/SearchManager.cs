using System;
using System.Reflection;
using Filmator.Model.Entities;
using Filmator.Model.Enums;
using Filmator.Model.Provider;
using Filmator.Model.Utils;
using TMDbLib.Objects.General;

namespace Filmator.Model.Manager {
    public class SearchManager : ISearchManager {
        public SearchMovieProvider ResultProvider { get; private set; }
        public MovieStoredProvider StoredProvider { get; private set; }
        public SearchManager() {
            StoredProvider = new MovieStoredProvider();
            ResultProvider = new SearchMovieProvider();
        }

        public SearchContainer<MovieResult> GetSearchByState(SearchState state, int page = 1, int numberByPage = 20) {
            var method = typeof(SearchManager).GetMethod(state.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
            if (method == null) {
                return new SearchContainer<MovieResult>();
            }
            var callback = FastDelegates.DelegateFactory.Create(method);
            return (SearchContainer<MovieResult>)callback(this , new object[] {page, numberByPage});
        }

        public MovieStored GetMovieStoredById(int id) {
            return ResultProvider.GetById(id);
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
    }
}
