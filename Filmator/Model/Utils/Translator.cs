using System.Collections.Generic;
using System.Linq;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace Filmator.Model.Utils {
    public class Translator {
        //public static MovieStored RemoteMovieToMovieStored(Movie movie) {
        //    return new MovieStored {
        //        RemoteID = movie.Id,
        //        ImdbId = movie.ImdbId,
        //        Title = movie.Title,
        //        OriginalTitle = movie.OriginalTitle,
        //        Overview = movie.Overview,
        //        Homepage = movie.Homepage,
        //        Tagline = movie.Tagline,
        //        Status = movie.Status,
        //        Adult = movie.Adult
        //    };
        //}

        //public static MovieResult MovieStoredToMovieResult(MovieStored movie) {
        //    return new MovieResult {
        //        Id = movie.RemoteID,
        //        Title = movie.Title
        //    };
        //}

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

        //public static SearchContainer<MovieResult> MoviesStoredToSearchContainerOfMovieResult(List<MovieStored> movies, int page = 0, int totalPages = 0, int totalResults = 0) {
        //    var moviesResult = movies.Select(MovieStoredToMovieResult).ToList();
        //    return new SearchContainer<MovieResult> {
        //        Page = page,
        //        TotalPages = totalPages,
        //        TotalResults = totalResults,
        //        Results = moviesResult
        //    };
        //}
    }
}
