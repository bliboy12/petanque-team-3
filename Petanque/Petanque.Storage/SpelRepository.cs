using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
