using System;
using System.Configuration;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace Filmator.Model.Provider {
    public class SearchMovieProvider {
        readonly TMDbClient _clientApi;
        public SearchMovieProvider() {
            _clientApi = new TMDbClient(ConfigurationManager.AppSettings["ApiKey"]);
        }

        public Movie GetById(int id) {
            return _clientApi == null ? null : _clientApi.GetMovie(id, "fr", MovieMethods.Casts);
        }

        public SearchContainer<SearchMovie> GetSearchByMovieName(string name, int page) {
            return _clientApi == null ? null : _clientApi.SearchMovie(name, "fr", page);
        }

        public SearchContainer<MovieResult> GetByRankingType(int page, string type)
        {
            var movieType = (MovieListType)Enum.Parse(typeof(MovieListType), type, true);
            return _clientApi == null ? null : _clientApi.GetMovieList(movieType, "fr", page);
        }

        public SearchContainer<MovieResult> GetByGenreId(int genreId, int page) {
            return _clientApi == null ? null : _clientApi.GetGenreMovies(genreId, "fr", page);
        }
    }
}
