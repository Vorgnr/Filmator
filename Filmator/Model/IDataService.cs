using System;

namespace Filmator.Model {
    public interface IDataService {
        void GetData(Action<DataItem, Exception> callback);
    }
}
