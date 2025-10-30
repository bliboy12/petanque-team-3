using Microsoft.EntityFrameworkCore;
using Petanque.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Storage.Interfaces {
    public interface ISpelverdelingRepository {
        public IEnumerable<Spelverdeling> GetBySpelId(List<int> spelIds);
        void RemoveSpelverdelingen(List<Spelverdeling> spellen);
        Spelverdeling Create(Spelverdeling spelverdeling);
    }
}
