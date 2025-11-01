using Petanque.Models.Exceptions;
using System.Security.Cryptography;

namespace Petanque.Models;

// Dutch: Seizoen (entity)
public class SeasonModel
{
	private int? _id;
	private DateOnly _startDate;
	private DateOnly _endDate;
    // Dutch: SeizoensId
    public int? Id
	{
		get { return _id; }
		set
		{
			if (value <= 0)
				throw new SeasonModelException("Id can not be '0' or negative");
			_id = value;
		}
	}

	// Dutch: Startdatum
	public DateOnly StartDate
	{
		get { return _startDate; }
		set
		{
			if (_endDate != default && value > _endDate)
				throw new SeasonModelException("startDate can't be later then endDate");
			_startDate = value;
		}
	}

	// Dutch: Einddatum
	public DateOnly EndDate
	{
		get { return _endDate; }
		set
		{
			if (_startDate != default && value < _startDate)
				throw new SeasonModelException("endDate can't be earlier then startDate");
			_endDate = value;
		}
	}

	// Dutch: Speeldags (collection)
	public List<MatchDayModel> MatchDays { get; set; } = new();
}