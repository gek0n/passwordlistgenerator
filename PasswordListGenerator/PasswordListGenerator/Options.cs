using CommandLine;

namespace PasswordListGenerator
{
	public class Options
	{
		[Option('k', "keywords-file", Required = true, HelpText = "File with keywords for generate passwords")]
		public string KeywordsFilename { get; set; }

		[Option("in-encoding", DefaultValue = "UTF-8", Required = false, HelpText = "Define input files encoding")]
		public string InFilesEncoding { get; set; }

		[Option("out-encoding", DefaultValue = "UTF-8", Required = false, HelpText = "Define output files encoding")]
		public string OutFilesEncoding { get; set; }

		[HelpOption]
		public string GetUsage()
		{
			return null;
		}
	}
}