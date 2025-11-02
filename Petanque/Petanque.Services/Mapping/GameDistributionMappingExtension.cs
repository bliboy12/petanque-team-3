using Petanque.Contracts.Requests;
using Petanque.Contracts.Responses;
using Petanque.Models;
using Petanque.Services.Exceptions;
using Petanque.Storage;
using Petanque.Storage.Entity;

namespace Petanque.Services.Mapping;

public static class GameDistributionMappingExtension
{
    public static GameDistributionModel AsModel(this Spelverdeling gameDistribution)
    {
        return new GameDistributionModel
        {
            Id = gameDistribution.SpelverdelingsId,
            GameId = gameDistribution.SpelId,
            Team = gameDistribution.Team,
            PlayerPosition = gameDistribution.SpelerPositie,
            PlayerOrderNumber = gameDistribution.SpelerVolgnr,
            PlayerId = gameDistribution.SpelerId,
            Player = gameDistribution.Speler.AsModel(),
            Game = gameDistribution.Spel.AsModel()
        };
    }

    public static GameDistributionModel AsModel(this SpelverdelingRequestContract gameDistribution)
    {
        return new GameDistributionModel
        {
            GameId = gameDistribution.SpelId,
            Team = gameDistribution.Team,
            PlayerPosition = gameDistribution.SpelerPositie,
            PlayerOrderNumber = gameDistribution.SpelerVolgnr
        };
    }

    public static SpelverdelingResponseContract AsContract(this GameDistributionModel gameDistribution)
    {
        return new SpelverdelingResponseContract
        {
            SpelverdelingsId = gameDistribution.Id ?? throw new MappingException(),
            SpelId = gameDistribution.GameId,
            Team = gameDistribution.Team,
            SpelerPositie = gameDistribution.PlayerPosition,
            SpelerVolgnr = gameDistribution.PlayerOrderNumber,
            Speler = gameDistribution.Player.AsContract(),
            Spel = gameDistribution.Game.AsContract()
        };
    }

    public static Spelverdeling AsEntity(this GameDistributionModel gameDistribution)
    {
        return new Spelverdeling
        {
            SpelverdelingsId = gameDistribution.Id ?? throw new MappingException(),
            SpelId = gameDistribution.GameId,
            Team = gameDistribution.Team,
            SpelerPositie = gameDistribution.PlayerPosition,
            SpelerVolgnr = gameDistribution.PlayerOrderNumber,
            SpelerId = gameDistribution.PlayerId,
            Spel = gameDistribution.Game.AsEntity(),
            Speler = gameDistribution.Player.AsEntity()
        };
    }
}