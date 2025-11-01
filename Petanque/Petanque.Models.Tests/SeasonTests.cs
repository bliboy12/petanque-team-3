using Petanque.Models;
using Petanque.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTModel
{
	public class SeasonTests
	{

		[Theory]
		[InlineData(1)]
		[InlineData(10)]
		public void Test_Id_Valid(int id)
		{
			SeasonModel s = new SeasonModel();
			s.Id = id;
			Assert.Equal(id, s.Id);
		}
		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(-10)]
		public void Test_Id_Invalid(int id)
		{
			SeasonModel s = new SeasonModel();

			Assert.Throws<SeasonModelException>(() => s.Id = id);
		}
		[Theory]
		[InlineData("2020-05-02", "2020-06-02")]
		[InlineData("2020-01-01", "2020-01-31")]
		[InlineData("2020-01-01", "2020-01-01")]
		public void Test_Valid_StartDate_IsNotLater_Then_EndDatum(string begin, string end)
		{
			var s = new SeasonModel();
			var beginDate = DateOnly.Parse(begin);
			var endDate = DateOnly.Parse(end);

			s.StartDate = beginDate;
			s.EndDate = endDate;

			Assert.Equal(beginDate, s.StartDate);
			Assert.Equal(endDate, s.EndDate);
			Assert.True(s.StartDate <= s.EndDate);
		}
		[Theory]
		[InlineData("2020-07-02", "2020-06-02")]
		public void Test_Invalid_StartDate_IsLater_Then_EndDatum(string begin, string end)
		{
			DateOnly startDate = DateOnly.Parse(begin);
			DateOnly endDate = DateOnly.Parse(end);

			SeasonModel s = new SeasonModel();
			s.EndDate = endDate;

			Assert.Throws<SeasonModelException>(() => s.StartDate = startDate);
		}
		[Fact]
		public void Test_Valid_MatchDay_Adding()
		{
			MatchDayModel matchday = new MatchDayModel { Id = 1 };
			SeasonModel s = new SeasonModel();
			
			s.MatchDays.Add(matchday);
			Assert.Contains(matchday, s.MatchDays);
		}
		// Test is not very usefull
		//[Fact]
		//public void Test_Invalid_MatchDay_Adding()
		//{
		//	SeasonModel s = new SeasonModel();
		//	Assert.Throws<SeasonModelException>(() => s.MatchDays.Add(null!)); // null! wilt zeggen dat je de waarschuwing van 'het is null' onderdrukt en zegt tegen de compiler dat je het weet, maar maak je geen zorgen.
		//}
	}
}
