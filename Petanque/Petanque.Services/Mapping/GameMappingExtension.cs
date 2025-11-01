using Petanque.Contracts.Requests;
using Petanque.Contracts.Responses;
using Petanque.Models;
using Petanque.Services.Exceptions;
using Petanque.Storage;
using Petanque.Storage.Entity;

namespace Petanque.Services.Mapping;

public static class GameMappingExtension
{
    public static GameModel AsModel(this Spel game)
    {
        return new GameModel
        {
            Id = game.SpelId,
            MatchDayId = game.SpeeldagId,
            Terrain = game.Terrein,
            ScoreA = game.ScoreA,
            ScoreB = game.ScoreB,
            MatchDay = game.Speeldag.AsModel(),
            GameDistributions = game.Spelverdelings.Select(g => g.AsModel()).ToList()
        };
    }

    public static GameModel AsModel(this SpelRequestContract game)
    {
        return new GameModel
        {
            MatchDayId = game.SpeeldagId,
            Terrain = game.Terrein,
            ScoreA = game.ScoreA,
            ScoreB = game.ScoreB
        };
    }

    public static SpelResponseContract AsContract(this GameModel game)
    {
        return new SpelResponseContract
        {
            SpelId = game.Id ?? throw new MappingException(),
            SpeeldagId = game.MatchDayId,
            Terrein = game.Terrain,
            ScoreA = game.ScoreA,
            ScoreB = game.ScoreB,
            Spelverdelingen = game.GameDistributions.Select(g => g.AsContract()).ToList()
        };
    }

    public static Spel AsEntity(this GameModel game)
    {
        return new Spel
        {
            SpelId = game.Id ?? throw new MappingException(),
            SpeeldagId = game.MatchDayId,
            Terrein = game.Terrain,
            ScoreA = game.ScoreA,
            ScoreB = game.ScoreB,
            Speeldag = game.MatchDay.AsEntity(),
            Spelverdelings = game.GameDistributions.Select(g => g.AsEntity()).ToList()
        };
    }
}