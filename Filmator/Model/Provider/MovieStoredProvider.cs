using Filmator.Model.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TMDbLib.Objects.Movies;

namespace Filmator.Model.Provider {
    public class MovieStoredProvider : IProvider<MovieStored>{

        public List<MovieStored> GetAll()
        {
            List<MovieStored> movies;
            using (var context = new FilmatorContext())
            {
                movies = context.MoviesStored.ToList();
                context.SaveChanges();
            }
            return movies;
        }

        public MovieStored Create(MovieStored obj)
        {
            MovieStored movie;
            using (var context = new FilmatorContext())
            {
                movie = context.MoviesStored.Add(obj);
                context.SaveChanges();
            }
            return movie;
        }

        public MovieStored Get(string name)
        {
            MovieStored movie;
            using (var context = new FilmatorContext())
            {
                movie = context.MoviesStored.Where(m => m.Title.Contains(name)).First();
                context.SaveChanges();
            }
            return movie;
        }

        public List<MovieStored> Find(string name)
        {
            List<MovieStored> movies;
            using (var context = new FilmatorContext())
            {
                movies =  context.MoviesStored.Where(m => m.Title.Contains(name)).ToList();
                context.SaveChanges();
            }
            return movies;
        }

        public void Delete(MovieStored obj)
        {
            using (var context = new FilmatorContext())
            {
                context.MoviesStored.Remove(obj);
                context.SaveChanges();
            }
        }

        public MovieStored Update(MovieStored obj)
        {
            MovieStored movie;
            using (var context = new FilmatorContext())
            {
                context.MoviesStored.Attach(obj);
                context.Entry(obj).State = EntityState.Modified;
                context.SaveChanges();
                movie = context.Entry(obj).Entity;
            }
            return movie;
        }
    }
}
