using Petanque.Models.Enums;
using Petanque.Models.Exceptions;

namespace Petanque.Models;

public class PlayerModel
{
	private int? _id;
	private string _firstname = "";
	private string _lastname = "";
	private SkillLevel _skillLevel = SkillLevel.Noob;

	// Dutch: SpelerId
	public int? Id
	{
		get { return _id; }
		set
		{
			if (value <= 0)
				throw new PlayerModelException("Id can not be '0' or negative");
			_id = value;
		}
	}

	// Dutch: Voornaam
	public string Firstname
	{
		get { return _firstname; }
		set
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new PlayerModelException("Firstname can not be empty or null");
			_firstname = value.Trim();
		}
	}

	// Dutch: Achternaam
	public string Lastname
	{
		get { return _lastname; }
		set
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new PlayerModelException("Lastname can not be empty or null");
			_lastname = value.Trim();
		}
	}
	public SkillLevel SkillLevel
	{
		get => _skillLevel;
		set
		{
			if (!Enum.IsDefined(typeof(SkillLevel), value))
				throw new PlayerModelException("Invalid skill level");
			_skillLevel = value;
		}
	}

	// Dutch: Aanwezigheden (lijst van aanwezigheden)
	public List<AttendanceModel> Attendances { get; set; } = new();

	// Dutch: Dagklassementen (lijst van dagklassementen)
	public List<DailyRankingModel> DailyRankings { get; set; } = new();
}