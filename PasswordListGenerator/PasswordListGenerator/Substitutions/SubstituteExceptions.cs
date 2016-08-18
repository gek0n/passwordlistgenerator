using System;

namespace PasswordListGenerator.Substitutions
{
	public class NotInTheDictionarySubstituteException : VerbOptionException
	{
		public NotInTheDictionarySubstituteException(string msg) : base(msg)
		{ }

		public NotInTheDictionarySubstituteException(string msg, Exception innerException) : base(msg, innerException)
		{ }
	}

	public class ParseJsonSubstituteException : VerbOptionException
	{
		public ParseJsonSubstituteException(string msg) : base(msg)
		{ }

		public ParseJsonSubstituteException(string msg, Exception innerException) : base(msg, innerException)
		{ }
	}

	public class ValidationJsonSubstituteException : VerbOptionException
	{
		public ValidationJsonSubstituteException(string msg) : base(msg)
		{ }

		public ValidationJsonSubstituteException(string msg, Exception innerException) : base(msg, innerException)
		{ }
	}

	public class SourceWordSubstituteException : VerbOptionException
	{
		public SourceWordSubstituteException(string msg) : base(msg)
		{ }

		public SourceWordSubstituteException(string msg, Exception innerException) : base(msg, innerException)
		{ }
	}

	public class DeserializeSubstituteException : VerbOptionException
	{
		public DeserializeSubstituteException(string msg) : base(msg)
		{ }

		public DeserializeSubstituteException(string msg, Exception innerException) : base(msg, innerException)
		{ }
	}

	public class IOSubstituteException : VerbOptionException
	{
		public IOSubstituteException(string msg) : base(msg)
		{ }

		public IOSubstituteException(string msg, Exception innerException) : base(msg, innerException)
		{ }
	}
}
