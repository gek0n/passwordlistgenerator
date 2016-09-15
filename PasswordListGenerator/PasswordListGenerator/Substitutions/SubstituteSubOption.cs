// Copyright © 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.

using CommandLine;

namespace PasswordListGenerator.Substitutions
{
	public class SubstituteSubOption : EncodingSubOption
	{
		[ValueOption(0)]
		public string SourceWord { get; set; }

		[Option('d', "dict", Required = false, HelpText = "Dict to subs symbols (Must contain array of methods with arrays for every symbol)")]
		public string DictFilename { get; set; }

		[Option('m', "method", Required = false, HelpText = "Method from dictionary to substitute letters in word")]
		public string Method { get; set; }

		[Option("ignore-case", Required = false, HelpText = "Ignore letter case in source word")]
		public bool IsIgnoreCase { get; set; }

		[Option('o', "out-file", Required = false, HelpText = "File to write output substitutions")]
		public string OutFilename { get; set; }

		[Option('i', Required = false, HelpText = "Use std input to get words for substitution")]
		public bool IsUseStdInput { get; set; }

		[Option('v', "verbose", Required = false, HelpText = "Show detailed substitute module configuration")]
		public bool IsVerbose { get; set; }
	}
}
