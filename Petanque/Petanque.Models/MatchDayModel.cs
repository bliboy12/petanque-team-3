using Petanque.Models.Exceptions;

namespace Petanque.Models;

// Dutch: Speeldag (entity)
public class MatchDayModel
{
    private int? _id;
    private DateTime _date;
    private int? _seasonId;

    // Dutch: SpeeldagId
    public int? Id
    {
        get { return _id; }
        set
        {
            if (value <= 0)
                throw new MatchDayModelException("Id can not be '0' or negative");
            _id = value;
        }
    }

    // Dutch: Datum
    public DateTime Date
    {
        get { return _date; }
        set
        {
            if (value == default)
                throw new MatchDayModelException("Date can not be empty or invalid");
            _date = value;
        }
    }

    // Dutch: SeizoenId
    public int? SeasonId
    {
        get { return _seasonId; }
        set
        {
            if (value <= 0)
                throw new MatchDayModelException("SeasonId can not be '0' or negative");
            _seasonId = value;
        }
    }

    // Dutch: Seizoen (object or reference)
    public SeasonModel? Season { get; set; }

    // Dutch: Spellen (lijst van spellen)
    public List<GameModel> Games { get; set; } = new();

}
