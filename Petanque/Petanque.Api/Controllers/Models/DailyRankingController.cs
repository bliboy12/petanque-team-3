using Microsoft.AspNetCore.Mvc;
using Petanque.Contracts.Requests;
using Petanque.Contracts.Responses;
using Petanque.Services.Interfaces;

namespace Petanque.Api.Controllers.Models;

[Route("api/dailyrankings")]
[ApiController]
public class DailyRankingController(IDagKlassementService service, ISpeeldagService Sservice) : ControllerBase {
    
    [HttpGet("{id}")]
    public ActionResult<IEnumerable<DagKlassementResponseContract>> Get([FromRoute] int id) {
        var dagklassement = service.GetById(id);
        if (dagklassement is null) return NotFound();
        return Ok(dagklassement);
    }

    [HttpPost]
    public ActionResult<DagKlassementResponseContract> Create(
        [FromBody] DagKlassementRequestContract request) {
        var created = service.Create(request);
        return CreatedAtAction(nameof(Get), new { id = created.SpelerId }, created);
    }
        
    /** Endpoint to generate all the daily rankings for a specific match day */
    [HttpPost("{matchDayId}")]
    public ActionResult<IEnumerable<DagKlassementResponseContract>> CreateDailyRankings([FromRoute] int matchDayId)
    {
        var dailyRankings = service.CreateDailyRankings(matchDayId);
        return Ok(dailyRankings);
    }
}