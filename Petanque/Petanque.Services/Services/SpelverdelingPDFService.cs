using Petanque.Contracts.Responses;
using Petanque.Services.Interfaces;
using Petanque.Storage;
using Petanque.Storage.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Petanque.Services.Services
{
    public class SpelverdelingPDFService(ISpeeldagRepository speeldagRepository) : ISpelverdelingPDFService
    {
        public Stream GenerateSpelverdelingPDF(IEnumerable<SpelverdelingResponseContract> spelverdelingen)
        {
			/// Create an in-memory stream that will contain the final PDF output.
			var stream = new MemoryStream();

			/// Group the flat list of spelverdelingen per SpelId, and convert each group
			/// into a SpelResponseContract containing all relevant spel data
			var spellen = spelverdelingen
                .GroupBy(sv => sv.SpelId)
                .Select(g =>
                {
					/// Take the first element to access spel-level data (same for whole group)
					var eerste = g.First();
                    return new SpelResponseContract
                    {
                        SpelId = (int)eerste.SpelId,
                        SpeeldagId = eerste.Spel.SpeeldagId,
                        Terrein = eerste.Spel.Terrein,
                        ScoreA = eerste.Spel.ScoreA,
                        ScoreB = eerste.Spel.ScoreB,
                        Spelverdelingen = g.ToList()
                    };
                })
                .ToList();
			/// Extract speeldag ID; throw if null because without it we cannot retrieve the date
			int speeldagIdd = spelverdelingen.First().Spel.SpeeldagId ?? throw new InvalidOperationException("SpeeldagId cannot be null.");
			/// Retrieve speeldag info such as date
			var speeldag = speeldagRepository.GetById(speeldagIdd);
			/// Format date in Dutch: e.g. "maandag 3 juni 2024"
			string datumFormatted = speeldag.Datum.ToString("dddd d MMMM yyyy", new System.Globalization.CultureInfo("nl-NL"));

			/// Begin PDF document creation
			Document.Create(container =>
            {
                container.Page(page =>
                {
					/// Configure page layout
					page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Content().Column(col =>
                    {
						/// Group all spellen by terrain and sort
						var spellenPerTerrein = spellen
                            .GroupBy(spel => spel.Terrein)
                            .OrderBy(g => g.Key);

                        int terreinNummer = 1;

						/// Iterate over each terrain group
						foreach (var terreinGroup in spellenPerTerrein)
                        {
							/// Terrain header with date on the right
							col.Item().PaddingBottom(15).Row(r =>
                            {
                                r.RelativeItem().Text($"TERREIN: {terreinGroup.Key}")
                                    .FontSize(18)
                                    .Bold()
                                    .Underline();
                                r.RelativeItem().AlignRight().Text(datumFormatted);
                            });

                            int spelnummer = 1;

							/// Render every spel under this terrain
							foreach (var spel in terreinGroup)
                            {
								/// Create lists of players per team
								var teamA = spel.Spelverdelingen.Where(x => x.Team == "Team A").ToList();
                                var teamB = spel.Spelverdelingen.Where(x => x.Team == "Team B").ToList();

								/// Box around the spel
								col.Item().PaddingBottom(20).Border(1).Padding(15).Column(spelCol =>
                                {
									/// Spel title bar (Spel 1, Spel 2, …)
									spelCol.Item().Row(row =>
                                    {
                                        row.RelativeItem().AlignCenter()
                                            .Background(Colors.BlueGrey.Darken2)
                                            .Padding(8)
                                            .Text($"Spel {spelnummer++}")
                                            .FontSize(16)
                                            .Bold()
                                            .FontColor(Colors.White);
                                    });
									/// Layout for Team A and Team B side-by-side
									spelCol.Item().PaddingTop(10);

                                    spelCol.Item().Row(row =>
                                    {
                                        /// Team A
                                        row.RelativeItem().Column(teamCol =>
                                        {
                                            teamCol.Item().Text("Team A").FontSize(14).Bold().Underline();
                                            teamCol.Item().PaddingBottom(5);

											/// Print all Team A players
											foreach (var speler in teamA)
                                            {
                                                var skill = speler.Speler.SkillLevel == 0 ? "Noob" : "Expert";
                                                var naam = speler.Speler != null
                                                    ? $"{speler.SpelerVolgnr}. {speler.Speler.Naam} {speler.Speler.Voornaam} ({skill})"
                                                    : $"Onbekende speler (volgnr {speler.SpelerVolgnr})";
                                                teamCol.Item().Text(naam);
                                            }

											/// Score boxes for Team A (13 small squares)
											teamCol.Item().Row(scoreRow =>
                                            {
                                                for (int i = 0; i < 13; i++)
                                                {
                                                    scoreRow.ConstantItem(18)
                                                        .Height(18)
                                                        .Border(1)
                                                        .PaddingRight(2);
                                                }
                                            });
											/// Score numbering row below boxes
											teamCol.Item().PaddingTop(1).Text("  1   2    3    4   5    6   7    8    9  10  11 12 13");
											/// Label for total points
											teamCol.Item().PaddingTop(5).Text("Punten Team A:");

                                        });

										/// Vertical divider between teams
										row.ConstantItem(2).Height(120).Background(Colors.Grey.Lighten2);

										/// === TEAM B SECTION ===
										row.RelativeItem().Column(teamCol =>
                                        {
                                            teamCol.Item().Text("Team B").FontSize(14).Bold().Underline();
                                            teamCol.Item().PaddingBottom(5);

											/// Print all Team B players
											foreach (var speler in teamB)
                                            {
                                                var skill = speler.Speler.SkillLevel == 0 ? "Noob" : "Expert";
                                                
                                                var naam = speler.Speler != null
                                                    ? $"{speler.SpelerVolgnr}. {speler.Speler.Naam} {speler.Speler.Voornaam} ({skill})"
                                                    : $"Onbekende speler (volgnr {speler.SpelerVolgnr})";
                                                teamCol.Item().Text(naam);
                                            }

											/// Score boxes for Team B
											teamCol.Item().Row(scoreRow =>
                                            {
                                                for (int i = 0; i < 13; i++)
                                                {
                                                    scoreRow.ConstantItem(18)
                                                        .Height(18)
                                                        .Border(1)
                                                        .PaddingRight(2);
                                                }
                                            });
											/// Score numbering row
											teamCol.Item().PaddingTop(1).Text("  1   2    3    4   5    6   7    8    9  10  11 12 13");
											/// Label for total points
											teamCol.Item().PaddingTop(5).Text("Punten Team B:");
                                        });
                                    });
                                });
                            }
							/// Add page break between terrains unless this is the last one
							if (terreinGroup.Key != spellenPerTerrein.Last().Key)
                            {
                                col.Item().PageBreak();
                            }

                            terreinNummer++;
                        }
                    });
                });
            })
			/// Write the finished PDF into the MemoryStream
			.GeneratePdf(stream);

			/// Reset position so the caller can read from the beginning
			stream.Position = 0;
            return stream;
        }
    }
}
