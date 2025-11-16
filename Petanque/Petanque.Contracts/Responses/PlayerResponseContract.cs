using Petanque.Models.Enums;

namespace Petanque.Contracts.Responses
{
    public class PlayerResponseContract
    {
        public int SpelerId { get; set; }

        public string Voornaam { get; set; } = null!;

        public string Naam { get; set; } = null!;

        public SkillLevel SkillLevel { get; set;}

        public virtual ICollection<AanwezigheidResponseContract> Aanwezigheids { get; set; } = new List<AanwezigheidResponseContract>();

        public virtual ICollection<DagKlassementResponseContract> Dagklassements { get; set; } = new List<DagKlassementResponseContract>();
    }
}
