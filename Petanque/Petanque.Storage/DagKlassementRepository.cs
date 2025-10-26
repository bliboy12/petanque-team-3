using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Storage {
    public class DagKlassementRepository(Id312896PetanqueContext dbContext) : IDagKlassementRepository {
        public Dagklassement Create(Dagklassement request) {
            throw new NotImplementedException();
        }

        public IEnumerable<Dagklassement> CreateDagKlassementen(Speeldag speeldagData, int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Dagklassement>? GetById(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Dagklassement> GetDagklassementOpSpeeldag(int id) {
            throw new NotImplementedException();
        }
    }
}
