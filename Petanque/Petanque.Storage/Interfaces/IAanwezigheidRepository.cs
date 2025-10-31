using Petanque.Storage.Entity;


namespace Petanque.Storage.Interfaces {
    public interface IAanwezigheidRepository {

        public Aanwezigheid Create(Aanwezigheid aanwezigheid);
        public Aanwezigheid? GetById(int id);
        public IEnumerable<Aanwezigheid> GetAll();
        public IEnumerable<Aanwezigheid> GetAanwezighedenOpSpeeldag(int id);
        public void DeleteAanwezigheid(int id);
        public IEnumerable<Aanwezigheid> GetAanwezighedenOpSpeler(int spelerId);
    }
}
