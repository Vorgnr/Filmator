using System.Collections.Generic;
using Filmator.Model.Entities;
using Filmator.Model.Enums;

namespace Filmator.Model.Manager {
    public interface ISearchManager {
        List<MovieStored> GetSearchByState(SearchState state);
    }
}
