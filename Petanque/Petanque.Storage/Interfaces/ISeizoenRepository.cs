using Petanque.Storage.Entity;

namespace Petanque.Storage.Interfaces {
    public interface ISeizoenRepository {
        public IEnumerable<Seizoen> GetAll();
        public Seizoen Create(Seizoen request);
        
    }
}
