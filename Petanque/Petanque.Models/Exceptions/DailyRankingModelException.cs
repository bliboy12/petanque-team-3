using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Models.Exceptions
{
	public class DailyRankingModelException : Exception
	{
		public DailyRankingModelException()
		{
		}

		public DailyRankingModelException(string? message) : base(message)
		{
		}

		public DailyRankingModelException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
