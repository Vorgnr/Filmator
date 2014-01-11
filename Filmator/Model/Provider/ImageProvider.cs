using System;
using System.Configuration;
using TMDbLib.Client;
using TMDbLib.Objects.General;

namespace Filmator.Model.Provider {
    public class ImageProvider {
        readonly TMDbClient _clientApi;
        public ImageProvider() {
            _clientApi = new TMDbClient(ConfigurationManager.AppSettings["ApiKey"]);
        }

        public Uri GetFullPosterPath(string poster, string size) {
            var config = new TMDbConfig {
                Images = new ConfigImageTypes {
                    BaseUrl = "http://image.tmdb.org/t/p/w"
                }
            };
            _clientApi.SetConfig(config);
            return _clientApi == null ? null : _clientApi.GetImageUrl(size, poster);
        }
    }
}
