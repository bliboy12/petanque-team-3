using Petanque.Contracts.Requests;
using Petanque.Contracts.Responses;
using Petanque.Services.Interfaces;
using Petanque.Services.Mapping;
using Petanque.Storage;
using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;

namespace Petanque.Services.Services
{
    public class SpeeldagService(ISpeeldagRepository speeldagRepository) : ISpeeldagService
    {
        public SpeeldagResponseContract Create(SpeeldagRequestContract request)
        {
            var requestedDate = request.Datum.Date;

            var speeldagCheck = speeldagRepository.GetSpeeldagByRequestedDate(requestedDate);

            if (speeldagCheck != null)
                return speeldagCheck.AsModel().AsContract();

            var speeldag = new Speeldag
            {
                Datum = requestedDate,  // schrijf enkel de Date component weg
                SeizoensId = request.SeizoensId
            };

            speeldagRepository.Create(speeldag);
         

            return speeldag.AsModel().AsContract();
        }
        
        public SpeeldagResponseContract GetById(int id)
        {
            var entity = speeldagRepository.GetBySpeeldag(id);

            if (entity == null)
                return null;
            
            return entity.AsModel().AsContract();
        }

        public IEnumerable<SpeeldagResponseContract> GetAll()
        {
            var speeldagen = speeldagRepository.GetAll();
            
            return speeldagen.Select(m => m.AsModel().AsContract());
        }
    }
}
