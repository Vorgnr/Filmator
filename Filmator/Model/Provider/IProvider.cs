using System.Collections.Generic;

namespace Filmator.Model.Provider {
    public interface IProvider<T> {
        List<T> GetAll();
        List<T> GetAll(int page);
        T Create(T obj);
        T GetByName(string name);
        T GetById(int id);
        T GetByRemoteId(int remoteId);
        List<T> Find(string name);
        void Delete(T obj);
        T Update(T obj);
    }
}
