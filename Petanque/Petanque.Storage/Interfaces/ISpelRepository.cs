using Petanque.Storage.Entity;

namespace Petanque.Storage.Interfaces {
    public interface ISpelRepository {
        Spel? GetById(int id);
        void UpdateScore(int spelId, int scoreA, int scoreB);
        void RemoveSpellen(List<Spel> spellen);
        Spel Create(Spel request);
        List<Spel> GetBySpeeldagAndTerrein(int speeldag, int terrein);
        IEnumerable<Spel> GetBySpeeldagId(int speeldagId);
    }
}
