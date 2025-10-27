namespace Petanque.Models;

public class PlayerScoresModel
{
    // Dutch: SpelerScoresId
    public int? Id { get; set; }

    // Dutch: SpelerVolgNr
    public int PlayerOrderNumber  { get; set; }

    // Dutch: ScoreA
    public int ScoreA { get; set; }

    // Dutch: ScoreB
    public int ScoreB { get; set; }
}