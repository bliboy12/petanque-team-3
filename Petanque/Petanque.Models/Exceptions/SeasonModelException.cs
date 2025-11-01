using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Models.Exceptions
{
	public class SeasonModelException : Exception
	{
		public SeasonModelException()
		{
		}

		public SeasonModelException(string? message) : base(message)
		{
		}

		public SeasonModelException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
