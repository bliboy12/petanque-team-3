using Petanque.Storage;
using Petanque.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Petanque.Storage.Interfaces {
    public interface IAanwezigheidRepository {

        public Aanwezigheid Create(Aanwezigheid aanwezigheid);
        public Aanwezigheid? GetById(int id);
        public IEnumerable<Aanwezigheid> GetAll();
        public IEnumerable<Aanwezigheid> GetAanwezighedenOpSpeeldag(int id);
        public void DeleteAanwezigheid(int id);
        public IEnumerable<Aanwezigheid> GetAanwezighedenOpSpeler(int spelerId);
    }
}
