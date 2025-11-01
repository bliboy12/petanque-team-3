using Petanque.Contracts.Responses;
using Petanque.Services.Interfaces;
using Petanque.Services.Mapping;
using Petanque.Storage;

namespace Petanque.Services.Services
{
    public class ScoreService(SpelRepository spelRepository) : IScoreService
    {
        public SpelResponseContract? GetById(int id)
        {
            var entity = spelRepository.GetById(id);
            
            return entity is null ? null : entity.AsModel().AsContract();
        }

        public void UpdateScore(int spelId, int scoreA, int scoreB) {
            spelRepository.UpdateScore(spelId, scoreA, scoreB);
        }
    }
}
