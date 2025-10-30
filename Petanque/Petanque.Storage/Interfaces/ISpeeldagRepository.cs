using Petanque.Storage.Entity;

namespace Petanque.Storage.Interfaces {
    public interface ISpeeldagRepository {
        public Speeldag Create(Speeldag request);
        public Speeldag? GetById(int id);
        public IEnumerable<Speeldag> GetAll();
        Speeldag? GetSpeeldagByRequestedDate(DateTime requestedDate);
        Speeldag? GetBySpeeldag(int id);
    }
}
