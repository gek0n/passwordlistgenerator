// Copyright © 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.

using System;
using PasswordListGenerator.Combinations;
using PasswordListGenerator.Substitutions;

namespace PasswordListGenerator
{
	public static class VerbOptionFactory
	{
		private static readonly Logger Logger = new Logger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public static IVerbOption Construct(string verb, object instance)
		{
			if (instance == null) return null;
			switch (verb)
			{
				case "comb":
					Logger.Debug("Comb verb");
					var combOption = (CombineSubOption)instance;
					return new Combine(combOption);

				case "subs":
					Logger.Debug("Subs verb creating...");
					var subsOption = (SubstituteSubOption)instance;
					return new Substitution(subsOption);

				default:
					Logger.Debug("Unknown verb");
					throw new ArgumentException("It's not made yet");
			}
		}
	}
}