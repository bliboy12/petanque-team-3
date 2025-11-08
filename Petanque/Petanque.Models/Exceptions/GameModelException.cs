using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Models.Exceptions
{
	public class GameModelException : Exception
	{
		public GameModelException(string? message) : base(message)
		{
		}

		public GameModelException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
