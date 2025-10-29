namespace Petanque.Models;

// Dutch: Dagklassement (entity)
public class DailyRankingModel
{
    // Dutch: DagklassementId
    public int? Id { get; set; }

    // Dutch: SpeeldagId
    public int? MatchDayId { get; set; }
    
    // Dutch: Speeldag (object or reference)
    public MatchDayModel? MatchDay { get; set; }

    // Dutch: SpelerId
    public int? PlayerId { get; set; }
    
    // Dutch: Speler (object or reference)
    public PlayerModel? Player { get; set; }

    // Dutch: Hoofdpunten
    public int MainPoints { get; set; }

    // Dutch: PlusMinPunten
    public int PlusMinPoints { get; set; }
}