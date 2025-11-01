using Petanque.Contracts.Requests;
using Petanque.Contracts.Responses;
using Petanque.Models;
using Petanque.Services.Exceptions;
using Petanque.Storage.Entity;

namespace Petanque.Services.Mapping;

public static class DailyRankingMappingExtension
{
    public static DailyRankingModel AsModel(this Dagklassement dailyRanking)
    {
        return new DailyRankingModel
        {
            Id = dailyRanking.DagklassementId,
            MatchDayId = dailyRanking.SpeeldagId,
            MatchDay = dailyRanking.Speeldag.AsModel(),
            PlayerId = dailyRanking.SpelerId,
            Player = dailyRanking.Speler.AsModel(),
            MainPoints = dailyRanking.Hoofdpunten,
            PlusMinPoints = dailyRanking.PlusMinPunten,
        };
    }

    public static DailyRankingModel AsModel(this DagKlassementRequestContract dailyRanking)
    {
        return new DailyRankingModel
        {
            MatchDayId = dailyRanking.SpeeldagId,
            PlayerId = dailyRanking.SpelerId,
            MainPoints = dailyRanking.Hoofdpunten,
            PlusMinPoints = dailyRanking.PlusMinPunten
        };
    }

    public static DagKlassementResponseContract AsContract(this DailyRankingModel dailyRanking)
    {
        return new DagKlassementResponseContract
        {
            DagklassementId = dailyRanking.Id ?? throw new MappingException(),
            SpeeldagId = dailyRanking.MatchDayId,
            SpelerId = dailyRanking.PlayerId,
            Hoofdpunten = dailyRanking.MainPoints,
            PlusMinPunten = dailyRanking.PlusMinPoints
        };
    }

    public static Dagklassement AsEntity(this DailyRankingModel dailyRanking)
    {
        return new Dagklassement
        {
            DagklassementId = dailyRanking.Id ?? throw new MappingException(),
            SpeeldagId = dailyRanking.MatchDayId,
            SpelerId = dailyRanking.PlayerId,
            Hoofdpunten = dailyRanking.MainPoints,
            PlusMinPunten = dailyRanking.PlusMinPoints,
            Speeldag = dailyRanking.MatchDay.AsEntity(),
            Speler = dailyRanking.Player.AsEntity()
        };
    }
}