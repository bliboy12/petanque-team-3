using System;
using Xunit;
using Petanque.Storage;

namespace Petanque.UnitTest
{
    public class SpeelDagUnitTest
    {
        [Fact]
        public void Speeldag_Properties_CanBeSetAndRead()
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
        public void Speeldag_Aanwezigheids_AddAanwezigheidAndEmpty()
        {
            // Arrange
            var speeldag = new Speeldag();

            // Assert empty
            Assert.Empty(speeldag.Aanwezigheids);

            // Add item
            var aanwezigheid = new Aanwezigheid { AanwezigheidId = 1, SpeeldagId = speeldag.SpeeldagId };
            speeldag.Aanwezigheids.Add(aanwezigheid);

            // Assert
            Assert.Single(speeldag.Aanwezigheids);
            Assert.Equal(aanwezigheid, Assert.Single(speeldag.Aanwezigheids));
        }

        [Fact]
        public void Speeldag_Dagklassements_AddDagklassementAndEmpty()
        {
            // Arrange
            var speeldag = new Speeldag();

            // Assert empty
            Assert.Empty(speeldag.Dagklassements);

            // Add item
            var dagklassement = new Dagklassement { DagklassementId = 2, SpeeldagId = speeldag.SpeeldagId };
            speeldag.Dagklassements.Add(dagklassement);

            // Assert
            Assert.Single(speeldag.Dagklassements);
            Assert.Equal(dagklassement, Assert.Single(speeldag.Dagklassements));
        }

        [Fact]
        public void Speeldag_Spels_AddSpelAndEmpty()
        {
            // Arrange
            var speeldag = new Speeldag();

            // Assert empty
            Assert.Empty(speeldag.Spels);

            // Add item
            var spel = new Spel { SpelId = 3, SpeeldagId = speeldag.SpeeldagId, Terrein = "B" };
            speeldag.Spels.Add(spel);

            // Assert
            Assert.Single(speeldag.Spels);
            Assert.Equal(spel, Assert.Single(speeldag.Spels));
        }

        [Fact]
        public void Speeldag_Seizoens_CanBeSetAndRead()
        {
            // Arrange
            var speeldag = new Speeldag();
            var seizoen = new Seizoen
            {
                SeizoensId = 99,
                Startdatum = new DateOnly(2024, 1, 1),
                Einddatum = new DateOnly(2024, 12, 31)
            };

            // Act
            speeldag.Seizoens = seizoen;

            // Assert
            Assert.Equal(seizoen, speeldag.Seizoens);
            Assert.Equal(99, speeldag.Seizoens.SeizoensId);
        }
    }
}
