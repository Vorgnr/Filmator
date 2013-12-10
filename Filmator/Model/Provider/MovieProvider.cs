using Filmator.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmator.Model.Provider {
    public class MovieProvider {
        public MovieProvider()
        {
 
        }
 
        public List<MovieStored> GetAllMovies()
        {
            using (FilmatorContext context = new FilmatorContext())
            {
                return context.MoviesStored.ToList();
            }
        }
    }
}
