using Petanque.Contracts.Requests;
using Petanque.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Services.Interfaces;

public interface IDagKlassementService 
{
    IEnumerable<DagKlassementResponseContract>? GetById(int id);
    DagKlassementResponseContract Create(DagKlassementRequestContract request);
    
    /** Service to generate all the daily rankings for a specific match day */
    IEnumerable<DagKlassementResponseContract> CreateDailyRankings(int matchDayId);
}