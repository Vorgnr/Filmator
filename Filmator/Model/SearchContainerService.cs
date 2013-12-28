using System;
using TMDbLib.Objects.General;

namespace Filmator.Model {
    public class SearchContainerService : ISearchContainerService {
        public void GetData(Action<SearchContainer<MovieResult>, Exception> callback) {
            callback(null, null);
        }
    }
}
