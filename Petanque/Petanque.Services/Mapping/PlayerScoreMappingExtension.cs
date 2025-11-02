using Petanque.Contracts.Responses;
using Petanque.Models;

namespace Petanque.Services.Mapping;

public static class PlayerScoreMappingExtension
{
    public static SpelerScoresResponseContract AsContract(this PlayerScoreModel playerScore)
    {
        return new SpelerScoresResponseContract
        {
            SpelerVolgNr = playerScore.PlayerOrderNumber,
            ScoreA = playerScore.ScoreA,
            ScoreB = playerScore.ScoreB
        };
    }
}