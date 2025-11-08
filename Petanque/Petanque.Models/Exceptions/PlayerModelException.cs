using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Models.Exceptions
{
	public class PlayerModelException : Exception
	{
		public PlayerModelException(string? message) : base(message)
		{
		}

		public PlayerModelException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
