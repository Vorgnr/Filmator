using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmator.Model.Provider
{
    interface IProvider<T>
    {
        List<T> GetAll();
        T Create(T obj);
        T Get(string name);
        List<T> Find(string name);
        void Delete(T obj);
        T Update(T obj);
    }
}
