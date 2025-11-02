using Microsoft.EntityFrameworkCore;
using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;

namespace Petanque.Storage {
    public class DagKlassementRepository(Id312896PetanqueContext dbContext) : IDagKlassementRepository {
        public Dagklassement Create(Dagklassement dagklassement) {
            dbContext.Dagklassements.Add(dagklassement);
            dbContext.SaveChanges();

            return dagklassement;
        }

        public IEnumerable<Dagklassement> CreateDagKlassementen(List<Dagklassement> dagklassementen, int speeldagId) {
            // TODO overgenomen uit service klassse maar is dit correct?? waarom hier add range en in try catch nog eens add range?

            dbContext.AddRange(dagklassementen);
            dbContext.SaveChanges();

            using var transaction = dbContext.Database.BeginTransaction();
            try {
                dbContext.Dagklassements
                    .Where(dk => dk.SpeeldagId == speeldagId)
                    .ExecuteDelete();

                dbContext.AddRange(dagklassementen);
                dbContext.SaveChanges();
                transaction.Commit();
            }
            catch {
                transaction.Rollback();
                throw;
            }
            return dagklassementen;
        }

        public IEnumerable<Dagklassement>? GetById(int id) {
            var dagklassementen = dbContext.Dagklassements
            .Where(d => d.SpeeldagId == id)
            .ToList();

            return dagklassementen;
        }
    }
}
