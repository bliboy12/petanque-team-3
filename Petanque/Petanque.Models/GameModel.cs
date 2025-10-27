namespace Petanque.Models;

public class GameModel
{
    public int? Id { get; set; }
    public MatchDayModel? MatchDay { get; set; }
    public string Terrain { get; set; } = string.Empty;
    public int ScoreA { get; set; }
    public int ScoreB { get; set; }
    public List<GameDistributionModel> GameDistributions { get; set; } = new();
}