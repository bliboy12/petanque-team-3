namespace Petanque.Models;

public class DailyRankingModel
{
    public int? Id { get; set; }
    public MatchDayModel? MatchDay { get; set; }
    public PlayerModel? Player { get; set; }
    public int MainPoints { get; set; }
    public int PlusMinPoints { get; set; }
}