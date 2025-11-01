using Petanque.Contracts.Requests;
using Petanque.Contracts.Responses;
using Petanque.Models;
using Petanque.Services.Exceptions;
using Petanque.Storage;
using Petanque.Storage.Entity;

namespace Petanque.Services.Mapping;

public static class MatchDayMappingExtension
{
    public static MatchDayModel AsModel(this Speeldag matchDay)
    {
        return new MatchDayModel
        {
            Id = matchDay.SpeeldagId,
            Date = matchDay.Datum,
            SeasonId = matchDay.SeizoensId,
            Season = matchDay.Seizoens.AsModel(),
            Games = matchDay.Spels.Select(g => g.AsModel()).ToList(),
        };
    }

    public static MatchDayModel AsModel(this SpeeldagRequestContract matchDay)
    {
        return new MatchDayModel
        {
            Date = matchDay.Datum,
            SeasonId = matchDay.SeizoensId
        };
    }

    public static SpeeldagResponseContract AsContract(this MatchDayModel matchDay)
    {
        return new SpeeldagResponseContract
        {
            SpeeldagId = matchDay.Id ?? throw new MappingException(),
            Datum = matchDay.Date,
            Seizoenen = matchDay.Season.AsContract(),
            Spel = matchDay.Games.Select(s => s.AsContract()).ToList(),
        };
    }

    public static Speeldag AsEntity(this MatchDayModel matchDay)
    {
        return new Speeldag
        {
            SpeeldagId = matchDay.Id ?? throw new MappingException(),
            Datum = matchDay.Date,
            SeizoensId = matchDay.SeasonId,
            Seizoens = matchDay.Season.AsEntity(),
            Spels = matchDay.Games.Select(g => g.AsEntity()).ToList(),
        };
    }
}