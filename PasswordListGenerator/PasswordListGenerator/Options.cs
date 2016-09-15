// Copyright © 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.

using System.Text;
using CommandLine;
using CommandLine.Text;
using PasswordListGenerator.Combinations;
using PasswordListGenerator.Properties;
using PasswordListGenerator.Substitutions;

namespace PasswordListGenerator
{
	public class Options
	{
		[VerbOption("comb", HelpText = "Combine list of words in list of combinations")]
		public CombineSubOption CombineVerb { get; set; }

		[VerbOption("subs", HelpText = "Substitute all symbols in word with leet letters")]
		public SubstituteSubOption SubstituteVerb { get; set; }

		[HelpVerbOption]
		public string GetUsage(string verb)
		{
			switch (verb)
			{
				case "subs":
					var helpStringBuilder = new StringBuilder(HelpText.AutoBuild(SubstituteVerb, current => HelpText.DefaultParsingErrorsHandler(SubstituteVerb, current)));
					helpStringBuilder.AppendLine();
					helpStringBuilder.AppendLine(Resources.additionalUsage);
					helpStringBuilder.Append(Resources.AdditionalSubsDictUsage);
					helpStringBuilder.Append(Resources.AdditionalSubsMethodUsage);
					helpStringBuilder.Append(Resources.AdditionalSubs_i_Usages);
					return helpStringBuilder.ToString();
				case "comb":
					return string.Empty;
				default:
					return HelpText.AutoBuild(this, verb);
			}
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