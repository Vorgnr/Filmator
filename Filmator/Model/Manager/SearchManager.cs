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
       // public MovieStoredProvider StoredProvider { get; private set; }
        public SearchManager() {
            //StoredProvider = new MovieStoredProvider();
            ResultProvider = new SearchMovieProvider();
        }

        public SearchContainer<MovieResult> GetSearchByState(SearchState state) {
            var method = typeof(SearchManager).GetMethod(state.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
            if (method == null) {
                return new SearchContainer<MovieResult>();
            }
            var callback = FastDelegates.DelegateFactory.Create(method);

            return (SearchContainer<MovieResult>)callback(this, null);
        }

        public MovieStored GetMovieStoredById(int id) {
            return ResultProvider.GetById(id);
        }

        private SearchContainer<MovieResult> TopRated() {
            return ResultProvider.TopRated(1);
        }

        private SearchContainer<MovieResult> Popular() {
            return ResultProvider.Popular(1);
        }

        private SearchContainer<MovieResult> Custom() {
            throw new NotImplementedException();
        }

        private SearchContainer<MovieResult> NowPlaying() {
            return ResultProvider.NowPlaying(1);
        }

        private SearchContainer<MovieResult> MyMovies() {
            throw new NotImplementedException();
        }
    }
}
