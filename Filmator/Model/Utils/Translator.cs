using Filmator.Model.Entities;
using TMDbLib.Objects.Movies;

namespace Filmator.Model.Utils {
    public class Translator {
        public MovieStored MovieToMovieStored(Movie movie) {
            return new MovieStored {
                RemoteID = movie.Id,
                ImdbId = movie.ImdbId,
                Title = movie.Title,
                OriginalTitle = movie.OriginalTitle,
                Overview = movie.Overview,
                Homepage = movie.Homepage,
                Tagline = movie.Tagline,
                Status = movie.Status,
                Adult = movie.Adult
            };
        }
    }
}
