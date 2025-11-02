using System;
using Xunit;
using Petanque.Storage;

namespace Petanque.UnitTest
{
    public class SpelverdelingUnitTest
    {
        [Fact]
        public void Spelverdeling_Properties_CanBeSetAndRead()
        {
            // Arrange
            var spelverdeling = new Spelverdeling
            {
                SpelverdelingsId = 1,
                SpelId = 2,
                Team = "Red",
                SpelerPositie = "Front",
                SpelerVolgnr = 3,
                SpelerId = 4
            };

            // Assert
            Assert.Equal(1, spelverdeling.SpelverdelingsId);
            Assert.Equal(2, spelverdeling.SpelId);
            Assert.Equal("Red", spelverdeling.Team);
            Assert.Equal("Front", spelverdeling.SpelerPositie);
            Assert.Equal(3, spelverdeling.SpelerVolgnr);
            Assert.Equal(4, spelverdeling.SpelerId);
        }

        [Fact]
        public void Spelverdeling_Spel_CanBeSetAndRead()
        {
            // Arrange
            var spelverdeling = new Spelverdeling();
            var spel = new Spel
            {
                SpelId = 10,
                Terrein = "A"
            };

            // Act
            spelverdeling.Spel = spel;

            // Assert
            Assert.Equal(spel, spelverdeling.Spel);
            Assert.Equal(10, spelverdeling.Spel.SpelId);
        }

        [Fact]
        public void Spelverdeling_Speler_CanBeSetAndRead()
        {
            // Arrange
            var spelverdeling = new Spelverdeling();
            var speler = new Speler
            {
                SpelerId = 20,
                Voornaam = "Jan",
                Naam = "Jansen"
            };

            // Act
            spelverdeling.Speler = speler;

            // Assert
            Assert.Equal(speler, spelverdeling.Speler);
            Assert.Equal(20, spelverdeling.Speler.SpelerId);
        }

        [Fact]
        public void Spelverdeling_Spel_CanBeNull()
        {
            var spelverdeling = new Spelverdeling { Spel = null };
            Assert.Null(spelverdeling.Spel);
        }

        [Fact]
        public void Spelverdeling_Speler_CanBeNull()
        {
            var spelverdeling = new Spelverdeling { Speler = null };
            Assert.Null(spelverdeling.Speler);
        }

        [Fact]
        public void Spelverdeling_SpelId_CanBeNull()
        {
            var spelverdeling = new Spelverdeling { SpelId = null };
            Assert.Null(spelverdeling.SpelId);
        }

        [Fact]
        public void Spelverdeling_SpelerId_CanBeNull()
        {
            var spelverdeling = new Spelverdeling { SpelerId = null };
            Assert.Null(spelverdeling.SpelerId);
        }
    }
}
