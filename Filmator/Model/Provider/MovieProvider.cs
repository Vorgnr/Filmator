using Filmator.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Filmator.Model.Provider {
    public class MovieProvider {
        public List<MovieStored> GetAllMovies() {
            using (var context = new FilmatorContext()) {
                return context.MoviesStored.ToList();
            }
        }
    }
}
