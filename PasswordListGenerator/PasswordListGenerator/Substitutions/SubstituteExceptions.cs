// Copyright © 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.

using System;
using CommandLine.Text;

namespace PasswordListGenerator.Substitutions
{
	public class NotInTheDictionarySubstituteException : VerbOptionException
	{
		public NotInTheDictionarySubstituteException(string msg) : base(msg)
		{ }

		public NotInTheDictionarySubstituteException(string msg, Exception innerException) : base(msg, innerException)
		{ }
	}

	public class ParseJsonSubstituteException : SubstituteException
	{
		public ParseJsonSubstituteException(string msg) : base(msg)
		{ }

		public ParseJsonSubstituteException(string msg, Exception innerException) : base(msg, innerException)
		{ }
	}

	public class ValidateJsonSubstituteException : SubstituteException
	{
		public ValidateJsonSubstituteException(string msg) : base(msg)
		{ }

		public ValidateJsonSubstituteException(string msg, Exception innerException) : base(msg, innerException)
		{ }
	}

	public class SourceWordSubstituteException : SubstituteException
	{
		public SourceWordSubstituteException(string msg) : base(msg)
		{ }

		public SourceWordSubstituteException(string msg, Exception innerException) : base(msg, innerException)
		{ }
	}

	public class DeserializeSubstituteException : SubstituteException
	{
		public DeserializeSubstituteException(string msg) : base(msg)
		{ }

		public DeserializeSubstituteException(string msg, Exception innerException) : base(msg, innerException)
		{ }
	}

	public class IOSubstituteException : SubstituteException
	{
		public IOSubstituteException(string msg) : base(msg)
		{ }

		public IOSubstituteException(string msg, Exception innerException) : base(msg, innerException)
		{ }
	}

	public class SubstituteException : VerbOptionException
	{
		public SubstituteException(string msg)
			: base(AddUsageToExceptionMessage(msg))
		{ }

		public SubstituteException(string msg, Exception innerException)
			: base(AddUsageToExceptionMessage(msg), innerException)
		{ }

		private static string AddUsageToExceptionMessage(string msg)
		{
			return msg + Environment.NewLine
						+ Environment.NewLine
						+ HelpText.AutoBuild(
							new SubstituteSubOption()
							, current => HelpText.DefaultParsingErrorsHandler(new SubstituteSubOption(), current));
		}
	}
}
