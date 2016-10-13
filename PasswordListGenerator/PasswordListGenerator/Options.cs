// Copyright © 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.

using System.Linq;
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
            StringBuilder helpStringBuilder;
			switch (verb)
			{
				case "subs":
					helpStringBuilder = new StringBuilder(HelpText.AutoBuild(SubstituteVerb, current => HelpText.DefaultParsingErrorsHandler(SubstituteVerb, current)));
					helpStringBuilder.AppendLine();
					helpStringBuilder.AppendLine(Resources.additionalUsage);
					helpStringBuilder.Append(Resources.AdditionalSubsDictUsage);
					helpStringBuilder.Append(Resources.AdditionalSubsMethodUsage);
					helpStringBuilder.Append(Resources.AdditionalSubs_i_Usages);
					if (SubstituteVerb.LastParserState?.Errors.Any() == true)
					{
						helpStringBuilder.Append("\r\n\r\nSOMETHING BAD!!!");
					}
					return helpStringBuilder.ToString();
				case "comb":
					helpStringBuilder = new StringBuilder(HelpText.AutoBuild(CombineVerb, current => HelpText.DefaultParsingErrorsHandler(CombineVerb, current)));
					helpStringBuilder.AppendLine();
					helpStringBuilder.AppendLine(Resources.additionalUsage);
			        helpStringBuilder.Append(Resources.AdditionalComb_m_Usage);
                    helpStringBuilder.Append(Resources.AdditionalComb_delimiter_Usage);
                    helpStringBuilder.Append(Resources.AdditionalComb_prefix_Usage);
                    helpStringBuilder.Append(Resources.AdditionalComb_suffix_Usage);
                    helpStringBuilder.Append(Resources.AdditionalComb_r_Usage);
                    if (CombineVerb.LastParserState?.Errors.Any() == true)
                    {
                        // Случается, когда не указаны обязательные параметры!
                        helpStringBuilder.Append("\r\n\r\nSOMETHING BAD!!!");
                    }
                    return helpStringBuilder.ToString();
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