// Copyright © 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.

using System;

namespace PasswordListGenerator.Combinations
{
	public class CombineException : VerbOptionException
	{
		public CombineException(string msg) : base(msg)
		{ }

		public CombineException(string msg, Exception innerException) : base(msg, innerException)
		{ }
	}
	public class FilenameInvalidCombineException : VerbOptionException
	{
		public FilenameInvalidCombineException(string msg) : base(msg)
		{ }

		public FilenameInvalidCombineException(string msg, Exception innerException) : base(msg, innerException)
		{ }
	}

	public class MaxLengthNotInRangeCombineException : VerbOptionException
	{
		public MaxLengthNotInRangeCombineException(string msg) : base(msg)
		{ }

		public MaxLengthNotInRangeCombineException(string msg, Exception innerException) : base(msg, innerException)
		{ }
	}
}
