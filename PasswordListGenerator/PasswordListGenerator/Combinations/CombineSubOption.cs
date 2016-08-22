using CommandLine;

namespace PasswordListGenerator.Combinations
{
	public class CombineSubOption : EncodingSubOption
	{
		[Option("max-length", Required = false, HelpText = "Number of words in one combination (between 2 and 20)")]
		public ushort MaxLength { get; set; }

		[Option('i', "in-file", Required = true, HelpText = "File with list of keywords to make combination")]
		public string KeywordFilename { get; set; }

		[Option('o', "out-file", Required = false, HelpText = "File to write output combinations")]
		public string CombinationFilename { get; set; }
	}
}
