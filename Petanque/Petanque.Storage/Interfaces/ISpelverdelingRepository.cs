using Microsoft.EntityFrameworkCore;
using Petanque.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Storage.Interfaces {
    public interface ISpelverdelingRepository {
        public IEnumerable<Spelverdeling> GetById(int speeldagId);
        public IEnumerable<Spelverdeling> MaakVerdeling(IEnumerable<Spelverdeling> aanwezigheden, int speeldagId);
        public IEnumerable<Spelverdeling> GetBySpeeldagAndTerrein(int speeldag, int terrein);
    }
}
