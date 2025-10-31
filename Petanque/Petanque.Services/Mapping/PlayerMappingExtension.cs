using Petanque.Contracts.Requests;
using Petanque.Contracts.Responses;
using Petanque.Models;
using Petanque.Services.Exceptions;
using Petanque.Storage;

namespace Petanque.Services.Mapping;

public static class PlayerMappingExtension
{
    public static PlayerModel AsModel(this Speler speler)
    {
        return new PlayerModel
        {
            Id = speler.SpelerId,
            Firstname = speler.Voornaam,
            Lastname = speler.Naam,
            Attendances = speler.Aanwezigheids.Select(a => a.AsModel()).ToList(),
            DailyRankings = speler.Dagklassements.Select(d => d.AsModel()).ToList()
        };
    }

    public static PlayerModel AsModel(this PlayerRequestContract player)
    {
        return new PlayerModel
        {
            Firstname = player.Voornaam,
            Lastname = player.Naam
        };
    }

    public static PlayerResponseContract AsContract(this PlayerModel player)
    {
        return new PlayerResponseContract
        {
            SpelerId = player.Id ?? throw new MappingException(),
            Voornaam = player.Firstname,
            Naam = player.Lastname,
            Aanwezigheids = player.Attendances.Select(a => a.AsContract()).ToList(),
            Dagklassements = player.DailyRankings.Select(d => d.AsContract()).ToList()
        };
    }

    public static Speler AsEntity(this PlayerModel player)
    {
        return new Speler
        {
            SpelerId = player.Id ?? throw new MappingException(),
            Voornaam = player.Firstname,
            Naam = player.Lastname
        };
    }
}