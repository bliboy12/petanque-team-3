using Microsoft.EntityFrameworkCore;
using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;

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
            return dbContext.Aanwezigheids.Include(a => a.Speler).Where(s => s.SpeeldagId == id).ToList();
        }

        public IEnumerable<Aanwezigheid> GetAanwezighedenOpSpeler(int spelerId) {
            
            var t = dbContext.Aanwezigheids
                .Include(a => a.Speeldag)
                .Where(a => a.SpelerId == spelerId)
                .ToList();

            return t;
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
