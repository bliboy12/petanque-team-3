using Petanque.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Storage.Interfaces {
    public interface ISpelerRepository {
        public Speler Create(Speler request);

        public Speler? GetById(int id);
        public IEnumerable<Speler> GetAll();

        public void Update(int id, string voornaam, string naam);

        public void Delete(int id);
    }
}
