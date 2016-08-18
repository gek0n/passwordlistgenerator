using System;
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
					throw new ArgumentException("It's not made yet");

				case "subs":
					Logger.Debug("Subs verb creating...");
					var subsOptions = (SubstituteSubOptions)instance;
                    return new Substitution(subsOptions);

				default:
					Logger.Debug("Unknown verb");
					throw new ArgumentException("It's not made yet");
			}
		}
	}
}