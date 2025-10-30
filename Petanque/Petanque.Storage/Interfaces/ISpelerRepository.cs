using Petanque.Storage.Entity;

namespace Petanque.Storage.Interfaces {
    public interface ISpelerRepository {
        public Speler Create(Speler request);

        public Speler? GetById(int id);
        public IEnumerable<Speler> GetAll();

        public void Update(int id, string voornaam, string naam);

        public void Delete(int id);
    }
}
