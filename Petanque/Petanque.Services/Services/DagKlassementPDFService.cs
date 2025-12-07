using Petanque.Services.Interfaces;
using Petanque.Storage;
using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Petanque.Services.Services
{
    public class DagKlassementPDFService(IDagKlassementRepository dagKlassementRepository, ISpelerRepository spelerRepository, ISpeeldagRepository speeldagRepository) : IDagKlassementPDFService
    {
        public async Task<Stream> GenerateDagKlassementPdfAsync(int id)
        {
			// Get the speeldag (match day) by ID
			var speeldag = speeldagRepository.GetById(id);
            if (speeldag == null)
                return null;

			// Get all speler IDs that appear in the dagklassement for this speeldag
			var spelerIdsInDagklassement = dagKlassementRepository.GetById(id).Select(d => d.SpelerId).ToList();

            // Load all spelers matching the IDs from the dagklassement
            var spelers = spelerRepository.GetBySpelerIds(spelerIdsInDagklassement);

            var dagklassements = dagKlassementRepository.GetById(id);

			// If none exist → no reason to generate a PDF
			if (dagklassements == null || !dagklassements.Any())
                return null;


			// Combine speler info with their scores, sort descending by hoofdpunten, then by score
			var spelersMetScores = spelers.Select(speler =>
            {
				// Find the klassement entry for this specific speler
				var dagKlassement = dagklassements.FirstOrDefault(dk => dk.SpelerId == speler.SpelerId);
                return new
                {
                    speler.Naam,
                    speler.Voornaam,
                    Score = dagKlassement?.PlusMinPunten ?? 0,
                    Hoofdpunten = dagKlassement?.Hoofdpunten ?? 0
                };
            }).OrderByDescending(s => s.Hoofdpunten).ThenByDescending(s => s.Score).ToList();

			// Memory stream that will contain the PDF output
			var memoryStream = new MemoryStream();

			// Create the PDF document using QuestPDF
			var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(595, 842);
                    page.Margin(20);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Content().Column(col =>
                    {
                        string datumFormatted = speeldag.Datum.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("nl-NL"));

						// PDF title
						col.Item().Element(e => e
                            .PaddingBottom(2)
                            .Text($"VL@S - Dagklassement - {datumFormatted}")
                            .FontSize(14)
                            .Bold()
                            .AlignCenter());

                        col.Item().Element(e => e.PaddingTop(10));

						// Build the score table
						col.Item().Table(table =>
                        {
							// Define table columns: rank, name, hoofdpunten, score
							table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(25);
                                columns.RelativeColumn(3);
                                columns.ConstantColumn(35);
                                columns.ConstantColumn(35);
                            });

                            int rang = 1; // Current ranking number
							int prevHoofdpunten = 0; // To compare equal scores
							int prevScore = 0;

                            foreach (var speler in spelersMetScores)
                            {
                                bool isEvenRow = rang % 2 == 0;
                                string background = isEvenRow ? Colors.Grey.Lighten4 : Colors.White;

								// If hoofdpunten + score are identical → tied rank → show blank instead of rank number
								if ((speler.Hoofdpunten == prevHoofdpunten) && (speler.Score == prevScore))
                                    table.Cell().Element(e => e.Background(background).PaddingVertical(2)).Text(' ');
                                else
                                    table.Cell().Element(e => e.Background(background).PaddingVertical(2)).Text(rang.ToString());

								// Player full name
								table.Cell().Element(e => e.Background(background).PaddingVertical(2)).Text($"{speler.Naam} {speler.Voornaam}");
								// Hoofdpunten column
								table.Cell().Element(e => e.Background(background).PaddingVertical(2)).AlignCenter().Text(speler.Hoofdpunten.ToString());
								// Score column
								table.Cell().Element(e => e.Background(background).PaddingVertical(2)).AlignCenter().Text(speler.Score.ToString());

								// Update ranking logic
								rang++;
                                prevHoofdpunten = speler.Hoofdpunten;
                                prevScore = speler.Score;
                            }
                        });
                    });
                });
            });

			// Render PDF into the memory stream
			document.GeneratePdf(memoryStream);
			// Reset stream pointer so it can be read from start
			memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }
    }
}
