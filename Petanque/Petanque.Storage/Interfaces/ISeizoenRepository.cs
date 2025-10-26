using Petanque.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Storage.Interfaces {
    public interface ISeizoenRepository {
        public IEnumerable<Seizoen> GetAll();
        public Seizoen Create(Seizoen request);
        
    }
}
