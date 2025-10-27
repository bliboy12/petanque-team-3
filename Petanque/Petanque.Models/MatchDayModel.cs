namespace Petanque.Models;

public class MatchDayModel
{
    // Dutch: SpeeldagId
    public int? Id { get; set; }

    // Dutch: Datum
    public DateTime Date { get; set; }

    // Dutch: Seizoen (reference to Seizoen)
    public SeasonModel? Season { get; set; }

    // Dutch: Spel (lijst van spellen)
    public List<GameModel> Games { get; set; } = new();
}