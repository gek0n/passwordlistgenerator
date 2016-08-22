using System;

namespace PasswordListGenerator.Helps
{
	public class Help : IVerbOption
	{
		private readonly string _verb;
		private readonly string _usage;
		private HelpSubOption s;

		public Help(HelpSubOption subOption)
		{
			_verb = subOption.Verb;
			_usage = subOption.GetUsage();
			s = subOption;
		}

		public void Process()
		{
			if (string.IsNullOrEmpty(_verb))
			{
				Console.WriteLine(_usage);
			}
			else
			{
				//Здесь надо конструировать Additional help
			}
		}
	}
}
