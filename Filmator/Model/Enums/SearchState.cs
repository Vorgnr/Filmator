using System.ComponentModel;

namespace Filmator.Model.Enums {
    public enum SearchState {
        [Description("Popular")]
        Popular,
        [Description("TopRated")]
        TopRated,
        [Description("NowPlaying")]
        NowPlaying,
        [Description("MyMovies")]
        MyMovies,
        [Description("Custom")]
        Custom
    }
}
