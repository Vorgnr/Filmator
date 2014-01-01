using Filmator.Model.Entities;
using Filmator.Model.Enums;
using TMDbLib.Objects.General;

namespace Filmator.Model.Manager {
    public interface IMovieManager {
        SearchContainer<MovieResult> GetSearchByState(SearchState state, int page = 1, int numberByPage = 20);
        MovieStored GetMovieStoredById(int id );
        MovieStored Add(MovieStored movie);
    }
}
