using System;
using Xunit;
using Petanque.Storage;

namespace Petanque.UnitTest
{
    public class SpeelDagUnitTest
    {
        [Fact]
        public void Speeldag_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            var speeldag = new Speeldag
            {
                SpeeldagId = 42,
                Datum = new DateTime(2024, 6, 10),
                SeizoensId = 7
            };

            // Assert
            Assert.Equal(42, speeldag.SpeeldagId);
            Assert.Equal(new DateTime(2024, 6, 10), speeldag.Datum);
            Assert.Equal(7, speeldag.SeizoensId);
        }

        [Fact]
        public void Speeldag_Aanwezigheids_AddAanwezigheid()
        {
            // Arrange
            var speeldag = new Speeldag();
            var aanwezigheid = new Aanwezigheid { AanwezigheidId = 1, SpeeldagId = speeldag.SpeeldagId };

            // Act
            speeldag.Aanwezigheids.Add(aanwezigheid);

            // Assert
            Assert.Single(speeldag.Aanwezigheids);
            Assert.Equal(aanwezigheid, Assert.Single(speeldag.Aanwezigheids));
        }

        [Fact]
        public void Speeldag_Dagklassements_AddDagklassement()
        {
            // Arrange
            var speeldag = new Speeldag();
            var dagklassement = new Dagklassement { DagklassementId = 2, SpeeldagId = speeldag.SpeeldagId };

            // Act
            speeldag.Dagklassements.Add(dagklassement);

            // Assert
            Assert.Single(speeldag.Dagklassements);
            Assert.Equal(dagklassement, Assert.Single(speeldag.Dagklassements));
        }

        [Fact]
        public void Speeldag_Spels_AddSpel()
        {
            // Arrange
            var speeldag = new Speeldag();
            var spel = new Spel { SpelId = 3, SpeeldagId = speeldag.SpeeldagId, Terrein = "B" };

            // Act
            speeldag.Spels.Add(spel);

            // Assert
            Assert.Single(speeldag.Spels);
            Assert.Equal(spel, Assert.Single(speeldag.Spels));
        }
    }
}
