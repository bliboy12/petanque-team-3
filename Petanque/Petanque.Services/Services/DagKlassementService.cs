using Microsoft.EntityFrameworkCore;
using Petanque.Contracts.Requests;
using Petanque.Contracts.Responses;
using Petanque.Services.Interfaces;
using Petanque.Services.Mapping;
using Petanque.Storage;
using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;

namespace Petanque.Services.Services
{
    public class DagKlassementService(IDagKlassementRepository? dagKlassementRepository, ISpeeldagRepository speeldagRepository, ISpeeldagService speeldagService, ISpelverdelingService spelverdelingService,  Id312896PetanqueContext context) : IDagKlassementService
    {
        public DagKlassementResponseContract Create(DagKlassementRequestContract request)
        {
            var entity = new Dagklassement()
            {
                SpeeldagId = request.SpeeldagId,
                Hoofdpunten = request.Hoofdpunten,
                PlusMinPunten = request.PlusMinPunten,
                SpelerId = request.SpelerId
            };

            dagKlassementRepository.Create(entity);

            return entity.AsModel().AsContract();
        }

        public IEnumerable<DagKlassementResponseContract>? GetById(int id)
        {
            var dagklassementen = dagKlassementRepository.GetById(id);

            return dagklassementen.Select(a => a.AsModel().AsContract()).Where(contract => contract != null) .ToList()!;
        }
        
        /** Service to generate all the daily rankings for a specific match day */
        public IEnumerable<DagKlassementResponseContract> CreateDailyRankings(int matchDayId)
        {
            // SpeeldagResponseContract speeldagData
            // var speeldagId = speeldagData.SpeeldagId;
            
            var speeldagData = speeldagRepository.GetById(matchDayId);
            
            var gebruikteVolgnrs = speeldagData.Spels
                .SelectMany(s => s.Spelverdelings ?? [])
                .Select(sv => sv.SpelerVolgnr)
                .Distinct()
                .ToList();
            
            var spelersInSpeeldag = context.Aanwezigheids
                .Where(x => x.SpeeldagId == matchDayId && gebruikteVolgnrs.Contains(x.SpelerVolgnr))
                .AsEnumerable()
                .GroupBy(x => x.SpelerVolgnr)
                .ToDictionary(g => g.Key, g => g.First().SpelerId);

			/// Tracking dictionaries:
			/// 1) plus/min score per spelerVolgnr
			/// 2) win count per spelerVolgnr
			var scorePerSpeler = new Dictionary<int, int>();
            var winsPerSpeler = new Dictionary<int, int>();
            
            foreach (var spel in speeldagData.Spels)
            {
                if (spel?.Spelverdelings == null || spel.Spelverdelings.Count == 0)
                    continue;

                var teamA = spel.Spelverdelings
                    .Where(v => v.Team == "Team A")
                    .Select(v => v.SpelerVolgnr)
                    .ToList();

                var teamB = spel.Spelverdelings
                    .Where(v => v.Team == "Team B")
                    .Select(v => v.SpelerVolgnr)
                    .ToList();

                if (teamA.Count == 0 || teamB.Count == 0)
                    continue;

                var scoreA = spel.ScoreA;
                var scoreB = spel.ScoreB;

				/// Score difference used for plus/min calculation
				var scoreVerschil = scoreA - scoreB;

				/// --- APPLY SCORING LOGIC ---

				/// For every speler in team A
				foreach (var speler in teamA)
                {
                    if (!scorePerSpeler.ContainsKey(speler)) 
                        scorePerSpeler[speler] = 0;

					/// Add the score difference (positive if A wins, negative if A loses)
					scorePerSpeler[speler] += scoreVerschil;

                    if (scoreA > scoreB)  ///Team A wins
                    {
                        if (!winsPerSpeler.ContainsKey(speler)) 
                            winsPerSpeler[speler] = 0;

                        winsPerSpeler[speler]++;
                    }
                }
				/// For every speler in team B
				foreach (var speler in teamB)
                {
                    if (!scorePerSpeler.ContainsKey(speler)) 
                        scorePerSpeler[speler] = 0;

					/// Team B’s plus/min is the inverted score difference
					scorePerSpeler[speler] -= scoreVerschil;

                    if (scoreB > scoreA)  ///Team B wins
                    {
                        if (!winsPerSpeler.ContainsKey(speler)) 
                            winsPerSpeler[speler] = 0;

                        winsPerSpeler[speler]++;
                    }
                }
            }

            var dagKlassementen = new List<DagKlassementResponseContract>();

            foreach (var (spelerVolgNr, spelerId) in spelersInSpeeldag)
            {
				/// Fetch plus/min score if it exists
				var plusMin = scorePerSpeler.TryGetValue(spelerVolgNr, out var punten) ? punten : 0;
				/// Fetch number of wins
				var gewonnenSpellen = winsPerSpeler.TryGetValue(spelerVolgNr, out var wins) ? wins : 0;

                dagKlassementen.Add(new DagKlassementResponseContract
                {
                    SpeeldagId = matchDayId,
                    SpelerId = spelerId,
                    Hoofdpunten = 1 + gewonnenSpellen, /// Base 1 + wins
					PlusMinPunten = plusMin
                });
            }

			/// Convert to EF Core entities
			var entities = dagKlassementen.Select(k => new Dagklassement
            {
                SpeeldagId = k.SpeeldagId,
                SpelerId = k.SpelerId,
                Hoofdpunten = k.Hoofdpunten,
                PlusMinPunten = k.PlusMinPunten
            }).ToList();

			/// Start transaction to clear existing records first, then re-insert
			using var transaction = context.Database.BeginTransaction();
            try
            {
				/// Delete all old klassements for this speeldag
				context.Dagklassements
                    .Where(dk => dk.SpeeldagId == speeldagData.SpeeldagId)
                    .ExecuteDelete();

				/// Insert new klassements
				context.AddRange(entities);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            return dagKlassementen;
        }
    }
}
