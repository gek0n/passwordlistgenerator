using CommandLine;
using CommandLine.Text;

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

	public class SubstituteSubOptions : EncodingSubOptions
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