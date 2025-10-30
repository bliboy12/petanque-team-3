using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
