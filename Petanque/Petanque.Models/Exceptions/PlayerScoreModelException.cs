using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Models.Exceptions
{
	public class PlayerScoreModelException : Exception
	{
		public PlayerScoreModelException()
		{
		}

		public PlayerScoreModelException(string? message) : base(message)
		{
		}

		public PlayerScoreModelException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
