using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Filmator.Model.Provider {
    public class MovieStoredProvider : IProvider<object> {
        //public List<MovieStored> GetAll() {
        //    using (var context = new FilmatorContext()) {
        //        var movies = context.MoviesStored;
        //        return movies.ToList();
        //    }
        //}

        //public List<MovieStored> GetAll(int page) {
        //    using (var context = new FilmatorContext()) {
        //        return context.MoviesStored
        //            .OrderBy(m => m.Title)
        //            .Skip((page-1)*20)
        //            .Take(20)
        //            .ToList();
        //    }
        //}

        //public MovieStored Create(MovieStored obj) {
        //    MovieStored movie;
        //    using (var context = new FilmatorContext()) {
        //        movie = context.MoviesStored.Add(obj);
        //        context.SaveChanges();
        //    }
        //    return movie;
        //}

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

        //public MovieStored GetByRemoteId(int remoteId) {
        //    using (var context = new FilmatorContext()) {
        //        return context.MoviesStored.FirstOrDefault(m => m.RemoteID == remoteId);
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
    }
}
