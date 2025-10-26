using Petanque.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Storage.Interfaces {
    public interface IDagKlassementRepository {
        public IEnumerable<Dagklassement> GetDagklassementOpSpeeldag(int id);
        public Dagklassement Create(Dagklassement request);
        public IEnumerable<Dagklassement>? GetById(int id);
        public IEnumerable<Dagklassement> CreateDagKlassementen(Speeldag speeldagData, int id);
    }
}
