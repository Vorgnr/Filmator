using System.Collections.Generic;
using System.Configuration;
using TMDbLib.Client;
using TMDbLib.Objects.General;

namespace Filmator.Model.Provider {
    public class GenreProvider {
        readonly TMDbClient _clientApi = null;
        public GenreProvider() {
            _clientApi = new TMDbClient(ConfigurationManager.AppSettings["ApiKey"]);
        }
        public List<Genre> GetAll() {
            return _clientApi.GetGenres();
        } 
    }
}
