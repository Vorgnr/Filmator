using TMDbLib.Objects.Movies;

namespace Filmator.Model.Entities {
    class MovieStored : Movie {
        public bool Seen { get; set; }
    }
}
