using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petanque.Models;
using Petanque.Models.Exceptions;
using Xunit;

namespace UTModel
{
	public class AttendanceTests
	{
		
		[Theory]
		[InlineData(1)]
		[InlineData(10)]
		public void Test_Id_Valid(int attendanceId)
		{
			AttendanceModel a = new AttendanceModel();

			a.Id = attendanceId;
			Assert.Equal(attendanceId, a.Id);

		}
		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(-10)]
		public void Test_Id_Invalid(int attendanceId)
		{
			AttendanceModel a = new AttendanceModel();

			Assert.Throws<AttendanceModelException>(() => a.Id = attendanceId);
		}
		[Theory]
		[InlineData(1)]
		[InlineData(10)]
		[InlineData(null)]
		public void Test_MatchDayId_Valid(int? matchDay)
		{
			AttendanceModel a = new AttendanceModel();

			a.MatchDayId = matchDay;
			Assert.Equal(matchDay, a.MatchDayId);
		}
		[Theory]
		[InlineData(-1)]
		[InlineData(-10)]
		public void Test_MatchDayId_Invalid(int? matchDay)
		{
			AttendanceModel a = new AttendanceModel();

			Assert.Throws<AttendanceModelException>(() => a.MatchDayId = matchDay);
		}
		[Theory]
		[InlineData(1)]
		[InlineData(10)]
		[InlineData(null)]
		public void Test_PlayerId_Valid(int? playerId)
		{
			AttendanceModel a = new AttendanceModel();

			a.PlayerId = playerId;
			Assert.Equal(playerId, a.PlayerId);
		}
		[Theory]
		[InlineData(-1)]
		[InlineData(-10)]
		public void Test_PlayerId_Invalid(int? playerId)
		{
			AttendanceModel aanwezigheid = new AttendanceModel();

			Assert.Throws<AttendanceModelException>(() => aanwezigheid.PlayerId = playerId);
		}
		[Theory]
		[InlineData(1)]
		[InlineData(10)] // limiteren aan aantal spelers per team
		public void Test_PlayerNumber_Valid(int playernumber)
		{
			AttendanceModel a = new AttendanceModel();
			a.PlayerNumber = playernumber;

			Assert.Equal(playernumber, a.PlayerNumber);
		}
		[Theory]
		[InlineData(0)] // zou nul toegelaten mogen zijn als volgnr?
		[InlineData(-1)]
		[InlineData(-10)]
		public void Test_PlayerNumber_Invalid(int playernumber)
		{
			AttendanceModel a = new AttendanceModel();

			Assert.Throws<AttendanceModelException>(() => a.PlayerNumber = playernumber);
		}
		// Dit kan niet echt checken als Speler geen constructor heeft
		//[Fact]
		//public void Test_SpelerValid_Assigned()
		//{
		//	speler s = new Speler(1, "bob", "marley");
		//	Assert.Equal(speler.SpelerId, s.SpelerId);
		//}
		[Fact]
		public void Test_Can_Make_A_Player()
		{
			AttendanceModel a = new AttendanceModel();
			PlayerModel s = new PlayerModel { Firstname = "Bob", Lastname = "Turner", Id = 5 };

			a.Player = s;
			Assert.Same(s, a.Player);
		}
		[Fact]
		public void Test_Can_Make_A_MatchDay()
		{
			AttendanceModel a = new AttendanceModel();
			MatchDayModel s = new MatchDayModel { Id = 5 };

			a.MatchDay = s;
			Assert.Same(s, a.MatchDay);
		}
		[Fact]
		public void Test_MatchDay_Can_Be_Null()
		{
			AttendanceModel a = new AttendanceModel();
			a.MatchDay = null;
			Assert.Null(a.MatchDay);
		}
		[Fact]
		public void Test_Player_Can_Be_Null()
		{
			AttendanceModel a = new AttendanceModel();
			a.Player = null;
			Assert.Null(a.Player);
		}
	}
}
