using System.Configuration;
using Filmator.Model.Utils;
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
            return _clientApi == null ? null : _clientApi.GetMovie(id);
        }

        public SearchContainer<SearchMovie> GetSearchByMovieName(string name, int page) {
            return _clientApi == null ? null : _clientApi.SearchMovie(name, page);
        }

        public SearchContainer<MovieResult> Popular(int page) {
            return _clientApi == null ? null : _clientApi.GetMovieList(MovieListType.Popular, page);
        }

        public SearchContainer<MovieResult> NowPlaying(int page) {
            return _clientApi == null ? null : _clientApi.GetMovieList(MovieListType.NowPlaying, page);
        }

        public SearchContainer<MovieResult> TopRated(int page) {
            return _clientApi == null ? null : _clientApi.GetMovieList(MovieListType.TopRated, page);
        }

        public SearchContainer<MovieResult> GetByGenreId(int genreId, int page) {
            return _clientApi == null ? null : _clientApi.GetGenreMovies(genreId, page);
        }
    }
}
