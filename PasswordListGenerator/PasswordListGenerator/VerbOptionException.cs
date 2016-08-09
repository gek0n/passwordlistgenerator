using System;

namespace PasswordListGenerator
{
	public class VerbOptionException : Exception
	{
		public VerbOptionException(string message) : base(message)
		{ }

		public VerbOptionException(string message, Exception innerException) : base(message, innerException)
		{ }
	}
}
