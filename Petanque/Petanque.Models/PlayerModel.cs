namespace Petanque.Models;

public class PlayerModel
{
    public int? Id {  get; set; }
    public required string Firstname { get; set; }
    public required string Lastname { get; set; }
    public List<AttendanceModel> Attendances { get; set; } = new();
    public List<DailyRankingModel> DailyRankings { get; set; } = new();
}