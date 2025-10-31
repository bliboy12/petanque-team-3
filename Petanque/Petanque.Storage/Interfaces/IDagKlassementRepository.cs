using Petanque.Storage.Entity;

namespace Petanque.Storage.Interfaces {
    public interface IDagKlassementRepository {
        public Dagklassement Create(Dagklassement request);
        public IEnumerable<Dagklassement>? GetById(int id);
        public IEnumerable<Dagklassement> CreateDagKlassementen(List<Dagklassement> dagklassementen, int speeldagId);
    }
}
