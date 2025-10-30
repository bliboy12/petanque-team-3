using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;

namespace Petanque.Storage {
    public class SpelverdelingRepository(Id312896PetanqueContext dbContext) : ISpelverdelingRepository {
        public IEnumerable<Spelverdeling> GetBySpelId(List<int> spelIds) {
            return dbContext.Spelverdelings
                .Where(sv => spelIds.Contains(sv.SpelId ?? 0))
                .ToList();
        }

        public void RemoveSpelverdelingen(List<Spelverdeling> spelverdelingen) {
            dbContext.Spelverdelings.RemoveRange(spelverdelingen);
            dbContext.SaveChanges();
        }

        public Spelverdeling Create(Spelverdeling spelverdeling) {
            dbContext.Spelverdelings.Add(spelverdeling);
            dbContext.SaveChanges();

            return spelverdeling;
        }
    }
}
