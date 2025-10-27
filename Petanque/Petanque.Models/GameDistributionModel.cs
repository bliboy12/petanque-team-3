namespace Petanque.Models;

public class GameDistributionModel
{
    public int? Id { get; set; }
    public GameModel? Game { get; set; }
    public string Team { get; set; } = string.Empty;
    public string PlayerPosition { get; set; } = string.Empty;
    public int PlayerOrderNumber { get; set; }
    public PlayerModel Player { get; set; }
}