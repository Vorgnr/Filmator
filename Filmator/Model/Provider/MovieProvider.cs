using Filmator.Model.Entities;
using System.Collections.Generic;
using System.Linq;
using TMDbLib.Objects.Movies;

namespace Filmator.Model.Provider {
    public class MovieProvider {
        public List<MovieStored> GetAllMovies() {
            using (var context = new FilmatorContext()) {
                return context.MoviesStored.ToList();
            }
        }

        public MovieStored Create(MovieStored movie)
        {
            using (var context = new FilmatorContext())
            {
                return context.MoviesStored.Add(movie);
            }
        }
    }
}
