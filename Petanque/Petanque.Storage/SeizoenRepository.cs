using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Storage {
    public class SeizoenRepository(Id312896PetanqueContext dbContext) : ISeizoenRepository {
        public Seizoen Create(Seizoen request) {
            throw new NotImplementedException();
        }

        public IEnumerable<Seizoen> GetAll() {
            throw new NotImplementedException();
        }
    }
}
