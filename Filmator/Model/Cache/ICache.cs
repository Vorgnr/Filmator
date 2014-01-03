using System;

namespace Filmator.Model.Cache {
    public interface ICache<T> {
        T Get(object id);
        void Add(T obj, DateTime expirationDate, string state = "");
    }
}
