namespace Petanque.Models;

// Dutch: Speeldag (entity)
public class MatchDayModel
{
    // Dutch: SpeeldagId
    public int? Id { get; set; }

    // Dutch: Datum
    public DateTime Date { get; set; }

    // Dutch: SeizoenId
    public int? SeasonId { get; set; }
    
    // Dutch: Seizoen (reference to Seizoen)
    public SeasonModel? Season { get; set; }

    // Dutch: Spel (lijst van spellen)
    public List<GameModel> Games { get; set; } = new();
}