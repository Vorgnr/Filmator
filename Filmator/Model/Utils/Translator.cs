using System.Collections.Generic;
using System.Linq;
using Filmator.Model.Entities;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace Filmator.Model.Utils {
    public class Translator {
        public static MovieResult MovieInfoToMovieResult(MovieInfo movie) {
            return new MovieResult {
                Id = movie.RemoteId,
                Title = movie.Title
            };
        }

        public static MovieResult SearchMovieToMovieResult(SearchMovie movie) {
            return new MovieResult {
                Id = movie.Id,
                Title = movie.Title
            };
        }

        public static SearchContainer<MovieResult> SearchMoviesToSearchContainerOfMovieResult(List<SearchMovie> movies, int page = 0, int totalPages = 0, int totalResults = 0) {
            var moviesResult = movies.Select(SearchMovieToMovieResult).ToList();
            return new SearchContainer<MovieResult> {
                Page = page,
                TotalPages = totalPages,
                TotalResults = totalResults,
                Results = moviesResult
            };
        }

        public static SearchContainer<MovieResult> MovieInfosToSearchContainerOfMovieResult(List<MovieInfo> movies, int page = 0, int totalPages = 0, int totalResults = 0) {
            var moviesResult = movies.Select(MovieInfoToMovieResult).ToList();
            return new SearchContainer<MovieResult> {
                Page = page,
                TotalPages = totalPages,
                TotalResults = totalResults,
                Results = moviesResult
            };
        }
    }
}
