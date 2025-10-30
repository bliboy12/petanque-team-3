using Petanque.Storage.Entity;

namespace Petanque.Storage.Interfaces {
    public interface ISeizoenKlassementRepository {
        public IEnumerable<Seizoensklassement>? GetById(int seizoensId);

    }
}
