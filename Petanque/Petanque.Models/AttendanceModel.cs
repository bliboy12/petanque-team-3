using Petanque.Models.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Petanque.Models;

// Dutch: Aanwezigheid (Entity)
public class AttendanceModel
{
	private int? _id;
	private int? _matchDayId;
	private int? _playerId;
	private int _playerNumber;
	// Dutch: AanwezigheidId
	public int? Id
	{
		get { return _id; }
		set
		{
			if (value <= 0)
				throw new AttendanceModelException("Id can not be '0' or negative");
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
				throw new AttendanceModelException("matchdayId can not be '0' or negative");
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
				throw new AttendanceModelException("playerId can not be '0' or negative");
			_playerId = value;
		}
	}

	// Dutch: Speler (object or reference)
	public PlayerModel? Player { get; set; }

	// Dutch: SpelerVolgnr
	public int PlayerNumber
	{
		get { return _playerNumber; }
		set
		{
			if (value <= 0)
				throw new AttendanceModelException("playerNumber can not be '0' or negative");
			_playerNumber = value;
		}
	}
}