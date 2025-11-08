using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Models.Exceptions
{
	public class MatchDayModelException : Exception
	{
		public MatchDayModelException(string? message) : base(message)
		{
		}

		public MatchDayModelException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
