namespace Petanque.Models;

// Dutch: Aanwezigheid (Entity)
public class AttendanceModel
{
    // Dutch: AanwezigheidId
    public int? Id { get; set; }

    // Dutch: SpeeldagId
    public int? MatchDayId { get; set; }
    
    // Dutch: Speeldag (object or reference)
    public MatchDayModel? MatchDay { get; set; }

    // Dutch: SpelerId
    public int? PlayerId { get; set; }
    
    // Dutch: Speler (object or reference)
    public PlayerModel? Player { get; set; }

    // Dutch: SpelerVolgnr
    public int PlayerNumber { get; set; }
}