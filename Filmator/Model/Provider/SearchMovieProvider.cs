using System.Configuration;
using Filmator.Model.Entities;
using Filmator.Model.Utils;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace Filmator.Model.Provider {
    public class SearchMovieProvider {
        readonly TMDbClient _clientApi = null;
        public SearchMovieProvider() {
            _clientApi = new TMDbClient(ConfigurationManager.AppSettings["ApiKey"]);
        }

        public MovieStored GetById(int id) {
            if (_clientApi == null)
                return null;
            return Translator.RemoteMovieToMovieStored(_clientApi.GetMovie(id));
        }

        public SearchContainer<SearchMovie> GetSearchByMovieName(string name, int page) {
            if (_clientApi == null)
                return null;
            return _clientApi.SearchMovie(name, page);
        }

        public SearchContainer<MovieResult> Popular(int page) {
            if (_clientApi == null)
                return null;
            return _clientApi.GetMovieList(MovieListType.Popular, page);
        }

        public SearchContainer<MovieResult> NowPlaying(int page) {
            if (_clientApi == null)
                return null;
            return _clientApi.GetMovieList(MovieListType.NowPlaying, page);
        }

        public SearchContainer<MovieResult> TopRated(int page) {
            if (_clientApi == null)
                return null;
            return _clientApi.GetMovieList(MovieListType.TopRated, page);
        }

        public SearchContainer<MovieResult> GetByGenreId(int genreId, int page) {
            if (_clientApi == null)
                return null;
            return _clientApi.GetGenreMovies(genreId, page);
        }
    }
}
