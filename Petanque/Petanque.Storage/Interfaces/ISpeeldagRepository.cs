using Microsoft.EntityFrameworkCore;
using Petanque.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Storage.Interfaces {
    public interface ISpeeldagRepository {
        public Speeldag Create(Speeldag request);
        public Speeldag GetById(int id);
        public IEnumerable<Speeldag> GetAll();
    }
}
