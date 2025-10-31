using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;

namespace Petanque.Storage {
    public class SeizoenRepository(Id312896PetanqueContext dbContext) : ISeizoenRepository {
        public Seizoen Create(Seizoen seizoen) {
            dbContext.Seizoens.Add(seizoen);
            dbContext.SaveChanges();

            return seizoen;
        }

        public IEnumerable<Seizoen> GetAll() {
            return dbContext.Seizoens
            .OrderByDescending(s => s.Startdatum) // Meest recente seizoenen eerst
            .ToList();
        }

        public Seizoen? GetOverlappendeSeizoenen(DateOnly startdatum, DateOnly einddatum) {
            return dbContext.Seizoens.FirstOrDefault(s => startdatum <= s.Einddatum && einddatum >= s.Startdatum);
        }
    }
}
