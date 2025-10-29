namespace Petanque.Models;

// Dutch: Spelverdeling (entity)
public class GameDistributionModel
{
    // Dutch: SpelverdelingsId
    public int? Id { get; set; }
    
    // Dutch: SpelId
    public int? GameId { get; set; }
    
    // Dutch: Spel (reference to Spel)
    public GameModel? Game { get; set; }
    
    // Dutch: Team
    public string Team { get; set; } = string.Empty;
    
    // Dutch: SpelerPositie
    public string PlayerPosition { get; set; } = string.Empty;
    
    // Dutch: SpelerVolgnr
    public int PlayerOrderNumber { get; set; }
    
    // Dutch: SpelerId
    public int? PlayerId { get; set; }
    
    // Dutch: Speler (reference to Speler)
    public PlayerModel? Player { get; set; }
}