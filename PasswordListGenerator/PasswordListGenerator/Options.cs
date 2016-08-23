// Copyright © 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.

using CommandLine;
using CommandLine.Text;
using PasswordListGenerator.Substitutions;

namespace PasswordListGenerator
{
	public class Options
	{
		[VerbOption("subs", HelpText = "Substitute all symbols in word with leet letters")]
		public SubstituteSubOption SubstituteVerb { get; set; }

		[HelpVerbOption]
		public string GetUsage(string verb)
		{
			return HelpText.AutoBuild(this, verb);
		}
	}
    
	public abstract class EncodingSubOption
	{
		[Option("in-encoding", DefaultValue = "UTF-8", Required = false, HelpText = "Define input files encoding")]
		public string InEncoding { get; set; }

		[Option("out-encoding", DefaultValue = "UTF-8", Required = false, HelpText = "Define output files encoding")]
		public string OutEncoding { get; set; }
	}
}