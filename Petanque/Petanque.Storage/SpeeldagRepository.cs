using Microsoft.EntityFrameworkCore;
using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;

namespace Petanque.Storage {
    public class SpeeldagRepository(Id312896PetanqueContext dbContext) : ISpeeldagRepository {
        public Speeldag Create(Speeldag speeldag) {
            dbContext.Speeldags.Add(speeldag);
            dbContext.SaveChanges();

            return speeldag;
        }

        public IEnumerable<Speeldag> GetAll() {
            return dbContext.Speeldags.Include(s => s.Seizoens)
                            .Include(s => s.Spels)
                            .ToList();
        }

        public Speeldag? GetById(int id) {
            return dbContext.Speeldags
                .Include(s => s.Seizoens)
                .Include(s => s.Spels)
                .FirstOrDefault(s => s.SpeeldagId == id);
        }
    }
}
