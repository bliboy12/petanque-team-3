using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;

namespace Petanque.Storage {
    public class SpelRepository(Id312896PetanqueContext dbContext) : ISpelRepository {
        public Spel? GetById(int id) {
            var entity = dbContext.Spels.Find(id);
            return entity != null ? entity : null;
        }

        public void UpdateScore(int spelId, int scoreA, int scoreB) {
            var spel = GetById(spelId);
            if (spel == null) throw new Exception("Spel niet gevonden");

            spel.ScoreA = scoreA;
            spel.ScoreB = scoreB;

            dbContext.SaveChanges();
        }

        public void RemoveSpellen(List<Spel> spellen) {
            dbContext.Spels.RemoveRange(spellen);
            dbContext.SaveChanges();
        }

        public Spel Create(Spel spel) {
            dbContext.Spels.Add(spel);
            dbContext.SaveChanges();

            return spel;
        }

        public List<Spel> GetBySpeeldagAndTerrein(int speeldag, int terrein) {
            var spellen = dbContext.Spels
                .Where(sp => sp.SpeeldagId == speeldag && sp.Terrein == $"Terrein {terrein}")
                .ToList();

            return spellen;
        }

        public IEnumerable<Spel> GetBySpeeldagId(int speeldagId) {
            return dbContext.Spels
                .Where(sp => sp.SpeeldagId == speeldagId)
                .ToList();
        }
    }
}
