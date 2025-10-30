using Petanque.Contracts.Requests;
using Petanque.Contracts.Responses;
using Petanque.Services.Interfaces;
using Petanque.Storage;
using Petanque.Storage.Entity;

namespace Petanque.Services.Services
{
    public class SpeeldagService(SpeeldagRepository speeldagRepository, SpelverdelingRepository spelverdelingRepository) : ISpeeldagService
    {
        public SpeeldagResponseContract Create(SpeeldagRequestContract request)
        {
            var requestedDate = request.Datum.Date;

            var speeldagCheck = speeldagRepository.GetSpeeldagByRequestedDate(requestedDate);

            if (speeldagCheck != null)
                return MapToContract(speeldagCheck);

            var speeldag = new Speeldag
            {
                Datum = requestedDate,  // schrijf enkel de Date component weg
                SeizoensId = request.SeizoensId
            };

            speeldagRepository.Create(speeldag);
         

            return MapToContract(speeldag);
        }


        public SpeeldagResponseContract GetById(int id)
        {
            var entity = speeldagRepository.GetBySpeeldag(id);

            if (entity == null)
                return null;

            var spelIds = entity.Spels.Select(s => s.SpelId).ToList();

            // Belangrijk: Include Speler zodat spelerinformatie beschikbaar is
            var spelverdelingen = spelverdelingRepository.GetBySpelIds(spelIds);

            return MapToContract(entity, spelverdelingen.ToList());
        }

        public IEnumerable<SpeeldagResponseContract> GetAll()
        {
            var speeldagen = speeldagRepository.GetAll();

            var spelIds = speeldagen.SelectMany(s => s.Spels).Select(sp => sp.SpelId).Distinct().ToList();

            var spelverdelingen = spelverdelingRepository.GetBySpelIds(spelIds);


            return speeldagen
                .Select(a => MapToContract(a, spelverdelingen.ToList()))
                .ToList();
        }

        private SpeeldagResponseContract MapToContract(Speeldag entity)
        {
            return new SpeeldagResponseContract
            {
                SpeeldagId = entity.SpeeldagId,
                Datum = entity.Datum,
                Spel = entity.Spels
                    .Select(s => new SpelResponseContract
                    {
                        SpelId = s.SpelId,
                        SpeeldagId = s.SpeeldagId,
                        Terrein = s.Terrein,
                        ScoreA = s.ScoreA,
                        ScoreB = s.ScoreB,
                        Spelverdelingen = new List<SpelverdelingResponseContract>()
                    })
                    .ToList()
            };
        }

        private SpeeldagResponseContract MapToContract(Speeldag entity, List<Spelverdeling> spelverdelingen)
        {
            return new SpeeldagResponseContract
            {
                SpeeldagId = entity.SpeeldagId,
                Datum = entity.Datum,
                Spel = entity.Spels
                    .Select(s => new SpelResponseContract
                    {
                        SpelId = s.SpelId,
                        SpeeldagId = s.SpeeldagId,
                        Terrein = s.Terrein,
                        ScoreA = s.ScoreA,
                        ScoreB = s.ScoreB,
                        Spelverdelingen = spelverdelingen
                            .Where(sv => sv.SpelId == s.SpelId)
                            .Select(sv => new SpelverdelingResponseContract
                            {
                                SpelverdelingsId = sv.SpelverdelingsId,
                                SpelId = sv.SpelId,
                                Team = sv.Team,
                                SpelerVolgnr = sv.SpelerVolgnr,
                                Speler = sv.Speler == null ? null : new PlayerResponseContract
                                {
                                    SpelerId = sv.Speler.SpelerId,
                                    Voornaam = sv.Speler.Voornaam,
                                    Naam = sv.Speler.Naam
                                }
                            })
                            .ToList()
                    })
                    .ToList()
            };
        }
    }
}
