using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Storage {
    public class SpelerRepository(Id312896PetanqueContext dbContext) : ISpelerRepository {
        public Speler Create(Speler request) {
            dbContext.Spelers.Add(request);
            dbContext.SaveChanges();
            return request; // TODO bekijken of ID hier nu ook beschikbaar is op speler
        }

        public void Delete(int id) {
            var entity = GetById(id);
            if (entity is null) {
                throw new ArgumentException($"Lid met ID {id} werd niet gevonden");
            }
            dbContext.Spelers.Remove(entity);
            dbContext.SaveChanges();
        }

        public IEnumerable<Speler> GetAll() {
            return dbContext.Spelers.OrderBy(a => a.Naam).ThenBy(a => a.Voornaam).ToList();
        }

        public Speler? GetById(int id) {
            var entity = dbContext.Spelers.Find(id);
            return entity != null ? entity : null;
        }

        public void Update(int id, string voornaam, string naam) {
            var entity = GetById(id);
            if (entity is null) {
                throw new ArgumentException($"Lid met ID {id} werd niet gevonden");
            }
            entity.Voornaam = voornaam;
            entity.Naam = naam;
            dbContext.SaveChanges();
        }
    }
}
