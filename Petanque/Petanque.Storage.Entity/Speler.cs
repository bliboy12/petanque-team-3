using Petanque.Storage.Entity;
using System;
using System.Collections.Generic;

namespace Petanque.Storage.Entity {
    public partial class Speler {
        public int SpelerId { get; set; }

        public string Voornaam { get; set; } = null!;

        public string Naam { get; set; } = null!;

        public int SkillLevel { get; set; } = 0;

        public virtual ICollection<Aanwezigheid> Aanwezigheids { get; set; } = new List<Aanwezigheid>();

        public virtual ICollection<Dagklassement> Dagklassements { get; set; } = new List<Dagklassement>();

        public virtual ICollection<Spelverdeling> Spelverdelings { get; set; } = new List<Spelverdeling>();
    }
}


