using System;
using System.Collections.Generic;

namespace Filmator.Model.Cache {
    public interface ICache<T> {
        bool Exist(T obj);
        List<T> GetAll();
        T Get(object arg = null);
        void Add(T obj, DateTime expirationDate);
    }
}
