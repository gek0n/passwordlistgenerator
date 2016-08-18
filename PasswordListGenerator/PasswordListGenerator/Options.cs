// Copyright © 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.

using CommandLine;
using CommandLine.Text;
using PasswordListGenerator.Substitutions;

namespace PasswordListGenerator
{
	public class Options
	{
		[VerbOption("comb", HelpText = "Combine list of words in list of combinations")]
		public CombineSubOptions CombineVerb { get; set; }

		[VerbOption("subs", HelpText = "Substitute all symbols in word with leet letters")]
		public SubstituteSubOptions SubstituteVerb { get; set; }

		[HelpVerbOption]
		public string GetUsage(string verb)
		{
			return HelpText.AutoBuild(this, verb);
		}
	}
    
	public class CombineSubOptions : EncodingSubOptions
	{
		[Option("max-length", Required = false, HelpText = "Number of words in one combination (between 2 and 20)")]
		public ushort MaxLength { get; set; }

		[Option('i', "in-file", Required = true, HelpText = "File with list of keywords to make combination")]
		public string KeywordFilename { get; set; }

		[Option('o', "out-file", Required = false, HelpText = "File to write output combinations")]
		public string CombinationFilename { get; set; }
	}

	public abstract class EncodingSubOptions
	{
		[Option("in-encoding", DefaultValue = "UTF-8", Required = false, HelpText = "Define input files encoding")]
		public string InEncoding { get; set; }

		[Option("out-encoding", DefaultValue = "UTF-8", Required = false, HelpText = "Define output files encoding")]
		public string OutEncoding { get; set; }
	}
}