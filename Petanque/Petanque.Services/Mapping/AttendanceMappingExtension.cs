using Petanque.Models;
using Petanque.Storage;

namespace Petanque.Services.Mapping;

public static class AttendanceMappingExtension
{
    public static AttendanceModel AsModel(this Aanwezigheid attendance)
    {
        /*return new AttendanceModel
        {
            Id = attendance.AanwezigheidId,
        }*/
    }
}