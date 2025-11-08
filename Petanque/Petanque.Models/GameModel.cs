using Petanque.Models.Exceptions;

namespace Petanque.Models;

// Dutch: Spel (entity)
public class GameModel
{
	private int? _id;
	private int? _matchDayId;
	private string _terrain;
	private int _scoreA;
	private int _scoreB;

	// Dutch: SpelId
	public int? Id
	{
		get { return _id; }
		set
		{
			if (value <= 0)
				throw new GameModelException("Id can not be '0' or negative");
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
				throw new GameModelException("MatchDayId can not be '0' or negative");
			_matchDayId = value;
		}
	}

	// Dutch: Speeldag (object or reference)
	public MatchDayModel? MatchDay { get; set; }

	// Dutch: Terrein
	public string Terrain
	{
		get { return _terrain; }
		set
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new GameModelException("Terrain can not be empty or null");
			_terrain = value.Trim();
		}
	}

	// Dutch: ScoreA
	public int ScoreA
	{
		get { return _scoreA; }
		set
		{
			if (value < 0 || value > 13)
				throw new GameModelException("ScoreA must be between 0 and 13");
			_scoreA = value;
		}
	}

	// Dutch: ScoreB
	public int ScoreB
	{
		get { return _scoreB; }
		set
		{
			if (value < 0 || value > 13)
				throw new GameModelException("ScoreB must be between 0 and 13");
			_scoreB = value;
		}
	}

	// Dutch: Spelverdelings (collection)
	public List<GameDistributionModel> GameDistributions { get; set; } = new();
}