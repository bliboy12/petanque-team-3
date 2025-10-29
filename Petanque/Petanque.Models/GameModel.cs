namespace Petanque.Models;

// Dutch: Spel (entity)
public class GameModel
{
    // Dutch: SpelId
    public int? Id { get; set; }

    // Dutch: SpeeldagId
    public int? MatchDayId { get; set; }

    // Dutch: Speeldag (reference to Speeldag)
    public MatchDayModel? MatchDay { get; set; }

    // Dutch: Terrein
    public string Terrain { get; set; } = string.Empty;

    // Dutch: ScoreA
    public int ScoreA { get; set; }

    // Dutch: ScoreB
    public int ScoreB { get; set; }

    // Dutch: Spelverdelings (collection)
    public List<GameDistributionModel> GameDistributions { get; set; } = new();
}