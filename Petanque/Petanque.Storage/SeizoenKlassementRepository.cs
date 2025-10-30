using Microsoft.EntityFrameworkCore;
using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Storage {
    public class SeizoenKlassementRepository(Id312896PetanqueContext dbContext) : ISeizoenKlassementRepository {
        public IEnumerable<Seizoensklassement>? GetById(int seizoensId) {
            var klassementen = dbContext.Seizoensklassements
                .Where(sk => sk.SeizoensId == seizoensId)
                .OrderByDescending(sk => sk.Hoofdpunten).ThenByDescending(sk => sk.PlusMinPunten)
                .ToList();

            return klassementen != null ? klassementen : null;
        }
    }
}
