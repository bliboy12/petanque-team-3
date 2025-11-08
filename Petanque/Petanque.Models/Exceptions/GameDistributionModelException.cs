using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Models.Exceptions
{
	public class GameDistributionModelException : Exception
	{
		public GameDistributionModelException(string? message) : base(message)
		{
		}

		public GameDistributionModelException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
