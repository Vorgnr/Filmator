using System.Collections.Generic;
using System.Linq;
using Filmator.Model.Entities;

namespace Filmator.Model.Provider {
    public class MovieInfoProvider : IProvider<MovieInfo> {

        public List<MovieInfo> GetAll(int page) {
            using (var context = new FilmatorContext()) {
                return context.MovieInfos
                    .OrderBy(m => m.Title)
                    .Skip((page - 1) * 20)
                    .Take(20)
                    .ToList();
            }
        }

        public MovieInfo Create(MovieInfo obj) {
            MovieInfo movie;
            using (var context = new FilmatorContext()) {
                movie = context.MovieInfos.Add(obj);
                context.SaveChanges();
            }
            return movie;
        }

        public MovieInfo GetByRemoteId(int remoteId) {
            using (var context = new FilmatorContext()) {
                return context.MovieInfos.FirstOrDefault(m => m.RemoteId == remoteId);
            }
        }

        //public MovieStored GetByName(string name) {
        //    using (var context = new FilmatorContext()) {
        //        return context.MoviesStored.FirstOrDefault(m => m.Title.Contains(name));
        //    }
        //}

        //public MovieStored GetById(int id) {
        //    using (var context = new FilmatorContext()) {
        //        return context.MoviesStored.FirstOrDefault(m => m.ID == id);
        //    }
        //}



        //public List<MovieStored> Find(string name) {
        //    List<MovieStored> movies;
        //    using (var context = new FilmatorContext()) {
        //        movies = context.MoviesStored.Where(m => m.Title.Contains(name)).ToList();
        //    }
        //    return movies;
        //}

        //public void Delete(MovieStored obj) {
        //    using (var context = new FilmatorContext()) {
        //        context.MoviesStored.Attach(obj);
        //        context.MoviesStored.Remove(obj);
        //        context.SaveChanges();
        //    }
        //}

        //public MovieStored Update(MovieStored obj) {
        //    using (var context = new FilmatorContext()) {
        //        context.MoviesStored.Attach(obj);
        //        context.Entry(obj).State = EntityState.Modified;
        //        context.SaveChanges();
        //        return context.Entry(obj).Entity;
        //    }
        //}

        public MovieInfo GetById(int id) {
            throw new System.NotImplementedException();
        }

        public void Delete(MovieInfo obj) {
            throw new System.NotImplementedException();
        }

        public MovieInfo Update(MovieInfo obj) {
            throw new System.NotImplementedException();
        }
    }
}
