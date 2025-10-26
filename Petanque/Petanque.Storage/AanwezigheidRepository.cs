using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Storage {
    public class AanwezigheidRepository(Id312896PetanqueContext dbContext) : IAanwezigheidRepository {
        public Aanwezigheid Create(Aanwezigheid aanwezigheid) {
            throw new NotImplementedException();
        }

        public void DeleteAanwezigheid(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Aanwezigheid> GetAanwezighedenOpSpeeldag(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Aanwezigheid> GetAanwezighedenOpSpeler(int spelerId) {
            throw new NotImplementedException();
        }

        public IEnumerable<Aanwezigheid> GetAll() {
            throw new NotImplementedException();
        }

        public Aanwezigheid? GetById(int id) {
            throw new NotImplementedException();
        }
    }
}
