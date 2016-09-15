using CommandLine;

namespace PasswordListGenerator.Combinations
{
	public class CombineSubOption : EncodingSubOption
	{
		[Option("max-length", Required = false, DefaultValue = (ushort)2, HelpText = "Number of words in one combination (between 2 and 20)")]
		public ushort MaxLength { get; set; }

		[Option("delimiter", Required = false, HelpText = "Set delimiter for words in combination")]
		public string Delimiter { get; set; }

		[Option('r', "repetition", Required = false, HelpText = "Set if every word can appear repeatedly")]
		public bool IsRepetition { get; set; }

		[Option('i', "in-file", Required = true, HelpText = "File with list of keywords to make combination")]
		public string KeywordFilename { get; set; }

		[Option('o', "out-file", Required = false, HelpText = "File to write output combinations")]
		public string CombinationFilename { get; set; }
	}
}
