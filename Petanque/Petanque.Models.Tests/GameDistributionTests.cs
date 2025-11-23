using Petanque.Contracts.Responses;
using Petanque.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Models.Tests
{
	public class GameDistributionTests
	{
		[Theory]
		[InlineData(1)]
		[InlineData(10)]
		public void Test_Id_Valid(int id)
		{
			GameDistributionModel g = new GameDistributionModel();

			g.Id = id;

			Assert.Equal(id, g.Id);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(-10)]
		public void Test_Id_Invalid(int id)
		{
			GameDistributionModel g = new GameDistributionModel();

			Assert.Throws<GameDistributionModelException>(() => g.Id = id);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(10)]
		[InlineData(null)]
		public void Test_GameId_Valid(int? gameId)
		{
			GameDistributionModel g = new GameDistributionModel();

			g.GameId = gameId;

			Assert.Equal(gameId, g.GameId);
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(-10)]
		public void Test_GameId_Invalid(int? gameId)
		{
			GameDistributionModel g = new GameDistributionModel();

			Assert.Throws<GameDistributionModelException>(() => g.GameId = gameId);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(10)]
		[InlineData(null)]
		public void Test_PlayerId_Valid(int? playerId)
		{
			GameDistributionModel g = new GameDistributionModel();

			g.PlayerId = playerId;

			Assert.Equal(playerId, g.PlayerId);
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(-10)]
		public void Test_PlayerId_Invalid(int? playerId)
		{
			GameDistributionModel g = new GameDistributionModel();

			Assert.Throws<GameDistributionModelException>(() => g.PlayerId = playerId);
		}

		[Theory]
		[InlineData("A")]
		[InlineData("B")]
		[InlineData("TeamX")]
		public void Test_Team_Valid(string team)
		{
			GameDistributionModel g = new GameDistributionModel();

			g.Team = team;

			Assert.Equal(team.Trim(), g.Team);
		}

		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData(null)]
		public void Test_Team_Invalid(string team)
		{
			GameDistributionModel g = new GameDistributionModel();

			Assert.Throws<GameDistributionModelException>(() => g.Team = team);
		}

		[Theory]
		[InlineData("Shooter")]
		[InlineData("Lead")]
		[InlineData("Middle")]
		public void Test_PlayerPosition_Valid(string position)
		{
			GameDistributionModel g = new GameDistributionModel();

			g.PlayerPosition = position;

			Assert.Equal(position.Trim(), g.PlayerPosition);
		}

		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData(null)]
		public void Test_PlayerPosition_Invalid(string position)
		{
			GameDistributionModel g = new GameDistributionModel();

			Assert.Throws<GameDistributionModelException>(() => g.PlayerPosition = position);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public void Test_PlayerOrderNumber_Valid(int order)
		{
			GameDistributionModel g = new GameDistributionModel();

			g.PlayerOrderNumber = order;

			Assert.Equal(order, g.PlayerOrderNumber);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(-10)]
		public void Test_PlayerOrderNumber_Invalid(int order)
		{
			GameDistributionModel g = new GameDistributionModel();

			Assert.Throws<GameDistributionModelException>(() => g.PlayerOrderNumber = order);
		}

		[Fact]
		public void Test_Can_Make_A_Player()
		{
			GameDistributionModel g = new GameDistributionModel();
			PlayerModel p = new PlayerModel { Id = 3, Firstname = "Alice", Lastname = "Doe" };

			g.Player = p;

			Assert.Same(p, g.Player);
		}

		[Fact]
		public void Test_Can_Make_A_Game()
		{
			GameDistributionModel g = new GameDistributionModel();
			GameModel game = new GameModel { Id = 7 };

			g.Game = game;

			Assert.Same(game, g.Game);
		}

		[Fact]
		public void Test_Player_Can_Be_Null()
		{
			GameDistributionModel g = new GameDistributionModel();

			g.Player = null;

			Assert.Null(g.Player);
		}

		[Fact]
		public void Test_Game_Can_Be_Null()
		{
			GameDistributionModel g = new GameDistributionModel();

			g.Game = null;

			Assert.Null(g.Game);
		}

        [Fact]
        public void Test_GameDistribution_Has_3Noob_Players_2Expert_Players() {

			var matchDayId = 1;
			var playerPresences = new List<AanwezigheidResponseContract>();
			// SpelverdelingService.MaakVerdeling aanroepen (IEnumerable<AanwezigheidResponseContract> aanwezigheden, int speeldagId)
			// Controleren of per team het aantal expert en noob spelers correct zijn
        }
    }
}
