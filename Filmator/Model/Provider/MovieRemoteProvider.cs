using System.Linq;
using Filmator.Model.Entities;
using System.Collections.Generic;
using System.Configuration;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;
using Filmator.Model.Utils;

namespace Filmator.Model.Provider {
    public class MovieRemoteProvider {
        readonly TMDbClient _clientApi = null;
        public MovieRemoteProvider() {
            _clientApi = new TMDbClient(ConfigurationManager.AppSettings["ApiKey"]);
        }

        public MovieStored GetById(int id) {
            return Translator.RemoteMovieToMovieStored(_clientApi.GetMovie(id));
        }

        public List<MovieStored> SearchByName(string searchString) {
            var results = _clientApi.SearchMovie(searchString);
            return results.Results.Select(result => GetById(result.Id)).ToList();
        }

        public List<MovieStored> GetPopular(int page) {
            var results = _clientApi.GetMovieList(MovieListType.Popular, page);
            return results.Results.Select(result => GetById(result.Id)).ToList();
        }

        public List<MovieStored> GetNowPlaying(int page) {
            var results = _clientApi.GetMovieList(MovieListType.NowPlaying, page);
            return results.Results.Select(result => GetById(result.Id)).ToList();
        }

        public List<MovieStored> GetTopRated(int page) {
            var results = _clientApi.GetMovieList(MovieListType.TopRated, page);
            return results.Results.Select(result => GetById(result.Id)).ToList();
        }
    }
}
