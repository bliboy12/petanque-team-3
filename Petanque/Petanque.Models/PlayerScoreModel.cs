using Petanque.Models.Exceptions;
using System.Security.Cryptography;

namespace Petanque.Models;

// Dutch : Spelerscore (entity)
public class PlayerScoreModel
{
	private int? _id;
	private int _playerOrderNumber;
	private int _scoreA;
	private int _scoreB;
    // Dutch: SpelerScoresId
    public int? Id
	{
		get { return _id; }
		set
		{
			if (value <= 0)
				throw new PlayerScoreModelException("Id can not be '0' or negative");
			_id = value;
		}
	}

	// Dutch: SpelerVolgNr
	public int PlayerOrderNumber
	{
		get { return _playerOrderNumber; }
		set
		{
			if (value <= 0)
				throw new PlayerScoreModelException("playerOrderNumber can not be '0' or negative");
			_playerOrderNumber = value;
		}
	}

	// Dutch: ScoreA
	public int ScoreA
	{
		get { return _scoreA; }
		set
		{
			if (value < -13 || value > 13)
				throw new PlayerScoreModelException("scoreA can only have a max point values of (-13 or +13)");
			_scoreA = value;
		}
	}

	// Dutch: ScoreB
	public int ScoreB
	{
		get { return _scoreB; }
		set
		{
			if (value < -13 || value > 13)
				throw new PlayerScoreModelException("scoreB can only have a max point values of (-13 or +13)");
			_scoreB = value;
		}
	}
}