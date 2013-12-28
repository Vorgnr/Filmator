﻿using Filmator.Model.Entities;
using Filmator.Model.Enums;
using TMDbLib.Objects.General;

namespace Filmator.Model.Manager {
    public interface ISearchManager {
        SearchContainer<MovieResult> GetSearchByState(SearchState state);
        MovieStored GetMovieStoredById(int id);
    }
}
