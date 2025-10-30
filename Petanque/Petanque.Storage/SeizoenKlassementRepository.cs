using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;

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
