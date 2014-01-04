using System.Collections.Generic;
using TMDbLib.Objects.General;

namespace Filmator.Model.Manager {
    public interface IGenreManager {
        List<Genre> GetAll();
        SearchContainer<MovieResult> GetSearchContainerByGenreId(int id, int page);
    }
}
