namespace Petanque.Models;

public class AttendanceModel
{
    // Dutch: AanwezigheidId
    public int? Id { get; set; }

    // Dutch: Speeldag (object or reference)
    public MatchDayModel? MatchDay { get; set; }

    // Dutch: Speler (object or reference)
    public PlayerModel? Player { get; set; }

    // Dutch: SpelerVolgnr
    public int PlayerNumber { get; set; }

    // Dutch: SpeeldagDatum
    public string MatchDayDate { get; set; } = string.Empty;
}