using System;
using Xunit;
using Petanque.Storage;

namespace Petanque.UnitTest
{
    public class SpelUnitTest
    {
        [Fact]
        public void Spel_Properties_CanBeSetAndRead()
        {
            // Arrange
            var spel = new Spel
            {
                SpelId = 10,
                SpeeldagId = 5,
                Terrein = "A",
                SpelerVolgnr = 2,
                ScoreA = 13,
                ScoreB = 7
            };

            // Assert
            Assert.Equal(10, spel.SpelId);
            Assert.Equal(5, spel.SpeeldagId);
            Assert.Equal("A", spel.Terrein);
            Assert.Equal(2, spel.SpelerVolgnr);
            Assert.Equal(13, spel.ScoreA);
            Assert.Equal(7, spel.ScoreB);
        }

        [Fact]
        public void Spel_Speeldag_CanBeSetAndRead()
        {
            // Arrange
            var spel = new Spel();
            var speeldag = new Speeldag
            {
                SpeeldagId = 99,
                Datum = new DateTime(2024, 6, 10)
            };

            // Act
            spel.Speeldag = speeldag;

            // Assert
            Assert.Equal(speeldag, spel.Speeldag);
            Assert.Equal(99, spel.Speeldag.SpeeldagId);
        }

        [Fact]
        public void Spel_Spelverdelings_AddSpelverdelingAndEmpty()
        {
            // Arrange
            var spel = new Spel();

            // Assert empty
            Assert.Empty(spel.Spelverdelings);

            // Add item
            var spelverdeling = new Spelverdeling
            {
                SpelverdelingsId = 1,
                SpelId = spel.SpelId,
                Team = "Blue",
                SpelerPositie = "Front",
                SpelerVolgnr = 1
            };
            spel.Spelverdelings.Add(spelverdeling);

            // Assert
            Assert.Single(spel.Spelverdelings);
            Assert.Equal(spelverdeling, Assert.Single(spel.Spelverdelings));
        }
    }
}
