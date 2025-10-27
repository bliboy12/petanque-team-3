namespace Petanque.Models;

public class SeasonModel
{
    public int? Id { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public List<AttendanceModel> Attendances { get; set; } = new();
}