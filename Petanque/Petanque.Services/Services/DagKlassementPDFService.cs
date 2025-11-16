using Petanque.Services.Interfaces;
using Petanque.Storage;
using Petanque.Storage.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Petanque.Services.Services
{
    public class DagKlassementPDFService(IDagKlassementRepository dagKlassementRepository, ISpelerRepository spelerRepository, ISpeeldagRepository speeldagRepository) : IDagKlassementPDFService
    {
        public async Task<Stream> GenerateDagKlassementPdfAsync(int id)
        {
            var speeldag = speeldagRepository.GetById(id);
            if (speeldag == null)
                return null;

            var spelerIdsInDagklassement = dagKlassementRepository.GetById(id).Select(d => d.SpelerId).ToList();

            var spelers = spelerRepository.GetBySpelerIds(spelerIdsInDagklassement);

            var dagklassements = dagKlassementRepository.GetById(id);

            if (dagklassements == null || !dagklassements.Any())
                return null;

            var spelersMetScores = spelers.Select(speler =>
            {
                var dagKlassement = dagklassements.FirstOrDefault(dk => dk.SpelerId == speler.SpelerId);
                return new
                {
                    speler.Naam,
                    speler.Voornaam,
                    Score = dagKlassement?.PlusMinPunten ?? 0,
                    Hoofdpunten = dagKlassement?.Hoofdpunten ?? 0
                };
            }).OrderByDescending(s => s.Hoofdpunten).ThenByDescending(s => s.Score).ToList();

            var memoryStream = new MemoryStream();

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

                        col.Item().Element(e => e
                            .PaddingBottom(2)
                            .Text($"VL@S - Dagklassement - {datumFormatted}")
                            .FontSize(14)
                            .Bold()
                            .AlignCenter());

                        col.Item().Element(e => e.PaddingTop(10));

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(25);
                                columns.RelativeColumn(3);
                                columns.ConstantColumn(35);
                                columns.ConstantColumn(35);
                            });

                            int rang = 1;
                            int prevHoofdpunten = 0;
                            int prevScore = 0;
                            foreach (var speler in spelersMetScores)
                            {
                                bool isEvenRow = rang % 2 == 0;
                                string background = isEvenRow ? Colors.Grey.Lighten4 : Colors.White;

                                if ((speler.Hoofdpunten == prevHoofdpunten) && (speler.Score == prevScore))
                                    table.Cell().Element(e => e.Background(background).PaddingVertical(2)).Text(' ');
                                else
                                    table.Cell().Element(e => e.Background(background).PaddingVertical(2)).Text(rang.ToString());
                                table.Cell().Element(e => e.Background(background).PaddingVertical(2)).Text($"{speler.Naam} {speler.Voornaam}");
                                table.Cell().Element(e => e.Background(background).PaddingVertical(2)).AlignCenter().Text(speler.Hoofdpunten.ToString());
                                table.Cell().Element(e => e.Background(background).PaddingVertical(2)).AlignCenter().Text(speler.Score.ToString());

                                rang++;
                                prevHoofdpunten = speler.Hoofdpunten;
                                prevScore = speler.Score;
                            }
                        });
                    });
                });
            });

            document.GeneratePdf(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }


    }
}
