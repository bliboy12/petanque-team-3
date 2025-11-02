using Petanque.Storage.Entity;

namespace Petanque.Storage.Interfaces {
    public interface ISpelverdelingRepository {
        public IEnumerable<Spelverdeling> GetBySpelIds(List<int> spelIds);
        void RemoveSpelverdelingen(List<Spelverdeling> spellen);
        Spelverdeling Create(Spelverdeling spelverdeling);
        IEnumerable<Spelverdeling> GetBySpelId(int spelId);
    }
}
