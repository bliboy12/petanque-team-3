using Petanque.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Storage.Interfaces {
    public interface ISpelRepository {
       public Spel? GetById(int id);
        public void UpdateScore(int spelId, int scoreA, int scoreB);
    }
}
