﻿// Copyright © 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.

using CommandLine;

namespace PasswordListGenerator.Combinations
{
	public class CombineSubOption : EncodingSubOption
	{
		[Option('m', "max-length", Required = false, DefaultValue = (ushort)2, HelpText = "Number of words in one combination (between 2 and 10)")]
		public ushort MaxLength { get; set; }

		[Option("delimiter", Required = false, DefaultValue = " ", HelpText = "Set delimiter for words in combination")]
		public string Delimiter { get; set; }

		[Option("suffix", Required = false, HelpText = "Set suffix appended to every combination")]
		public string Suffix { get; set; }

		[Option("prefix", Required = false, HelpText = "Set prefix appended to every combination")]
		public string Prefix { get; set; }

		[Option('r', "repetition", Required = false, HelpText = "Set if every word can appear repeatedly")]
		public bool IsRepetition { get; set; }

		[Option('i', "in-file", Required = true, HelpText = "File with list of keywords to make combination")]
		public string InFilename { get; set; }

		[Option('o', "out-file", Required = false, HelpText = "File to write output combinations")]
		public string OutFilename { get; set; }

		[Option('v', "verbose", Required = false, HelpText = "Show detailed combination module configuration")]
		public bool IsVerbose { get; set; }

		[ParserState]
		public IParserState LastParserState { get; set; }
	}
}
