using System.Collections.Generic;
using System.Configuration;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace Filmator.Model.Provider
{
    public class MovieRemoteProvider
    {
        TMDbClient _clientAPI = null;
        public MovieRemoteProvider()
        {
            _clientAPI = new TMDbClient(ConfigurationManager.AppSettings["ApiKey"]);
        }

        public Movie GetById(int id)
        {
            return _clientAPI.GetMovie(id);
        }

        public List<Movie> SearchByName(string searchString)
        {
            List<Movie> movies = new List<Movie>();
            SearchContainer<SearchMovie> results = _clientAPI.SearchMovie(searchString);
            foreach (var result in results.Results) {
                movies.Add(GetById(result.Id));
            }
            return movies;
        }

        public List<Movie> GetMostPopular(int page)
        {
            List<Movie> movies = new List<Movie>();
            SearchContainer<MovieResult> results = _clientAPI.GetMovieList(MovieListType.Popular, page);
            foreach (var result in results.Results)
            {
                movies.Add(GetById(result.Id));
            }
            return movies;
        }

        public List<Movie> GetNowPlaying(int page)
        {
            List<Movie> movies = new List<Movie>();
            SearchContainer<MovieResult> results = _clientAPI.GetMovieList(MovieListType.NowPlaying, page);
            foreach (var result in results.Results)
            {
                movies.Add(GetById(result.Id));
            }
            return movies;
        }

        public List<Movie> GetTopRated(int page)
        {
            List<Movie> movies = new List<Movie>();
            SearchContainer<MovieResult> results = _clientAPI.GetMovieList(MovieListType.TopRated, page);
            foreach (var result in results.Results)
            {
                movies.Add(GetById(result.Id));
            }
            return movies;
        }
    }
}
