using Microsoft.EntityFrameworkCore;
using Petanque.Contracts.Requests;
using Petanque.Contracts.Responses;
using Petanque.Services.Interfaces;
using Petanque.Storage;
using Petanque.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
