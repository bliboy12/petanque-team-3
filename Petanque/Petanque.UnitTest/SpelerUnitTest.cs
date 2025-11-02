using System;
using Xunit;
using Petanque.Storage;

namespace Petanque.UnitTest
{
    public class SpelerUnitTest
    {
        [Fact]
        public void Speler_Properties_CanBeSetAndRead()
        {
            // Arrange
            var speler = new Speler
            {
                SpelerId = 1,
                Voornaam = "Jan",
                Naam = "Jansen"
            };

            // Assert
            Assert.Equal(1, speler.SpelerId);
            Assert.Equal("Jan", speler.Voornaam);
            Assert.Equal("Jansen", speler.Naam);
        }

        [Fact]
        public void Speler_Aanwezigheids_AddAanwezigheidAndClear()
        {
            var speler = new Speler();
            Assert.Empty(speler.Aanwezigheids);

            var aanwezigheid = new Aanwezigheid { AanwezigheidId = 10, SpelerId = speler.SpelerId };
            speler.Aanwezigheids.Add(aanwezigheid);
            Assert.Single(speler.Aanwezigheids);
            Assert.Equal(aanwezigheid, Assert.Single(speler.Aanwezigheids));

            speler.Aanwezigheids.Clear();
            Assert.Empty(speler.Aanwezigheids);
        }

        [Fact]
        public void Speler_Dagklassements_AddDagklassementAndClear()
        {
            var speler = new Speler();
            Assert.Empty(speler.Dagklassements);

            var dagklassement = new Dagklassement { DagklassementId = 20, SpelerId = speler.SpelerId };
            speler.Dagklassements.Add(dagklassement);
            Assert.Single(speler.Dagklassements);
            Assert.Equal(dagklassement, Assert.Single(speler.Dagklassements));

            speler.Dagklassements.Clear();
            Assert.Empty(speler.Dagklassements);
        }

        [Fact]
        public void Speler_Spelverdelings_AddSpelverdelingAndClear()
        {
            var speler = new Speler();
            Assert.Empty(speler.Spelverdelings);

            var spelverdeling = new Spelverdeling { SpelverdelingsId = 30, SpelerId = speler.SpelerId, Team = "Red", SpelerPositie = "Back", SpelerVolgnr = 1 };
            speler.Spelverdelings.Add(spelverdeling);
            Assert.Single(speler.Spelverdelings);
            Assert.Equal(spelverdeling, Assert.Single(speler.Spelverdelings));

            speler.Spelverdelings.Clear();
            Assert.Empty(speler.Spelverdelings);
        }
    }
}
