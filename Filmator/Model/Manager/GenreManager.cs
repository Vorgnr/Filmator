using System.Collections.Generic;
using Filmator.Model.Provider;
using TMDbLib.Objects.General;

namespace Filmator.Model.Manager {
    public class GenreManager : IGenreManager {
        public List<Genre> GetAll() {
            return new GenreProvider().GetAll();
        }


        public SearchContainer<MovieResult> GetSearchContainerByGenreId(int id, int page) {
            return new SearchMovieProvider().GetByGenreId(id, page);
        }
    }
}
