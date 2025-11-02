using Petanque.Models.Exceptions;

namespace Petanque.Models;

// Dutch: Spelverdeling (entity)
public class GameDistributionModel
{
    private int? _id;
    private int? _gameId;
    private int? _playerId;
    private int _playerOrderNumber;
    private string _team;
    private string _playerPosition;

    // Dutch: SpelverdelingId
    public int? Id
    {
        get { return _id; }
        set
        {
            if (value <= 0)
                throw new GameDistributionModelException("Id can not be '0' or negative");
            _id = value;
        }
    }

    // Dutch: SpelId
    public int? GameId
    {
        get { return _gameId; }
        set
        {
            if (value <= 0)
                throw new GameDistributionModelException("GameId can not be '0' or negative");
            _gameId = value;
        }
    }

    // Dutch: Spel (object or reference)
    public GameModel? Game { get; set; }

    // Dutch: Team
    public string Team
    {
        get { return _team; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new GameDistributionModelException("Team can not be empty or null");
            _team = value.Trim();
        }
    }

    // Dutch: SpelerPositie
    public string PlayerPosition
    {
        get { return _playerPosition; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new GameDistributionModelException("PlayerPosition can not be empty or null");
            _playerPosition = value.Trim();
        }
    }

    // Dutch: SpelerVolgnr
    public int PlayerOrderNumber
    {
        get { return _playerOrderNumber; }
        set
        {
            if (value <= 0)
                throw new GameDistributionModelException("PlayerOrderNumber can not be '0' or negative");
            _playerOrderNumber = value;
        }
    }

    // Dutch: SpelerId
    public int? PlayerId
    {
        get { return _playerId; }
        set
        {
            if (value <= 0)
                throw new GameDistributionModelException("PlayerId can not be '0' or negative");
            _playerId = value;
        }
    }

    // Dutch: Speler (object or reference)
    public PlayerModel? Player { get; set; }
}
