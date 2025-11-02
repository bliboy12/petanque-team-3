using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine.TesthostProtocol;
using Petanque.Models;
using Petanque.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTModel
{
	public class DailyRankingTests
	{
		[Theory]
		[InlineData(1)]
		[InlineData(10)]
		public void Test_Id_Valid(int id)
		{
			DailyRankingModel d = new DailyRankingModel();
			d.Id = id;
			Assert.Equal(id, d.Id);
		}
		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(-10)]
		public void Test_Id_Invalid(int id)
		{
			DailyRankingModel d = new DailyRankingModel();

			Assert.Throws<DailyRankingModelException>(() => d.Id = id);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(10)]
		[InlineData(null)]
		public void Test_MatchDayId_Valid(int? matchDayId)
		{
			DailyRankingModel d = new DailyRankingModel();
			d.MatchDayId = matchDayId;
			Assert.Equal(matchDayId, d.MatchDayId);
		}
		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(-10)]
		public void Test_MatchDayId_Invalid(int? matchDayId)
		{
			DailyRankingModel d = new DailyRankingModel();

			Assert.Throws<DailyRankingModelException>(() => d.MatchDayId = matchDayId);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(10)]
		public void Test_PlayerId_Valid(int? playerId)
		{
			DailyRankingModel d = new DailyRankingModel();
			d.PlayerId = playerId;
			Assert.Equal(playerId, d.PlayerId);
		}
		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(-10)]
		public void Test_PlayerId_Invalid(int? playerId)
		{
			DailyRankingModel d = new DailyRankingModel();

			Assert.Throws<DailyRankingModelException>(() => d.PlayerId = playerId);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(13)] // Als ik goed had begrijpen is dit de max die een team kan behalen
		public void Test_MainPoints_Valid(int mainpoints)
		{
			DailyRankingModel d = new DailyRankingModel();
			d.MainPoints = mainpoints;
			Assert.Equal(mainpoints, d.MainPoints);
		}
		[Theory]
		[InlineData(-1)]
		[InlineData(-10)]
		public void Test_Mainpoints_Invalid(int mainpoints)
		{
			DailyRankingModel d = new DailyRankingModel();

			Assert.Throws<DailyRankingModelException>(() => d.MainPoints = mainpoints);
		}
		[Theory]
		[InlineData(-100)] // Minimum heeft invloed op hvl wedstrijden er zijn
		[InlineData(100)] // Maximum heeft invloed op hvl wedstrijden er zijn
		[InlineData(0)]
		public void Test_PlusMinPoints_Valid(int plusMinPoints)
		{
			DailyRankingModel d = new DailyRankingModel();
			d.PlusMinPoints = plusMinPoints;
			Assert.Equal(plusMinPoints, d.PlusMinPoints);
		}

		// Hoe test je invalid voor Test_PlusMinPunten_Invalid?
		//[Fact]
		//public void Test_PlusMinPunten_Invalid()
		//{
		//	Dagklassement d = new Dagklassement();
		//	d.PlusMinPunten = null;

		//}

		[Fact]
		public void Test_Can_Make_A_Player()
		{
			DailyRankingModel drm = new DailyRankingModel();
			PlayerModel s = new PlayerModel { Firstname = "Bob", Lastname = "Turner", Id = 5 };

			drm.Player = s;
			Assert.Same(s, drm.Player);
		}
		[Fact]
		public void Test_Can_Make_A_MatchDay()
		{
			DailyRankingModel drm = new DailyRankingModel();
			MatchDayModel s = new MatchDayModel { Id = 5 };

			drm.MatchDay = s;
			Assert.Same(s, drm.MatchDay);
		}
		[Fact]
		public void Test_Matchday_Can_Be_Null()
		{
			DailyRankingModel drm = new DailyRankingModel();
			drm.MatchDay = null;
			Assert.Null(drm.MatchDay);
		}
		[Fact]
		public void Test_Player_Can_Be_Null()
		{
			DailyRankingModel drm = new DailyRankingModel();
			drm.Player = null;
			Assert.Null(drm.Player);
		}
	}
}
