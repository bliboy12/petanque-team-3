using Petanque.Models.Exceptions;
using System.Security.Cryptography;

namespace Petanque.Models;

// Dutch: Dagklassement (entity)
public class DailyRankingModel
{
    private int? _id;
    private int? _matchDayId;
    private int? _playerId;
    private int _mainPoints;
    private int _plusMinPoints;
    // Dutch: DagklassementId
    public int? Id
	{
		get { return _id; }
		set
		{
			if (value <= 0)
				throw new DailyRankingModelException("Id can not be '0' or negative");
			_id = value;
		}
	}

	// Dutch: SpeeldagId
	public int? MatchDayId
	{
		get { return _matchDayId; }
		set
		{
			if (value <= 0)
				throw new DailyRankingModelException("matchDayId can not be '0' or negative");
			_matchDayId = value;
		}
	}

	// Dutch: Speeldag (object or reference)
	public MatchDayModel? MatchDay { get; set; }

    // Dutch: SpelerId
    public int? PlayerId
	{
		get { return _playerId; }
		set
		{
			if (value <= 0)
				throw new DailyRankingModelException("playerId can not be '0' or negative");
			_playerId = value;
		}
	}

	// Dutch: Speler (object or reference)
	public PlayerModel? Player { get; set; }

    // Dutch: Hoofdpunten
    public int MainPoints
	{
		get { return _mainPoints; }
		set
		{
			if (value < 0)
				throw new DailyRankingModelException("mainPoints can only have a max point values of (-13 and +13)");
			_mainPoints = value;
		}
	}

	// Dutch: PlusMinPunten
	// how do check which numbers are valid?
	public int PlusMinPoints
	{
		get { return _plusMinPoints; }
		set
		{
			_plusMinPoints = value;
		}
	}
}