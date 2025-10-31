using Petanque.Contracts.Responses;
using Petanque.Services.Interfaces;
using Petanque.Storage;
using Petanque.Storage.Entity;

namespace Petanque.Services.Services
{
    public class ScoreService(SpelRepository spelRepository) : IScoreService
    {

        public SpelResponseContract? GetById(int id)
        {
            var entity = spelRepository.GetById(id);
            return entity is null ? null : MapToContract(entity);
        }
        private static SpelResponseContract MapToContract(Spel entity)
        {
            return new SpelResponseContract()
            {
                SpelId = entity.SpelId,
                SpeeldagId = entity.SpeeldagId,
                Terrein = entity.Terrein
            };
        }

        public void UpdateScore(int spelId, int scoreA, int scoreB) {
            spelRepository.UpdateScore(spelId, scoreA, scoreB);
        }
    }
}
