using Petanque.Storage.Entity;

namespace Petanque.Storage.Interfaces {
    public interface ISpelverdelingRepository {
        public IEnumerable<Spelverdeling> GetBySpelId(List<int> spelIds);
        void RemoveSpelverdelingen(List<Spelverdeling> spellen);
        Spelverdeling Create(Spelverdeling spelverdeling);
    }
}
