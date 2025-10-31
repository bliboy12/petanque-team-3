namespace Petanque.Models;

// Dutch: Seizoen (entity)
public class SeasonModel
{
    // Dutch: SeizoensId
    public int? Id { get; set; }

    // Dutch: Startdatum
    public DateOnly StartDate { get; set; }

    // Dutch: Einddatum
    public DateOnly EndDate { get; set; }

    // Dutch: Speeldags (collection)
    public List<MatchDayModel> MatchDays { get; set; } = new();
}