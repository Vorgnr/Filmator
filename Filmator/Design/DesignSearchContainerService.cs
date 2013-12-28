using System;
using System.Collections.Generic;
using Filmator.Model;
using TMDbLib.Objects.General;

namespace Filmator.Design {
    public class DesignSearchContainerService : ISearchContainerService {
        public void GetData(Action<SearchContainer<MovieResult>, Exception> callback) {
            var movieList = new List<MovieResult> {
                new MovieResult {
                    Title = "un titre",
                    Id = 4,
                    VoteAverage = 7.5,
                    VoteCount = 125,
                    PosterPath = "bla",

                },
                new MovieResult {
                    Title = "un titre 2",
                    Id = 5,
                    VoteAverage = 9,
                    VoteCount = 225,
                    PosterPath = "bla",

                }
            };
            var item = new SearchContainer<MovieResult> {
                Results = movieList,
                TotalResults = 20
            };
            callback(item, null);
        }
    }
}
