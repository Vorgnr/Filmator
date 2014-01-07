using Filmator.Model.Entities;
using Filmator.Model.Enums;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;

namespace Filmator.Model.Manager {
    public interface IMovieManager {
        SearchContainer<MovieResult> GetSearchByState(SearchState state, int page = 1, string name = "", int numberByPage = 20);
        Movie GetMovieById(int id );
        MovieInfo GetMovieInfoByRemoteId(int id);
        MovieInfo Add(MovieInfo movie);
    }
}
