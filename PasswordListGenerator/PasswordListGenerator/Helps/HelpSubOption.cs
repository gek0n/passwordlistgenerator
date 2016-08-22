using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace PasswordListGenerator.Helps
{
	public class HelpSubOption
	{
		[ValueOption(0)]
		public string Verb { get; set; }
		
		/*Disallow any other values*/
		[ValueList(typeof(List<string>), MaximumElements = 0)]
		public IList<string> UnboundValues { get; set; }

		[HelpOption]
		public string GetUsage()
		{
			return HelpText.AutoBuild(this);
		}
	}
}
