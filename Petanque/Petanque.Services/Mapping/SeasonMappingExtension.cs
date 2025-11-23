using Petanque.Contracts.Requests;
using Petanque.Contracts.Responses;
using Petanque.Models;
using Petanque.Services.Exceptions;
using Petanque.Storage;
using Petanque.Storage.Entity;

namespace Petanque.Services.Mapping;

public static class SeasonMappingExtension
{
    public static SeasonModel AsModel(this Seizoen season)
    {
        return new SeasonModel
        {
            Id = season.SeizoensId,
            StartDate = season.Startdatum,
            EndDate = season.Einddatum,
            // MatchDays = season.Speeldags.Select(m => m.AsModel()).ToList() // Create a circular reference
            MatchDays = new List<MatchDayModel>()
        };
    }

    public static SeasonModel AsModel(this SeizoenRequestContract season)
    {
        return new SeasonModel
        {
            StartDate = season.Startdatum,
            EndDate = season.Einddatum,
        };
    }

    public static SeizoenResponseContract AsContract(this SeasonModel season)
    {
        return new SeizoenResponseContract
        {
            SeizoensId = season.Id ?? throw new MappingException(),
            Startdatum = season.StartDate,
            Einddatum = season.EndDate,
            Speeldags = season.MatchDays.Select(m => m.AsContract()).ToList()
        };
    }

    public static Seizoen AsEntity(this SeasonModel season)
    {
        return new Seizoen
        {
            SeizoensId = season.Id ?? throw new MappingException(),
            Startdatum = season.StartDate,
            Einddatum = season.EndDate,
            Speeldags = season.MatchDays.Select(m => m.AsEntity()).ToList()
        };
    }
}