using Filmator.Model.Entities;
using System.Collections.Generic;
using System.Configuration;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using Filmator.Model.Utils;

namespace Filmator.Model.Provider
{
    public class MovieRemoteProvider
    {
        TMDbClient _clientAPI = null;
        public MovieRemoteProvider()
        {
            _clientAPI = new TMDbClient(ConfigurationManager.AppSettings["ApiKey"]);
        }

        public MovieStored GetById(int id)
        {
            return Translator.RemoteMovieToMovieStored(_clientAPI.GetMovie(id));
        }

        public List<MovieStored> SearchByName(string searchString)
        {
            List<MovieStored> movies = new List<MovieStored>();
            SearchContainer<SearchMovie> results = _clientAPI.SearchMovie(searchString);
            foreach (var result in results.Results) {
                movies.Add(GetById(result.Id));
            }
            return movies;
        }

        public List<MovieStored> GetMostPopular(int page)
        {
            List<MovieStored> movies = new List<MovieStored>();
            SearchContainer<MovieResult> results = _clientAPI.GetMovieList(MovieListType.Popular, page);
            foreach (var result in results.Results)
            {
                movies.Add(GetById(result.Id));
            }
            return movies;
        }

        public List<MovieStored> GetNowPlaying(int page)
        {
            List<MovieStored> movies = new List<MovieStored>();
            SearchContainer<MovieResult> results = _clientAPI.GetMovieList(MovieListType.NowPlaying, page);
            foreach (var result in results.Results)
            {
                movies.Add(GetById(result.Id));
            }
            return movies;
        }

        public List<MovieStored> GetTopRated(int page)
        {
            List<MovieStored> movies = new List<MovieStored>();
            SearchContainer<MovieResult> results = _clientAPI.GetMovieList(MovieListType.TopRated, page);
            foreach (var result in results.Results)
            {
                movies.Add(GetById(result.Id));
            }
            return movies;
        }
    }
}
