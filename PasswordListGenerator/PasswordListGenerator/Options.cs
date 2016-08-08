// Copyright 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.

using System.Text;
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

		[Option('i', Required = false, HelpText = "Use std input to get words for substitution")]
		public bool IsUseStdInput { get; set; }

		[HelpOption]
		public string GetUsage()
		{
			var helpStringBuilder = new StringBuilder(HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current)));
			helpStringBuilder.AppendLine();
			helpStringBuilder.AppendLine("Additional information for options:");
			helpStringBuilder.AppendLine("-d, --dict");
			helpStringBuilder.AppendLine("		Use this option to specify file, that contains json data in folowing format:");
			helpStringBuilder.AppendLine("		{");
			helpStringBuilder.AppendLine("			\"method_1\": {");
			helpStringBuilder.AppendLine("				\"A\": [");
			helpStringBuilder.AppendLine("					\"a\",");
			helpStringBuilder.AppendLine("					\"/-\\\",");
			helpStringBuilder.AppendLine("				],");
			helpStringBuilder.AppendLine("				\"1\": [");
			helpStringBuilder.AppendLine("					\"|\",");
			helpStringBuilder.AppendLine("					\"i\",");
			helpStringBuilder.AppendLine("					\"I\"");
			helpStringBuilder.AppendLine("				]");
			helpStringBuilder.AppendLine("				...");
			helpStringBuilder.AppendLine("			},");
			helpStringBuilder.AppendLine("			\"method_2\": {");
			helpStringBuilder.AppendLine("				\"A\": [...],");
			helpStringBuilder.AppendLine("				...");
			helpStringBuilder.AppendLine("			},");
			helpStringBuilder.AppendLine("			...");
			helpStringBuilder.AppendLine("		}");
			helpStringBuilder.AppendLine();
			helpStringBuilder.AppendLine("		Now if you type: PasswordListGenerator subs -d dictFilename.json -m method_1 A");
			helpStringBuilder.AppendLine("		You will see folowing text in console:");
			helpStringBuilder.AppendLine("		A");
			helpStringBuilder.AppendLine("		a");
			helpStringBuilder.AppendLine("		/-\\");
			helpStringBuilder.AppendLine("		It's all possible substitutions for symbol \"A\" in method \"method_1\", specified in file");
			helpStringBuilder.AppendLine("		\"dictFilename.json\". If file does not specified, default file wii be used. Default file");
			helpStringBuilder.AppendLine("		contains 4 methods: GoodLeet, BadLeet, Cyrillic and Pronunciation. Each of this methods ");
			helpStringBuilder.AppendLine("		contains dictionary for latin alphabet.");
			helpStringBuilder.AppendLine();
			helpStringBuilder.AppendLine("-m, --method");
			helpStringBuilder.AppendLine("		Use this option to specify method, that containing dictionaty of symbols with all possible substitutions");
			helpStringBuilder.AppendLine("		for each. If you specify file with user dictionary, then you must use methods, written in this file");
			helpStringBuilder.AppendLine("		(see -d option). If method does not specified, default method will be used. First method in dictionary");
			helpStringBuilder.AppendLine("		will be used as default. In default file \"GoodLeet\" method is used by default.");
			helpStringBuilder.AppendLine();
			helpStringBuilder.AppendLine("-i");
			helpStringBuilder.AppendLine("		Use this option if you want to substitute many words typed form keyboard or loaded from input stream.");
			helpStringBuilder.AppendLine("		For example, if you type :PasswordListGenerator subs -i");
			helpStringBuilder.AppendLine("		You will see folowing:");
			helpStringBuilder.AppendLine("		Selected method is GoodLeet");
			helpStringBuilder.AppendLine("		");
			helpStringBuilder.AppendLine("		Now you can type any word or symbol and substitutions will appear in the console:");
			helpStringBuilder.AppendLine("		<Q> (typed)");
			helpStringBuilder.AppendLine("		Q");
			helpStringBuilder.AppendLine("		(,)");
			helpStringBuilder.AppendLine("		<B> (typed)");
			helpStringBuilder.AppendLine("		B");
			helpStringBuilder.AppendLine("		|3");
			helpStringBuilder.AppendLine("		8");
			helpStringBuilder.AppendLine("		<q> (typed)");
			helpStringBuilder.AppendLine("		[ERROR]: The symbol \"q\" is not in the dictionary. Please specify other dictionary or use");
			helpStringBuilder.AppendLine("		ignore-case option");
			helpStringBuilder.AppendLine();
			helpStringBuilder.AppendLine("		<Enter> (pressed)");
			helpStringBuilder.AppendLine("		If you press <Enter> without type any symbols, application session will be interrupted.");
			helpStringBuilder.AppendLine("		Note: If you use this option with \"-o\" option, then new substitutions will be append to the end");
			helpStringBuilder.AppendLine("		of the specified file.");
			helpStringBuilder.AppendLine();
			helpStringBuilder.AppendLine("		You can also use this option with file in input stream:");
			helpStringBuilder.AppendLine("		type inputFilename.txt | PasswordListGenerator subs -i");
			helpStringBuilder.AppendLine();
			helpStringBuilder.AppendLine("		ZB");
			helpStringBuilder.AppendLine("		2B");
			helpStringBuilder.AppendLine("		7_B");
			helpStringBuilder.AppendLine("		Z|3");
			helpStringBuilder.AppendLine("		Z8");
			helpStringBuilder.AppendLine("		2|3");
			helpStringBuilder.AppendLine("		28");
			helpStringBuilder.AppendLine("		7_|3");
			helpStringBuilder.AppendLine("		7_8");
			helpStringBuilder.AppendLine("		C");
			helpStringBuilder.AppendLine("		[");
			helpStringBuilder.AppendLine("		{");
			helpStringBuilder.AppendLine("		(");
			helpStringBuilder.AppendLine("		<");
			helpStringBuilder.AppendLine();
			helpStringBuilder.AppendLine("		It's output for inputFilename.txt file, contains \"ZB\\r\\nC\"");
			helpStringBuilder.AppendLine("		You can use \"-o\" option to save it output directly in out file or use");
			helpStringBuilder.AppendLine("		\">> outFilename.txt\" to redirect application output (not recommended, because all supporting");
			helpStringBuilder.AppendLine("		information will be included into the file)");
			return helpStringBuilder.ToString();
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