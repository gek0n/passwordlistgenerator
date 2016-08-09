using System;

namespace PasswordListGenerator
{
	public static class VerbOptionFactory
	{
		private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public static IVerbOption Construct(string verb, object instance)
		{
			switch (verb)
			{
				case "comb":
					Logger.Debug("Comb verb");
					throw new ArgumentException("It's not made yet");

				case "subs":
					Logger.Debug("Subs verb");
					var subsOptions = (SubstituteSubOptions)instance;

					return new Substitution(subsOptions);

				default:
					Logger.Debug("Unknown verb");
					throw new ArgumentException("It's not made yet");
			}
		}
	}
}