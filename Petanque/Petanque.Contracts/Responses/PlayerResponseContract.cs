using Petanque.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Contracts.Responses
{
    public class PlayerResponseContract
    {
        public int SpelerId { get; set; }

        public string Voornaam { get; set; } = null!;

        public string Naam { get; set; } = null!;

        public int SkillLevel { get; set;}

        public virtual ICollection<AanwezigheidResponseContract> Aanwezigheids { get; set; } = new List<AanwezigheidResponseContract>();

        public virtual ICollection<DagKlassementResponseContract> Dagklassements { get; set; } = new List<DagKlassementResponseContract>();
    }
}
