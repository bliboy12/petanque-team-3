using Microsoft.EntityFrameworkCore;
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
            dbContext.Aanwezigheids.Add(aanwezigheid);
            dbContext.SaveChanges();

            return aanwezigheid;
        }

        public void DeleteAanwezigheid(int id) {
            var entity = dbContext.Aanwezigheids.Find(id);
            if (entity == null) {
                throw new ArgumentException($"Aanwezigheid met ID {id} werd niet gevonden.");
            }

            dbContext.Aanwezigheids.Remove(entity);
            dbContext.SaveChanges();
        }

        public IEnumerable<Aanwezigheid> GetAanwezighedenOpSpeeldag(int id) {
            return dbContext.Aanwezigheids.Where(s => s.SpeeldagId == id).ToList();
        }

        public IEnumerable<Aanwezigheid> GetAanwezighedenOpSpeler(int spelerId) {
            return dbContext.Aanwezigheids
               .Include(a => a.Speeldag)
               .Where(a => a.SpelerId == spelerId)
               .ToList();
        }

        public IEnumerable<Aanwezigheid> GetAll() {
            return dbContext.Aanwezigheids.ToList();
        }

        public Aanwezigheid? GetById(int id) {
            var entity = dbContext.Aanwezigheids.Find(id);
            return entity != null ? entity : null;
        }
    }
}
