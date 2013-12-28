using System;
using TMDbLib.Objects.General;

namespace Filmator.Model {
    public interface ISearchContainerService {
        void GetData(Action<SearchContainer<MovieResult>, Exception> callback);
    }
}
