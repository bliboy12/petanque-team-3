using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Storage {
    public class SpelverdelingRepository(Id312896PetanqueContext dbContext) : ISpelverdelingRepository {
        public IEnumerable<Spelverdeling> GetById(int speeldagId) {
            throw new NotImplementedException();
        }

        public IEnumerable<Spelverdeling> GetBySpeeldagAndTerrein(int speeldag, int terrein) {
            throw new NotImplementedException();
        }

        public IEnumerable<Spelverdeling> MaakVerdeling(IEnumerable<Spelverdeling> aanwezigheden, int speeldagId) {
            throw new NotImplementedException();
        }
    }
}
