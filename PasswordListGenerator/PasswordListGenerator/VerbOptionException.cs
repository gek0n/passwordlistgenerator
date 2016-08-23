// Copyright © 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.
using System;

namespace PasswordListGenerator
{
	public class VerbOptionException : Exception
	{
		public VerbOptionException()
		{ }

		public VerbOptionException(string message) : base(message)
		{ }

		public VerbOptionException(string message, Exception innerException) : base(message, innerException)
		{ }
	}
}
