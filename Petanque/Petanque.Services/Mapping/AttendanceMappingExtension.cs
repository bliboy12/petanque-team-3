using Petanque.Contracts.Requests;
using Petanque.Contracts.Responses;
using Petanque.Models;
using Petanque.Services.Exceptions;
using Petanque.Storage;

namespace Petanque.Services.Mapping;

public static class AttendanceMappingExtension
{
    public static AttendanceModel AsModel(this Aanwezigheid attendance)
    {
        return new AttendanceModel
        {
            Id = attendance.AanwezigheidId,
            MatchDayId = attendance.SpeeldagId,
            PlayerId = attendance.SpelerId,
            PlayerNumber = attendance.SpelerVolgnr
        };
    }

    public static AttendanceModel AsModel(this AanwezigheidRequestContract attendance)
    {
        return new AttendanceModel
        {
            MatchDayId = attendance.SpeeldagId,
            PlayerId = attendance.SpelerId,
            PlayerNumber = attendance.SpelerVolgnr
        };
    }

    public static AanwezigheidResponseContract AsContract(this AttendanceModel attendance)
    {
        return new AanwezigheidResponseContract
        {
            AanwezigheidId = attendance.Id ?? throw new MappingException(),
            SpeeldagId = attendance.MatchDayId,
            SpelerId = attendance.PlayerId,
            SpelerVolgnr = attendance.PlayerNumber
        };
    }

    public static Aanwezigheid AsEntity(this AttendanceModel attendance)
    {
        return new Aanwezigheid
        {
            AanwezigheidId = attendance.Id ?? throw new MappingException(),
            SpeeldagId = attendance.MatchDayId,
            SpelerId = attendance.PlayerId,
            SpelerVolgnr = attendance.PlayerNumber,
            Speeldag = attendance.MatchDay.AsEntity(),
            Speler = attendance.Player.AsEntity()
        };
    }
}