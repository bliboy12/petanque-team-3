using Petanque.Models;
using Petanque.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTModel
{
	public class PlayerScoreTests
	{
		[Theory]
		[InlineData(1)]
		[InlineData(10)]
		public void Test_Id_Valid(int attendanceId)
		{
			PlayerScoreModel a = new PlayerScoreModel();

			a.Id = attendanceId;
			Assert.Equal(attendanceId, a.Id);

		}
		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(-10)]
		public void Test_Id_Invalid(int attendanceId)
		{
			PlayerScoreModel a = new PlayerScoreModel();

			Assert.Throws<PlayerScoreModelException>(() => a.Id = attendanceId);
		}
		[Theory]
		[InlineData(1)]
		[InlineData(10)] // limiteren aan aantal spelers per team
		public void Test_PlayerNumber_Valid(int playernumber)
		{
			PlayerScoreModel a = new PlayerScoreModel();
			a.PlayerOrderNumber = playernumber;

			Assert.Equal(playernumber, a.PlayerOrderNumber);
		}
		[Theory]
		[InlineData(0)] // zou nul toegelaten mogen zijn als volgnr?
		[InlineData(-1)]
		[InlineData(-10)]
		public void Test_PlayerNumber_Invalid(int playerOrderNumber)
		{
			PlayerScoreModel a = new PlayerScoreModel();

			Assert.Throws<PlayerScoreModelException>(() => a.PlayerOrderNumber = playerOrderNumber);
		}
		[Theory]
		[InlineData(13)]
		[InlineData(1)]
		[InlineData(0)]
		public void Test_Valid_ScoreA(int scoreA)
		{
			PlayerScoreModel gm = new PlayerScoreModel();
			gm.ScoreA = scoreA;
			Assert.Equal(gm.ScoreA, scoreA);
		}

		[Theory]
		[InlineData(-14)]
		[InlineData(14)]
		[InlineData(25)]
		[InlineData(-25)]
		public void Test_Invalid_ScoreA(int scoreA)
		{
			PlayerScoreModel gm = new PlayerScoreModel();

			Assert.Throws<PlayerScoreModelException>(() => gm.ScoreA = scoreA);
		}

		[Theory]
		[InlineData(13)]
		[InlineData(-13)]
		[InlineData(10)]
		[InlineData(1)]
		[InlineData(0)]
		public void Test_Valid_ScoreB(int scoreB)
		{
			PlayerScoreModel gm = new PlayerScoreModel();
			gm.ScoreB = scoreB;
			Assert.Equal(gm.ScoreB, scoreB);
		}

		[Theory]
		[InlineData(-14)]
		[InlineData(14)]
		[InlineData(25)]
		[InlineData(-25)]
		public void Test_Invalid_ScoreB(int scoreB)
		{
			PlayerScoreModel gm = new PlayerScoreModel();

			Assert.Throws<PlayerScoreModelException>(() => gm.ScoreB = scoreB);
		}

	}
}
