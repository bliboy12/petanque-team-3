using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petanque.Models.Exceptions
{
	public class AttendanceModelException : Exception
	{
		public AttendanceModelException()
		{
		}

		public AttendanceModelException(string? message) : base(message)
		{
		}

		public AttendanceModelException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
