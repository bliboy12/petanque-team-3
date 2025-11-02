using Petanque.Models;
using Petanque.Models.Exceptions;
using Xunit;

namespace Petanque.Models.Tests
{
    public class MatchDayTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Test_Id_Valid(int id)
        {
            MatchDayModel m = new MatchDayModel();

            m.Id = id;

            Assert.Equal(id, m.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void Test_Id_Invalid(int id)
        {
            MatchDayModel m = new MatchDayModel();

            Assert.Throws<MatchDayModelException>(() => m.Id = id);
        }

        [Fact]
        public void Test_Date_Valid()
        {
            MatchDayModel m = new MatchDayModel();
            DateTime date = new DateTime(2025, 11, 1);

            m.Date = date;

            Assert.Equal(date, m.Date);
        }

        [Fact]
        public void Test_Date_Invalid()
        {
            MatchDayModel m = new MatchDayModel();

            Assert.Throws<MatchDayModelException>(() => m.Date = default);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(null)]
        public void Test_SeasonId_Valid(int? seasonId)
        {
            MatchDayModel m = new MatchDayModel();

            m.SeasonId = seasonId;

            Assert.Equal(seasonId, m.SeasonId);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        public void Test_SeasonId_Invalid(int? seasonId)
        {
            MatchDayModel m = new MatchDayModel();

            Assert.Throws<MatchDayModelException>(() => m.SeasonId = seasonId);
        }

        [Fact]
        public void Test_Can_Make_A_Season()
        {
            MatchDayModel m = new MatchDayModel();
            SeasonModel s = new SeasonModel();

            m.Season = s;

            Assert.Same(s, m.Season);
        }

        [Fact]
        public void Test_Season_Can_Be_Null()
        {
            MatchDayModel m = new MatchDayModel();

            m.Season = null;

            Assert.Null(m.Season);
        }

        [Fact]
        public void Test_Can_Add_Game()
        {
            MatchDayModel m = new MatchDayModel();
            GameModel g = new GameModel { Id = 1, Terrain = "A1" };

            m.Games.Add(g);

            Assert.Contains(g, m.Games);
        }

        [Fact]
        public void Test_Games_List_Initialized()
        {
            MatchDayModel m = new MatchDayModel();

            Assert.NotNull(m.Games);
            Assert.Empty(m.Games);
        }
    }
}
