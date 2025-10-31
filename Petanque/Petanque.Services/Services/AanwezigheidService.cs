using Petanque.Contracts.Requests;
using Petanque.Contracts.Responses;
using Petanque.Services.Interfaces;
using Petanque.Storage;
using Petanque.Storage.Entity;

namespace Petanque.Services.Services
{
    public class AanwezigheidService(AanwezigheidRepository aanwezigheidRepository) : IAanwezigheidService
    {
        public AanwezigheidResponseContract Create(AanwezigheidRequestContract request)
        {
            var entity = new Aanwezigheid()
            {
                SpeeldagId = request.SpeeldagId,
                SpelerId = request.SpelerId,
                SpelerVolgnr = request.SpelerVolgnr
            };

            aanwezigheidRepository.Create(entity);

            return MapToContract(entity);
        }
        public AanwezigheidResponseContract? GetById(int id)
        {
            var entity = aanwezigheidRepository.GetById(id);
            return entity is null ? null : MapToContract(entity);
        }
        public IEnumerable<AanwezigheidResponseContract> GetAll()
        {
            var entities = aanwezigheidRepository.GetAll();
            return entities.Select(a => MapToContract(a)).ToList();
        }
        private static AanwezigheidResponseContract MapToContract(Aanwezigheid entity)
        {
            return new AanwezigheidResponseContract()
            {
                AanwezigheidId = entity.AanwezigheidId,
                SpeeldagId = entity.SpeeldagId,
                SpelerId = entity.SpelerId,
                SpelerVolgnr = entity.SpelerVolgnr,
                SpeeldagDatum = entity.Speeldag?.Datum.ToString("yyyy-MM-dd") ?? ""
            };
        }

        public IEnumerable<AanwezigheidResponseContract> GetAanwezighedenOpSpeeldag(int id)
        {
            var entities = aanwezigheidRepository.GetAanwezighedenOpSpeeldag(id);
            return entities.Select(a => MapToContract(a)).ToList();
            // verbetering  - ToList() op het einde zetten
            //return context.Aanwezigheids.Select(a => MapToContract(a)).Where(s => s.SpeeldagId == id).ToList();

        }

        public void Delete(int id)
        {
            aanwezigheidRepository.DeleteAanwezigheid(id);
        }

        public IEnumerable<AanwezigheidResponseContract> GetAanwezighedenOpSpeler(int spelerId)
        {
            return aanwezigheidRepository.GetAanwezighedenOpSpeler(spelerId).Select(a => MapToContract(a))
                .ToList();
        }

    }
}
