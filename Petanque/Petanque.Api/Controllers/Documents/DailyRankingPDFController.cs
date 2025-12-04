using Microsoft.AspNetCore.Mvc;
using Petanque.Services.Interfaces;

namespace Petanque.Api.Controllers.Documents;

[Route("api/pdf/dailyrankings")]
[ApiController]
public class DailyRankingPDFController(IDagKlassementPDFService dagKlassementPdfService) : ControllerBase
{
    [HttpPost("{id}")]
    public async Task<IActionResult> GeneratePdf(int id)
    {
        // Roep de service aan om de PDF te genereren
        var pdfStream = await dagKlassementPdfService.GenerateDagKlassementPdfAsync(id);

        if (pdfStream == null)
        {
            return NotFound($"Dagklassement voor id {id} niet gevonden.");
        }

        // Geef de PDF terug als een bestand naar de client
        return File(pdfStream, "application/pdf", $"DagKlassement_{id}.pdf");
    }
}