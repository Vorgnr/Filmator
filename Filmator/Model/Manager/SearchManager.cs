using System;
using System.Collections.Generic;
using System.Reflection;
using Filmator.Model.Entities;
using Filmator.Model.Enums;
using Filmator.Model.Provider;
using Filmator.Model.Utils;

namespace Filmator.Model.Manager {
    public class SearchManager : ISearchManager {
        public MovieRemoteProvider RemoteProvider { get; private set; }
        public MovieStoredProvider StoredProvider { get; private set; }
        public SearchManager() {
            StoredProvider = new MovieStoredProvider();
            RemoteProvider = new MovieRemoteProvider();
        }

        public List<MovieStored> GetSearchByState(SearchState state) {
            var method = typeof(SearchManager).GetMethod(state.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
            if (method == null) {
                return new List<MovieStored>();
            }
            var callback = FastDelegates.DelegateFactory.Create(method);

            return (List<MovieStored>)callback(this, null);
        }

        private List<MovieStored> TopRated() {
            return RemoteProvider.GetTopRated(1);
        }

        private List<MovieStored> Popular() {
            return RemoteProvider.GetPopular(1);
        }

        private List<MovieStored> Custom() {
            throw new NotImplementedException();
        }

        private List<MovieStored> NowPlaying() {
            return RemoteProvider.GetNowPlaying(1);
        }

        private List<MovieStored> MyMovies() {
            throw new NotImplementedException();
        } 
    }
}
