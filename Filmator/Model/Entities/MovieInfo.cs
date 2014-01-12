using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmator.Model.Entities {
    public class MovieInfo {
        public int ID { get; set; }
        public string Title { get; set; }
        public int RemoteId { get; set; }
        public bool IsSeen { get; set; }
        public bool IsOwned { get; set; }
        public double Rate { get; set; }
    }
}
