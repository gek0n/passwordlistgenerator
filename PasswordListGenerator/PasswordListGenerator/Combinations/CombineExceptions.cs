// Copyright © 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.

using System;

namespace PasswordListGenerator.Combinations
{
	public class CombineExceptions : VerbOptionException
	{
		public CombineExceptions(string msg) : base(msg)
		{ }

		public CombineExceptions(string msg, Exception innerException) : base(msg, innerException)
		{ }
	}
}
