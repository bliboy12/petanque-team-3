using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Storage {
    public class SpeeldagRepository(Id312896PetanqueContext dbContext) : ISpeeldagRepository {
        public Speeldag Create(Speeldag request) {
            throw new NotImplementedException();
        }

        public IEnumerable<Speeldag> GetAll() {
            throw new NotImplementedException();
        }

        public Speeldag GetById(int id) {
            throw new NotImplementedException();
        }
    }
}
