using Petanque.Models;
using Petanque.Models.Exceptions;
using Xunit;

namespace Petanque.Models.Tests
{
    public class GameTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Test_Id_Valid(int id)
        {
            GameModel g = new GameModel();

            g.Id = id;

            Assert.Equal(id, g.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void Test_Id_Invalid(int id)
        {
            GameModel g = new GameModel();

            Assert.Throws<GameModelException>(() => g.Id = id);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(null)]
        public void Test_MatchDayId_Valid(int? matchDayId)
        {
            GameModel g = new GameModel();

            g.MatchDayId = matchDayId;

            Assert.Equal(matchDayId, g.MatchDayId);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        public void Test_MatchDayId_Invalid(int? matchDayId)
        {
            GameModel g = new GameModel();

            Assert.Throws<GameModelException>(() => g.MatchDayId = matchDayId);
        }

        [Theory]
        [InlineData("Terrain 1")]
        [InlineData("A1")]
        [InlineData("Center Court")]
        public void Test_Terrain_Valid(string terrain)
        {
            GameModel g = new GameModel();

            g.Terrain = terrain;

            Assert.Equal(terrain.Trim(), g.Terrain);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_Terrain_Invalid(string terrain)
        {
            GameModel g = new GameModel();

            Assert.Throws<GameModelException>(() => g.Terrain = terrain);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(13)]
        public void Test_ScoreA_Valid(int scoreA)
        {
            GameModel g = new GameModel();

            g.ScoreA = scoreA;

            Assert.Equal(scoreA, g.ScoreA);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(14)]
        [InlineData(20)]
        public void Test_ScoreA_Invalid(int scoreA)
        {
            GameModel g = new GameModel();

            Assert.Throws<GameModelException>(() => g.ScoreA = scoreA);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(8)]
        [InlineData(13)]
        public void Test_ScoreB_Valid(int scoreB)
        {
            GameModel g = new GameModel();

            g.ScoreB = scoreB;

            Assert.Equal(scoreB, g.ScoreB);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(14)]
        [InlineData(30)]
        public void Test_ScoreB_Invalid(int scoreB)
        {
            GameModel g = new GameModel();

            Assert.Throws<GameModelException>(() => g.ScoreB = scoreB);
        }

        [Fact]
        public void Test_Can_Make_A_MatchDay()
        {
            GameModel g = new GameModel();
            MatchDayModel m = new MatchDayModel { Id = 5 };

            g.MatchDay = m;

            Assert.Same(m, g.MatchDay);
        }

        [Fact]
        public void Test_MatchDay_Can_Be_Null()
        {
            GameModel g = new GameModel();

            g.MatchDay = null;

            Assert.Null(g.MatchDay);
        }

        [Fact]
        public void Test_Can_Add_GameDistribution()
        {
            GameModel g = new GameModel();
            GameDistributionModel dist = new GameDistributionModel { Id = 1, Team = "A", PlayerPosition = "Shooter", PlayerOrderNumber = 1 };

            g.GameDistributions.Add(dist);

            Assert.Contains(dist, g.GameDistributions);
        }

        [Fact]
        public void Test_GameDistributions_List_Initialized()
        {
            GameModel g = new GameModel();

            Assert.NotNull(g.GameDistributions);
            Assert.Empty(g.GameDistributions);
        }
    }
}
