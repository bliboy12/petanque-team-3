using Microsoft.EntityFrameworkCore;
using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;

namespace Petanque.Storage {
    public class SpelverdelingRepository(Id312896PetanqueContext dbContext) : ISpelverdelingRepository {
        public IEnumerable<Spelverdeling> GetBySpelIds(List<int> spelIds) {
            return dbContext.Spelverdelings.Include(sv => sv.Speler)
                .Where(sv => spelIds.Contains(sv.SpelId ?? 0))
                .ToList();
        }

        public IEnumerable<Spelverdeling> GetBySpelId(int spelId) {
            return dbContext.Spelverdelings.Where(v => v.SpelId == spelId);
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
		public bool HeeftSpelerGespeeld(int spelerId)
		{
			return dbContext.Spelverdelings.Any(sv => sv.SpelerId == spelerId);
		}
	}
}
