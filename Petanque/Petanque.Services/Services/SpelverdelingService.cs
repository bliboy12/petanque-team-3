using Microsoft.Extensions.Logging;
using Petanque.Contracts.Responses;
using Petanque.Models.Enums;
using Petanque.Services.Mapping;
using Petanque.Storage;
using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;

namespace Petanque.Services.Services
{
    public class SpelverdelingService : ISpelverdelingService
    {
        private readonly Random _random = new();
        private readonly ISpelverdelingRepository _spelverdelingRepository;
        private readonly ISpelRepository _spelRepository;
        private readonly IAanwezigheidRepository _aanwezigheidRepository;
        private readonly ILogger _logger;

        public SpelverdelingService(ISpelverdelingRepository spelverdelingRepository, ISpelRepository spelRepository, IAanwezigheidRepository aanwezigheidRepository, ILogger<SpelverdelingService> logger)
        {
            _spelverdelingRepository = spelverdelingRepository;
            _spelRepository = spelRepository;
            _aanwezigheidRepository = aanwezigheidRepository;
            _logger = logger;
        }

        public IEnumerable<SpelverdelingResponseContract> GetById(int speeldagId)
        {
            var spellen = _spelRepository.GetBySpeeldagId(speeldagId);

            if (!spellen.Any())
                return Enumerable.Empty<SpelverdelingResponseContract>();

            var spelIds = spellen.Select(s => s.SpelId).ToList();

            var spelverdelingen = _spelverdelingRepository.GetBySpelIds(spelIds);

            var aanwezigheden = _aanwezigheidRepository.GetAanwezighedenOpSpeeldag(speeldagId);

            return spelverdelingen.Select(sv =>
            {
                var speler = aanwezigheden
                    .FirstOrDefault(a => a.SpelerVolgnr == sv.SpelerVolgnr)
                    ?.Speler;

                var spel = spellen.FirstOrDefault(sp => sp.SpelId == sv.SpelId);

                return MapToReturn(sv, speler, spel);
            }).ToList();
        }

        public struct smartDetails
        {
            public int Terrein;
            public List<int> TeamLeden, Tegenspelers;
        }
		// Voor elke speler die aanwezig is op de speeldag geven wij een dictionary terug per volgnr van de speler met zijn correspondent SkillLevel
		public Dictionary<int, SkillLevel> BepaalSkillLevels(int speeldag)
		{
			// de volgnr van elke speler en zijn SkillLevel
			var skillLevels = new Dictionary<int, SkillLevel>();
			var aanwezighedenMetSpeler = _aanwezigheidRepository.GetAanwezighedenOpSpeeldag(speeldag);

			foreach (var aanwezigheid in aanwezighedenMetSpeler)
			{
				int spelerVolgnr = aanwezigheid.SpelerVolgnr;

				var speler = aanwezigheid.Speler;
				// checked als de speler al andere matchen ervoor heeft gespeelt, zo ja wordt hij geclassifeerd als een expert anders als een noob
				// Als de spelerId al niet bestaat wilt het zeggen dat het een nieuwe speler is dus geven wij 0 als id maar die id bestaat sowieso niet dus
				// wordt false terug gegeven.
				bool heeftGespeeld = _spelverdelingRepository.HeeftSpelerGespeeld(aanwezigheid.SpelerId ?? 0);

				if (speler.SkillLevel == (int)SkillLevel.Noob && !heeftGespeeld)
					skillLevels[spelerVolgnr] = SkillLevel.Noob;
				else
					skillLevels[spelerVolgnr] = (SkillLevel)speler.SkillLevel;
			}
			return skillLevels;
		}
		public IEnumerable<SpelverdelingResponseContract> MaakVerdeling(IEnumerable<AanwezigheidResponseContract> aanwezigheden, int speeldagId)
        {
            _logger.LogCritical("Starting MaakVerdeling");

            const int maxAantalTerreinen = 10;
            const int aantalSpelrondes = 3;

            const int minAantalSpelersPerTeam = 2;
            const int maxAantalSpelersPerTeam = 3;

            int aantalGebruikteTerreinen; // aantal GEBRUIKTE terreinen
            List<int> masterSpelerList; // lijst van Volgnrs van aanwezige spelers
            Dictionary<string, int> aantalSpelersPerTerreinPerTeam; // key="terrein,team", value=aantalSpelers
            Dictionary<string, int> spelverdelingsInfo; // key="spelronde,terrein,team,nrInTeam", value=spelerVolgnr
            Dictionary<string, smartDetails> smartDetailsDictionary; // key="spelronde,spelerVolgnr", value="Terrein,TeamLeden,Tegenspelers"

            // STAP 1: Vul 'masterSpelerList', check aantal aanwezigen en terreinen, vul 'aantalSpelersPerTerreinPerTeam'
            {
                if (aanwezigheden == null)
                    throw new InvalidOperationException($"BUG: Aanwezigheden zijn null. Dit mag niet gebeuren.");

                masterSpelerList = aanwezigheden.Select(a => a.SpelerVolgnr).ToList();
                if (masterSpelerList.Distinct().Count() != masterSpelerList.Count())
                    throw new InvalidOperationException($"BUG: Er zitten dubbele 'SpelerVolgnr's in de lijst 'aanwezigheden'.");

                int aantalAanwezigen = masterSpelerList.Count();
                if (aantalAanwezigen == 0)
                    throw new InvalidOperationException($"Er zijn nog geen aanwezigen aangeduid op deze speeldag");
                if ((int)Math.Ceiling((double)aantalAanwezigen / maxAantalSpelersPerTeam / 2) > maxAantalTerreinen)
                    throw new InvalidOperationException($"Er zijn {maxAantalTerreinen} terreinen beschikbaar. Er is dus slechts plaats voor {maxAantalSpelersPerTeam * 2 * maxAantalTerreinen} van de {aantalAanwezigen} aanwezigen. (Verhoog evt. 'maxAantalSpelersPerTeam')");

                aantalGebruikteTerreinen = (int)Math.Floor((double)aantalAanwezigen / minAantalSpelersPerTeam / 2);
                if (aantalGebruikteTerreinen < 1)
                    throw new InvalidOperationException($"Er zijn slechts {aantalAanwezigen} aanwezigen. Dit is onvoldoende als je minstens {minAantalSpelersPerTeam} spelers per team wilt. (Verlaag evt. 'minAantalSpelersPerTeam')");
                if ((int)Math.Ceiling((double)aantalAanwezigen / aantalGebruikteTerreinen / 2) > maxAantalSpelersPerTeam)
                    throw new InvalidOperationException($"Met {aantalAanwezigen} aanwezigen kan er geen spelverdeling gemaakt worden met minstens {minAantalSpelersPerTeam} en maximaal {maxAantalSpelersPerTeam} spelers per team. (Verlaag evt. 'minAantalSpelersPerTeam' of verhoog 'maxAantalSpelersPerTeam')");

                aantalSpelersPerTerreinPerTeam = new Dictionary<string, int>();
                int totaalAantalSpelers = 0;
                int terrein;
                for (terrein = 1; terrein <= aantalGebruikteTerreinen; terrein++)
                {
                    aantalSpelersPerTerreinPerTeam[$"{terrein},A"] = minAantalSpelersPerTeam;
                    aantalSpelersPerTerreinPerTeam[$"{terrein},B"] = minAantalSpelersPerTeam;
                    totaalAantalSpelers += 2 * minAantalSpelersPerTeam;
                }
                if (totaalAantalSpelers > aantalAanwezigen)
                    throw new InvalidOperationException($"BUG: totaalAantalSpelers={totaalAantalSpelers} > aantalAanwezigen={aantalAanwezigen}");

                terrein = 1; char team = 'A';
                while (totaalAantalSpelers < aantalAanwezigen)
                {
                    aantalSpelersPerTerreinPerTeam[$"{terrein},{team}"]++;
                    totaalAantalSpelers++;
                    team++;
                    if (team == 'C')
                    {
                        team = 'A';
                        terrein++;
                        if (terrein > aantalGebruikteTerreinen) terrein = 1;
                    }
                }
            }
			//Bepaal skill levels voor alle spelers
			Dictionary<int, SkillLevel> spelerSkillLevels = BepaalSkillLevels(speeldagId);

            // Dit is om te testen als alle data werkt
			int aantalExperts = spelerSkillLevels.Values.Count(s => s == SkillLevel.Expert);
			int aantalNoobs = spelerSkillLevels.Values.Count(s => s == SkillLevel.Noob);
			_logger.LogInformation($"Skill levels: {aantalExperts} Experts, {aantalNoobs} Noobs");

			// STAP 2: Spelverdeling maken (lokaal in Dictionary)
			{
                smartDetailsDictionary = new Dictionary<string, smartDetails>();
                spelverdelingsInfo = new Dictionary<string, int>();
                for (int spelronde = 1; spelronde <= aantalSpelrondes; spelronde++)
                {
                    var beschikbareSpelers = new List<int>(masterSpelerList);
                    var selectieVoorkeurScores = beschikbareSpelers.ToDictionary(n => n, n => 100);
                    for (int terrein = 1; terrein <= aantalGebruikteTerreinen; terrein++)
                    {
                        var teamListDict = new Dictionary<char, List<int>>();
                        teamListDict['A'] = new List<int>();
                        teamListDict['B'] = new List<int>();
                        foreach (char team in new List<char> { 'A', 'B' })
                        {
                            char otherTeam = (team == 'A') ? 'B' : 'A';
                            if (spelronde > 1) { selectieVoorkeurScores = beschikbareSpelers.ToDictionary(n => n, n => 100); }
                            for (int nrInTeam = 1; nrInTeam <= aantalSpelersPerTerreinPerTeam[$"{terrein},{team}"]; nrInTeam++)
                            {

								int huidigTeamSize = aantalSpelersPerTerreinPerTeam[$"{terrein},{team}"];

								// check of alle reeds geselecteerde teamleden noobs zijn
								bool alleTeamLedenZijnNoobs = teamListDict[team].Count > 0 && teamListDict[team].All(s => spelerSkillLevels[s] == SkillLevel.Noob);
								bool isLaatsteSpelerInTeam = (nrInTeam == aantalSpelersPerTerreinPerTeam[$"{terrein},{team}"]);

								foreach (int speler in beschikbareSpelers)
								{
                                    // Als alle teamleden noobs zijn en als we bij de laatste persoon zijn die gaat toegevoegd worden aan team, geven wij hem hogere bonus zodat het een expert is
									if (alleTeamLedenZijnNoobs && isLaatsteSpelerInTeam && spelerSkillLevels[speler] == SkillLevel.Expert)
									{
										selectieVoorkeurScores[speler] += 50; // Grote bonus om all-Noob team te voorkomen maar ik weet nu niet als dit een te grote nummer zou zijn
									}

									// Skill level voorkeur
									if (spelerSkillLevels[speler] == SkillLevel.Expert)
									{
										// Expert heeft voorkeur voor 2-speler team
										if (huidigTeamSize == 2)
											selectieVoorkeurScores[speler] += 2;
										else if (huidigTeamSize == 3)
											selectieVoorkeurScores[speler] -= 1;
									}
									else
									{
										// Noob heeft voorkeur voor 3-speler team
										if (huidigTeamSize == 3)
											selectieVoorkeurScores[speler] += 2;
										else if (huidigTeamSize == 2)
											selectieVoorkeurScores[speler] -= 1;
									}
								}
								// pas 'selectieVoorkeurScores' aan
								for (int spelronde2 = 1; spelronde2 < spelronde; spelronde2++)
                                {
                                    foreach (int speler in beschikbareSpelers)
                                    {
                                        if (nrInTeam == 1)
                                        {
                                            if (smartDetailsDictionary[$"{spelronde2},{speler}"].Terrein == terrein)
                                                selectieVoorkeurScores[speler] -= spelronde2; // speelde al eens op dit terrein

                                            if (aantalSpelersPerTerreinPerTeam[$"{terrein},{team}"] > minAantalSpelersPerTeam) // zal nu in te groot team zitten
                                            {
                                                if (smartDetailsDictionary[$"{spelronde2},{speler}"].TeamLeden.Count > minAantalSpelersPerTeam)
                                                    selectieVoorkeurScores[speler] -= spelronde2 + 10; // speelde al eens in een te groot team
                                                if (smartDetailsDictionary[$"{spelronde2},{speler}"].Tegenspelers.Count > minAantalSpelersPerTeam)
                                                    selectieVoorkeurScores[speler] -= spelronde2 + 8; // speelde al eens TEGEN een te groot team
                                            }
                                            if (aantalSpelersPerTerreinPerTeam[$"{terrein},{otherTeam}"] > minAantalSpelersPerTeam) // zal TEGEN te groot team spelen
                                            {
                                                if (smartDetailsDictionary[$"{spelronde2},{speler}"].TeamLeden.Count > minAantalSpelersPerTeam)
                                                    selectieVoorkeurScores[speler] -= spelronde2 + 8; // speelde zelf al eens in een te groot team
                                                if (smartDetailsDictionary[$"{spelronde2},{speler}"].Tegenspelers.Count > minAantalSpelersPerTeam)
                                                    selectieVoorkeurScores[speler] -= spelronde2 + 6; // speelde al eens TEGEN een te groot team

                                            }
                                            if (team == 'B')
                                            {
                                                foreach (int speler2 in teamListDict['A'])
                                                {
                                                    if (smartDetailsDictionary[$"{spelronde2},{speler}"].TeamLeden.Contains(speler2))
                                                        selectieVoorkeurScores[speler] -= spelronde2 + 14; // was Teamlid, zou nu Tegenspeler zijn
                                                    if (smartDetailsDictionary[$"{spelronde2},{speler}"].Tegenspelers.Contains(speler2))
                                                        selectieVoorkeurScores[speler] -= spelronde2 + 17; // was Tegenspeler, zou nu opnieuw Tegenspeler zijn
                                                }
                                            }
                                        }
                                        else // nrInTeam > 1
                                        {
                                            int vorigeSpeler = spelverdelingsInfo[$"{spelronde},{terrein},{team},{nrInTeam - 1}"];
                                            if (smartDetailsDictionary[$"{spelronde2},{speler}"].TeamLeden.Contains(vorigeSpeler))
                                                selectieVoorkeurScores[speler] -= spelronde2 + 20; // was TeamLid, zou nu opnieuw TeamLid zijn
                                            if (smartDetailsDictionary[$"{spelronde2},{speler}"].Tegenspelers.Contains(vorigeSpeler))
                                                selectieVoorkeurScores[speler] -= spelronde2 + 14; // was Tegenspeler, zou nu TeamLid zijn
                                        }
                                    }
                                    /*foreach (int speler in beschikbareSpelers)
                                    {
                                        _logger.LogInformation($"speler={speler}, score={selectieVoorkeurScores[speler]}");
                                    }*/
                                }
                                int maxVal = selectieVoorkeurScores.Values.Max();
                                int count = selectieVoorkeurScores.Where(kvp => kvp.Value == maxVal).Count();
                                int s = selectieVoorkeurScores.Where(kvp => kvp.Value == maxVal).ToDictionary().Keys.ElementAt(_random.Next(count));
                                _logger.LogCritical($"spelronde={spelronde}, terrein={terrein}, team={team}, nrInTeam={nrInTeam}, maxVal={maxVal}, count={count}: speler={s}");
                                beschikbareSpelers.Remove(s);
                                selectieVoorkeurScores.Remove(s);
                                spelverdelingsInfo[$"{spelronde},{terrein},{team},{nrInTeam}"] = s;
                                teamListDict[team].Add(s);
                            }
                        }
                        // vul smartDetailsDictionary in, behalve de laatste keer
                        if (spelronde < aantalSpelrondes)
                        {
                            //_logger.LogInformation($"--- vul smartDetailsDictionary in");
                            foreach (char team in new List<char> { 'A', 'B' })
                            {
                                char otherTeam = (team == 'A') ? 'B' : 'A';
                                for (int nrInTeam = 1; nrInTeam <= aantalSpelersPerTerreinPerTeam[$"{terrein},{team}"]; nrInTeam++)
                                {
                                    int s = spelverdelingsInfo[$"{spelronde},{terrein},{team},{nrInTeam}"];
                                    smartDetailsDictionary[$"{spelronde},{s}"] = new smartDetails
                                    {
                                        Terrein = terrein,
                                        TeamLeden = new List<int>(teamListDict[team]),
                                        Tegenspelers = new List<int>(teamListDict[otherTeam])
                                    };
                                }
                            }
                        }
                    }
                }
            }
            // STAP 3: DELETE old Spel + Spelverdeling from DB
            {
                var oudeSpelIds = _spelRepository.GetBySpeeldagId(speeldagId).Select(sp => sp.SpelId).ToList();
                var oudeSpelverdelingen = _spelverdelingRepository.GetBySpelIds(oudeSpelIds);
                _spelverdelingRepository.RemoveSpelverdelingen(oudeSpelverdelingen.ToList());

                var oudeSpellen = _spelRepository.GetBySpeeldagId(speeldagId);
                _spelRepository.RemoveSpellen(oudeSpellen.ToList());
            }
            // STAP 4: INSERT content of (Dictionary) spelverdelingsInfo to DB
            {
                var responses = new List<SpelverdelingResponseContract>();
                for (int spelronde = 1; spelronde <= aantalSpelrondes; spelronde++)
                {
                    for (int terrein = 1; terrein <= aantalGebruikteTerreinen; terrein++)
                    {
                        var spel = new Spel
                        {
                            SpeeldagId = speeldagId,
                            Terrein = $"Terrein {terrein}",
                            ScoreA = 0,
                            ScoreB = 0,
                            SpelerVolgnr = spelverdelingsInfo[$"{spelronde},{terrein},A,1"]
                        };

                        _spelRepository.Create(spel);

                        foreach (char team in new List<char> { 'A', 'B' })
                        {
                            for (int nrInTeam = 1; nrInTeam <= aantalSpelersPerTerreinPerTeam[$"{terrein},{team}"]; nrInTeam++)
                            {
                                _spelverdelingRepository.Create(new Spelverdeling
                                {
                                    SpelId = spel.SpelId,
                                    Team = $"Team {team}",
                                    SpelerPositie = $"P{nrInTeam}",
                                    SpelerVolgnr = spelverdelingsInfo[$"{spelronde},{terrein},{team},{nrInTeam}"],
                                    SpelerId = spelverdelingsInfo[$"{spelronde},{terrein},{team},{nrInTeam}"]
                                });
                            }
                        }
                        var spelverdelingenToAdd = _spelverdelingRepository.GetBySpelId(spel.SpelId).Select(s => s.AsModel().AsContract()).ToList();
                        responses.AddRange(spelverdelingenToAdd);
                    }
                }
                return responses;
            }
        }

        private static SpelverdelingResponseContract MapToReturn(Spelverdeling entity, Speler? speler, Spel? spel)
        {
            if (speler == null) throw new ArgumentNullException(nameof(speler), "Speler mag niet null zijn.");
            if (spel == null) throw new ArgumentNullException(nameof(spel), "Spel mag niet null zijn.");

            return new SpelverdelingResponseContract
            {
                SpelverdelingsId = entity.SpelverdelingsId,
                SpelId = entity.SpelId,
                Team = entity.Team,
                SpelerPositie = entity.SpelerPositie,
                SpelerVolgnr = entity.SpelerVolgnr,
                Speler = new PlayerResponseContract
                {
                    SpelerId = speler.SpelerId,
                    Voornaam = speler.Voornaam,
                    Naam = speler.Naam
                },
                Spel = new SpelResponseContract
                {
                    SpelId = spel.SpelId,
                    SpeeldagId = spel.SpeeldagId,
                    Terrein = spel.Terrein
                }
            };
        }
        public IEnumerable<SpelverdelingResponseContract> GetBySpeeldagAndTerrein(int speeldag, int terrein)
        {
            var spellen = _spelRepository.GetBySpeeldagAndTerrein(speeldag, terrein);

            if (!spellen.Any())
                return Enumerable.Empty<SpelverdelingResponseContract>();

            var spelIds = spellen.Select(s => s.SpelId).ToList();

            var spelverdelingen = _spelverdelingRepository.GetBySpelIds(spelIds);


            var aanwezigheden = _aanwezigheidRepository.GetAanwezighedenOpSpeeldag(speeldag);

            return spelverdelingen.Select(sv =>
            {
                var speler = aanwezigheden
                    .FirstOrDefault(a => a.SpelerVolgnr == sv.SpelerVolgnr)
                    ?.Speler;

                var spel = spellen.FirstOrDefault(sp => sp.SpelId == sv.SpelId);

                return MapToReturn(sv, speler, spel);
            }).ToList();
        }
    }
}
