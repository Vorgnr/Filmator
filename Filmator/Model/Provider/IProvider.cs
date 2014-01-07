using System.Collections.Generic;

namespace Filmator.Model.Provider {
    public interface IProvider<T> {
        List<T> GetAll(int page);
        T Create(T obj);
        T GetById(int id);
        T GetByRemoteId(int remoteId);
        void Delete(T obj);
        T Update(T obj);
    }
}
