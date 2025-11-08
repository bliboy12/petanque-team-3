using Petanque.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Models.Tests
{
	public class PlayerTests
	{
		[Theory]
		[InlineData(1)]
		[InlineData(10)]
		public void Test_Id_Valid(int id)
		{
			PlayerModel p = new PlayerModel { Firstname = "bob", Lastname = "Marley"};

			p.Id = id;

			Assert.Equal(id, p.Id);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(-10)]
		public void Test_Id_Invalid(int id)
		{
			PlayerModel p = new PlayerModel { Firstname = "bob", Lastname = "Marley" };

			Assert.Throws<PlayerModelException>(() => p.Id = id);
		}

		[Theory]
		[InlineData("Bob")]
		[InlineData("Alice")]
		[InlineData("John")]
		public void Test_Firstname_Valid(string name)
		{
			PlayerModel p = new PlayerModel { Firstname = name, Lastname = "Marley" };

			Assert.Equal(name.Trim(), p.Firstname);
		}

		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData(null)]
		public void Test_Firstname_Invalid(string name)
		{
			PlayerModel p = new PlayerModel { Firstname = "bob", Lastname = "Marley"};

			Assert.Throws<PlayerModelException>(() => p.Firstname = name);
		}

		[Theory]
		[InlineData("Marley")]
		[InlineData("Turner")]
		[InlineData("Doe")]
		public void Test_Lastname_Valid(string name)
		{
			PlayerModel p = new PlayerModel { Firstname = "bob", Lastname = name};

			Assert.Equal(name.Trim(), p.Lastname);
		}

		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData(null)]
		public void Test_Lastname_Invalid(string name)
		{
			PlayerModel p = new PlayerModel { Firstname = "bob", Lastname = "Marley" };

			Assert.Throws<PlayerModelException>(() => p.Lastname = name);
		}

		[Fact]
		public void Test_Can_Add_Attendance()
		{
			PlayerModel p = new PlayerModel { Firstname = "bob", Lastname = "Marley"};
			AttendanceModel a = new AttendanceModel { Id = 1, PlayerNumber = 1 };

			p.Attendances.Add(a);

			Assert.Contains(a, p.Attendances);
		}

		[Fact]
		public void Test_Can_Add_DailyRanking()
		{
			PlayerModel p = new PlayerModel { Firstname = "bob", Lastname = "Marley" };
			DailyRankingModel r = new DailyRankingModel { Id = 1, MainPoints = 10 };

			p.DailyRankings.Add(r);

			Assert.Contains(r, p.DailyRankings);
		}

		[Fact]
		public void Test_Attendances_List_Initialized()
		{
			PlayerModel p = new PlayerModel { Firstname = "bob", Lastname = "Marley" };

			Assert.NotNull(p.Attendances);
			Assert.Empty(p.Attendances);
		}

		[Fact]
		public void Test_DailyRankings_List_Initialized()
		{
			PlayerModel p = new PlayerModel { Firstname = "bob", Lastname = "Marley" };

			Assert.NotNull(p.DailyRankings);
			Assert.Empty(p.DailyRankings);
		}
	}
}
