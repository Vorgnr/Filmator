using System.Data.Entity;
using Filmator.Model.Entities;

namespace Filmator.Model {
    class FilmatorContext : DbContext {
        public DbSet<MovieStored> MoviesStored { get; set; }
    }
}